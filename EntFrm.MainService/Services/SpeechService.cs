using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility; 
using EntFrm.MainService.Entities;
using EntFrm.MainService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EntFrm.MainService.Services
{
    public class SpeechService
    {
        private volatile static SpeechService _instance = null;
        private static readonly object lockHelper = new object();
        private AsynQueue<SpeechData> speechQueue;

        public static SpeechService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new SpeechService();

                }
            }
            return _instance;
        }

        private SpeechService()
        {
            speechQueue = new AsynQueue<SpeechData>();
            speechQueue.ProcessItemFunction += doSpeechText;
            speechQueue.ProcessException += doExpection; //new EventHandler<EventArgs<Exception>>(C);
        }


        private void doSpeechText(SpeechData data)
        {
            try
            {
                //if (!string.IsNullOrEmpty(data.PreMusic))
                //{
                //    doPlayMusic(data.PreMusic,data.VoiceVolume);
                //    //Thread.Sleep(1000);
                //}

                //CustomSpeech cs = new CustomSpeech();
                //cs.SpeakText(data.VoiceText, data.VoiceName, data.VoiceVolume, data.VoiceRate); 

                MainFrame.DoSpeechText(data.VoiceText, data.VoiceName, data.VoiceVolume, data.VoiceRate);
                doPlayVoice_Android(data.CounterNo, data.VoiceText);
                Thread.Sleep(1000);
            }
            catch (Exception ex) {
                MainFrame.PrintMessage(ex.Message);
            }
        }

        private void doExpection(object ex, EventArgs<Exception> args)
        {
        }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="sTicketFlowNo"></param>
        /// <param name="sCounterNo"></param>
        /// <returns></returns>
        private void doPlayMusic(string sMusicFile, int iVolume)
        {
            try
            {
                MediaPlayEx mplayer = new MediaPlayEx();
                mplayer.OpenMusic(sMusicFile, IntPtr.Zero);
                mplayer.PlayMusic();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 播放叫号语音 tts
        /// </summary>
        /// <param name="sTicketFlowNo"></param>
        /// <param name="sCounterNo"></param>
        /// <returns></returns>
        public bool doPlayVoice(string sCounterNo, string sPFlowNo, string sVoiceModel = "calling")
        {
            try
            {
                bool bResult = false;
                SpeechData speech = null;
                VoiceInfoBLL ttsBoss = new VoiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfo counterInfo = IPublicHelper.GetCounterByNo(sCounterNo);

                if (counterInfo != null && speechQueue != null)
                {
                    string[] ttsNos = counterInfo.sVoiceStyleNos.Split(';');
                    string strSpeech = "";
                    VoiceInfo ttsInfo = null;

                    foreach (string ttsNo in ttsNos)
                    {
                        ttsInfo = ttsBoss.GetRecordByNo(ttsNo);
                        if (ttsInfo != null)
                        {
                            if (sVoiceModel.Equals("calling"))
                            {
                                strSpeech = ttsInfo.sFormatCalling;
                            }
                            else
                            {
                                strSpeech = ttsInfo.sFormatWaiting;
                            }
                            strSpeech = IPublicHelper.ReplaceVariables(strSpeech, sPFlowNo);

                            speech = new SpeechData();
                            speech.CounterNo = counterInfo.sCounterNo;
                            speech.VoiceName = ttsInfo.sVoice;
                            speech.VoiceRate = ttsInfo.iRate;
                            speech.VoiceVolume = ttsInfo.iVolume;
                            speech.VoiceText = strSpeech;
                            speech.PreMusic = ttsInfo.sPreMusic;
                            speech.PostMusic = ttsInfo.sPostMusic;

                            //插入语音播放队列
                            speechQueue.Enqueue(speech);
                            //doPlayVoice_Android(sCounterNo, strSpeech); 

                            bResult = true;
                        }
                    }
                }
                return bResult;
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "播放语音出错，详细：" + ex.Message);
                return false;
            }
        }

        public void doPlayVoice(string sCounterNo, string sVoiceName, int iVoiceRate, int iVoiceVolume, string sText)
        {
            SpeechData speech = new SpeechData();
            speech.CounterNo = sCounterNo;
            speech.VoiceName = sVoiceName;
            speech.VoiceRate = iVoiceRate;
            speech.VoiceVolume = iVoiceVolume;
            speech.VoiceText = sText;
            speech.PreMusic = "";
            speech.PostMusic = "";

            //插入语音播放队列
            speechQueue.Enqueue(speech);
            //doPlayVoice_Android(sCounterNo, sText);
        }

        private void doPlayVoice_Android(string counterNo, string strSpeech)
        {
            try
            {
                int count = 0;
                HashSet<string> codeSet = new HashSet<string>();
                string stemp = Base64Helper.Base64Encode(strSpeech);
                string where = " Comments Like '%" + counterNo + "%' ";

                DsHrtbeatFlowsBLL infoBLL = new DsHrtbeatFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                DsHrtbeatFlowsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 10, where);

                if (infoColl != null && infoColl.Count > 0)
                {
                    CmmdData command = new CmmdData();
                    command.cmmdName = "doSpeech";
                    command.cmmdType = "MAdapter";
                    command.cmmdArgs = new string[] { stemp };

                    stemp = JsonConvert.SerializeObject(command);

                    foreach (DsHrtbeatFlows info in infoColl)
                    {
                        codeSet.Add(info.sPlayerCode);
                    }

                    foreach(var playerCode in codeSet)
                    { 
                        RmtCmdService.CreateInstance().doRemoteCommand(playerCode, stemp);
                    }
                }
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage(ex.Message);
            }
        }

    }
}
