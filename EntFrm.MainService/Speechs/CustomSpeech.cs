using DotNetSpeech;
using System;
using System.Collections;
using System.Linq;

namespace EntFrm.MainService.Speechs
{
    public class CustomSpeech
     {
        public delegate void EndSpeakHanlder();
        public  event EndSpeakHanlder EndSpeakEvent=null;
        SpVoice voice = new SpVoice();

        public CustomSpeech()
        {
            try
            {
                voice.EndStream += new _ISpeechVoiceEvents_EndStreamEventHandler(voice_EndStream);
            }
            catch (Exception ex)
            {            }
        }
        public  void Stop()
        {
            try
            {
                voice.Speak(string.Empty, SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
            }
            catch (Exception ex)
            {
                //ImcpLog.WrtieExceptionLog("AlarmBroadcaster", "Stop", ex.Message, ex.StackTrace);
            }
        }    

         void voice_EndStream(int StreamNumber, object StreamPosition)
        {
            try
            {
                if (EndSpeakEvent != null)
                {
                    EndSpeakEvent();
                }
            }
            catch (Exception ex)
            {                     }
        }
        public  void Pause()
        {
            try
            {
                voice.Pause();//暂停，使用
            }
            catch (Exception ex)
            {  }
        }
        //从暂停中继续刚才的朗读，使用
        public  void Restart()
        {
            try
            {
                voice.Resume();
            }
            catch (Exception ex)
            {}
        }


        /// <summary>
        /// 获取系统所有语音类型
        /// </summary>
        /// <returns>语音类型数组</returns>
        public System.Collections.DictionaryEntry[] GetTokens()
        {
            string m_tokenDiscription = "";
            ISpeechObjectTokens tokens = voice.GetVoices(m_tokenDiscription, "");
            int TokensCount = tokens.Count;

            if (TokensCount > 0)
            {
                System.Collections.DictionaryEntry[] tokensDict = new System.Collections.DictionaryEntry[TokensCount];
                //取得全部语音识别对象模块，及名称，以后使用
                for (int i = 0; i < TokensCount; i++)
                {
                    //从集合中取出单个 识别对象模块
                    SpObjectToken tokenItem = tokens.Item(i); //返回 SAPI.SpObjectToken

                    //取名称
                    string Description = tokenItem.GetDescription(0);

                    //放到 DictionaryEntry 对象中，key 是 识别对象模块，value 是名称
                    tokensDict[i] = new System.Collections.DictionaryEntry(tokenItem, Description);
                }
                return tokensDict;
            }
            return null;
        }

        public int SpeakText(string text,string voiceName,int volume,int rate)
        {
            try
            {
                DictionaryEntry[] dict = GetTokens();
                voice.Voice = (SpObjectToken)dict.First(kv => kv.Value.ToString().Equals(voiceName)).Key;
                //voice.Voice = voice.GetVoices("name="+voiceName, "").Item(0);
                //Voice中是语音类型(语言、男(女)声),有 Microsoft Simplified Chinese，Microsoft Mary(Sam,Mike)等,
                //也可以这样：voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0); //0选择默认的语音,
                voice.Volume = volume;
                voice.Rate = rate;//朗读语速
                int i= voice.Speak(text, SpeechVoiceSpeakFlags.SVSFlagsAsync); //SpeechVoiceSpeakFlags是语音朗读的风格    
                return i;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public  int SpeakText(string text)
        {
            try
            {
                //voice.Voice = voice.GetVoices("name=Microsoft Simplified Chinese", "").Item(0);
                //Voice中是语音类型(语言、男(女)声),有 Microsoft Simplified Chinese，Microsoft Mary(Sam,Mike)等,
                //也可以这样：voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0); //0选择默认的语音,
                voice.Volume = 100;
                voice.Rate = -2;//朗读语速
                return voice.Speak(text, SpeechVoiceSpeakFlags.SVSFlagsAsync); //SpeechVoiceSpeakFlags是语音朗读的风格     
            }
            catch (Exception ex)
            {
                      return -1;
            }
        }
        //生成语音文件
        public void GenerateVoiceFile(string text)
        {
            try
            {
                //SpeechVoiceSpeakFlags spFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;

                //SaveFileDialog dialog = new SaveFileDialog();
                //dialog.Filter = "All files (*.*)|*.*|wav files (*.wav)|*.wav";
                //dialog.Title = "保存WAV文件";
                //dialog.FilterIndex = 2;
                //dialog.RestoreDirectory = true;
                //if (dialog.ShowDialog() == true)
                //{
                //    SpeechStreamFileMode spFileMode = SpeechStreamFileMode.SSFMCreateForWrite;
                //    SpFileStream spFileStream = new SpFileStream();
                //    spFileStream.Open(dialog.FileName, spFileMode, false);
                //    voice.AudioOutputStream = spFileStream;
                //    voice.Speak(text, spFlags);
                //    voice.WaitUntilDone(1000);
                //    //上面两句一定要写上，否则产生的文件没有声音
                //    //WaitUntilDone的后面的smTimeout是一个int型
                //    spFileStream.Close();
                //}
            }
            catch (Exception ex)
            { }
        }
    }
}
