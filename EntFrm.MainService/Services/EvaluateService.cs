using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.TextLine;
using System;
using System.Net;
using System.Text;
using System.Threading;

namespace EntFrm.MainService.Services
{
    public class EvaluateService
    {
        private volatile static EvaluateService _instance = null;
        private static readonly object lockHelper = new object();

        public static EvaluateService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new EvaluateService();
                }
            }
            return _instance;
        }


        /// <summary>  
        /// socket发送字节数组  
        /// </summary>  
        /// <param name="sendData">要发送的byte[]</param>  
        /// <param name="ip">服务器IP</param>  
        /// <param name="port">服务器端口</param>  
        /// <returns>接收的byte[]</returns>  
        private void doEvaluteCommand(string ip, int port, string message)
        {
            try
            {
                IoSession session = null;
                var connector = new Mina.Transport.Socket.AsyncSocketConnector();
                connector.FilterChain.AddLast("codec", new ProtocolCodecFilter(new TextLineCodecFactory(Encoding.UTF8)));

                connector.DefaultRemoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

                connector.MessageReceived += (ss, ee) =>//接收到消息后
                {
                    //Console.WriteLine("客户端收到" + ee.Session.RemoteEndPoint + "的消息：" + ee.Message + "");

                    if (session != null)
                    {
                        session.Close(false);
                    }
                };


                connector.SessionClosed += (ss, ee) =>
                {
                    //Console.WriteLine("SessionClosed");
                };

                session = connector.Connect().Await().Session;
                session.Write(message + "|End");

            }
            catch (Exception ex)
            { }
        }

        //评价器登录
        public void doSigninEvaluator(string counterNo, string stafferNo)
        {
            try
            {
                EvaluatorInfo evaluator = null;
                CounterInfo counter = IPublicHelper.GetCounterByNo(counterNo);

                if (counter != null)
                {
                    CallerInfo caller = IPublicHelper.GetCallerByNo(counter.sCallerNo);

                    evaluator = IPublicHelper.GetEvaluatorByNo(caller.sEvalorNo);
                    if (evaluator != null)
                    {
                        //使用Lamdba表达式
                        new Thread(() =>
                        {
                            doEvaluteCommand(evaluator.sEvaIpAddr, int.Parse(evaluator.sEvaLcPort), "doSignIn|" + stafferNo + "|" + counterNo);
                        }).Start();
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        //评价器退出
        public void doSignoutEvaluator(string counterNo)
        {
            try
            {
                EvaluatorInfo evaluator = null;
                CounterInfo counter = IPublicHelper.GetCounterByNo(counterNo);

                if (counter != null)
                {
                    CallerInfo caller = IPublicHelper.GetCallerByNo(counter.sCallerNo);

                    evaluator = IPublicHelper.GetEvaluatorByNo(caller.sEvalorNo);
                    if (evaluator != null)
                    {
                        //使用Lamdba表达式
                        new Thread(() =>
                        {
                            doEvaluteCommand(evaluator.sEvaIpAddr, int.Parse(evaluator.sEvaLcPort), "doSignOut");
                        }).Start();
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        //评价器暂停服务
        public void doPauseEvaluator(string counterNo)
        {
            try
            {
                EvaluatorInfo evaluator = null;
                CounterInfo counter = IPublicHelper.GetCounterByNo(counterNo);

                if (counter != null)
                {
                    CallerInfo caller = IPublicHelper.GetCallerByNo(counter.sCallerNo);

                    evaluator = IPublicHelper.GetEvaluatorByNo(caller.sEvalorNo);
                    if (evaluator != null)
                    {
                        //使用Lamdba表达式
                        new Thread(() =>
                        {
                            doEvaluteCommand(evaluator.sEvaIpAddr, int.Parse(evaluator.sEvaLcPort), "doPause");
                        }).Start();
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        //评价器开始服务
        public void doResumeEvaluator(string counterNo)
        {
            try
            {
                EvaluatorInfo evaluator = null;
                CounterInfo counter = IPublicHelper.GetCounterByNo(counterNo);

                if (counter != null)
                {
                    CallerInfo caller = IPublicHelper.GetCallerByNo(counter.sCallerNo);

                    evaluator = IPublicHelper.GetEvaluatorByNo(caller.sEvalorNo);
                    if (evaluator != null)
                    {
                        //使用Lamdba表达式
                        new Thread(() =>
                        {
                            doEvaluteCommand(evaluator.sEvaIpAddr, int.Parse(evaluator.sEvaLcPort), "doResume");
                        }).Start();
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        //评价器欢迎光临
        public void doWelcomeEvaluator(string counterNo)
        {
            try
            {
                EvaluatorInfo evaluator = null;
                CounterInfo counter = IPublicHelper.GetCounterByNo(counterNo);

                if (counter != null)
                {
                    CallerInfo caller = IPublicHelper.GetCallerByNo(counter.sCallerNo);

                    evaluator = IPublicHelper.GetEvaluatorByNo(caller.sEvalorNo);
                    if (evaluator != null)
                    {
                        //使用Lamdba表达式
                        new Thread(() =>
                        {
                            doEvaluteCommand(evaluator.sEvaIpAddr, int.Parse(evaluator.sEvaLcPort), "doWelcome");
                        }).Start();
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        //评价器黄线外等待
        public void doWaitingEvaluator(string counterNo)
        {
            try
            {
                EvaluatorInfo evaluator = null;
                CounterInfo counter = IPublicHelper.GetCounterByNo(counterNo);

                if (counter != null)
                {
                    CallerInfo caller = IPublicHelper.GetCallerByNo(counter.sCallerNo);

                    evaluator = IPublicHelper.GetEvaluatorByNo(caller.sEvalorNo);
                    if (evaluator != null)
                    {
                        //使用Lamdba表达式
                        new Thread(() =>
                        {
                            doEvaluteCommand(evaluator.sEvaIpAddr, int.Parse(evaluator.sEvaLcPort), "doWaiting");
                        }).Start();
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        //评价器开始评价
        public void doStartEvaluator(string counterNo)
        {
            try
            {
                EvaluatorInfo evaluator = null;
                CounterInfo counter = IPublicHelper.GetCounterByNo(counterNo);

                if (counter != null)
                {
                    CallerInfo caller = IPublicHelper.GetCallerByNo(counter.sCallerNo);

                    evaluator = IPublicHelper.GetEvaluatorByNo(caller.sEvalorNo);
                    if (evaluator != null)
                    {
                        //使用Lamdba表达式
                        new Thread(() =>
                        {
                            doEvaluteCommand(evaluator.sEvaIpAddr, int.Parse(evaluator.sEvaLcPort), "doEvaluate");
                        }).Start();
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        //评价器完成评价 
        public string doFinishEvaluator(string sCounterNo, string sStafferNo, string sScore)
        {
            try
            {
                string sResult = "";

                int score = int.Parse(sScore);
                EvaluateFlowsBLL evalBoss = new EvaluateFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ProcessFlowsBLL pflowBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

                ViewTicketFlows vTicketFlow = IBusinessHelper.getVTicketFlowBy(sCounterNo, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), IPublicConsts.PROCSTATE_CALLING+"", "Desc");
                EvaluateFlows eval = null;

                if (vTicketFlow != null)
                {
                    ProcessFlows processFlow = pflowBoss.GetRecordByNo(vTicketFlow.sPFlowNo);

                    processFlow.dFinishTime = DateTime.Now;
                    processFlow.iProcessState = IPublicConsts.PROCSTATE_EVALUATE;//完成评价
                    processFlow.sProcessedCounterNo = sCounterNo;
                    processFlow.sProcessedStafferNo = sStafferNo;
                    processFlow.dProcessedTime = DateTime.Now;
                    processFlow.dModDate = DateTime.Now;

                    pflowBoss.UpdateRecord(processFlow);

                    int count = evalBoss.GetCountByCondition(" PFlowNo='" + vTicketFlow.sPFlowNo + "' ");
                    if (count < 1)
                    {
                        eval = new EvaluateFlows();
                        eval.sEFlowNo = CommonHelper.Get_New12ByteGuid();

                        eval.sPFlowNo = vTicketFlow.sPFlowNo;
                        eval.iDataFlag = 0;
                        eval.iEvaluateFlag = 1;
                        eval.dEvaluateTime = DateTime.Now;
                        eval.sEvalCounterNo = vTicketFlow.sProcessedCounterNo;
                        eval.sEvalStafferNo = sStafferNo;
                        eval.iEvalResult = score;
                        eval.iVeryGood = (score == 3 ? 1 : 0);
                        eval.iGood = (score == 2 ? 1 : 0);
                        eval.iNormal = (score == 1 ? 1 : 0);
                        eval.iBad = (score == 0 ? 1 : 0);
                        eval.iUnknown = 0;

                        eval.sBranchNo = IUserContext.GetBranchNo();
                        eval.sAddOptor = "00000000";
                        eval.dAddDate = DateTime.Now;
                        eval.sModOptor = "00000000";
                        eval.dModDate = DateTime.Now;
                        eval.iValidityState = 1;
                        eval.sComments = "";
                        eval.sAppCode = IUserContext.GetAppCode() + ";";

                        if (evalBoss.AddNewRecord(eval))
                        {
                            sResult = vTicketFlow.sTicketNo;
                        }
                    }
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //评价器注册
        public string doRegistEvaluator(string sEvalCode, string sIpAddress, string sLocalPort)
        {
            try
            {
                int count = 0;
                EvaluatorInfoBLL evalBoss = new EvaluatorInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                EvaluatorInfoCollections evalColl = evalBoss.GetRecordsByPaging(ref count, 1, 1, " EvaVCode='" + sEvalCode + "' And BranchNo='" + IUserContext.GetBranchNo() + "' ");
                EvaluatorInfo evalInfo = null;

                if (evalColl != null && evalColl.Count > 0)
                {
                    evalInfo = evalColl.GetFirstOne();

                    //evalInfo.sEvaVCode = sEvalCode;
                    evalInfo.dRegistDate = DateTime.Now;
                    evalInfo.sEvaIpAddr = sIpAddress;
                    evalInfo.sEvaLcPort = sLocalPort;

                    evalInfo.dModDate = DateTime.Now;

                    evalBoss.UpdateRecord(evalInfo);
                }
                else
                {
                    evalInfo = new EvaluatorInfo();

                    evalInfo.sEvalorNo = CommonHelper.Get_New12ByteGuid();
                    evalInfo.sEvaVCode = sEvalCode;
                    evalInfo.dRegistDate = DateTime.Now;
                    evalInfo.sEvaIpAddr = sIpAddress;
                    evalInfo.sEvaLcPort = sLocalPort;

                    evalInfo.sBranchNo = IUserContext.GetBranchNo();
                    evalInfo.sAddOptor = "00000000";
                    evalInfo.dAddDate = DateTime.Now;
                    evalInfo.sModOptor = "00000000";
                    evalInfo.dModDate = DateTime.Now;
                    evalInfo.iValidityState = 1;
                    evalInfo.sComments = "";
                    evalInfo.sAppCode = IUserContext.GetAppCode() + ";";

                    evalBoss.AddNewRecord(evalInfo);
                }

                return "true";
            }
            catch (Exception ex)
            {
                return "false";
            }
        }


    }
}
