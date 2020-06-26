using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.MainService.Entities;
using EntFrm.MainService.Models;
using System;
using System.Threading;

namespace EntFrm.MainService.Services
{
    public class DisplayService
    {
        private volatile static DisplayService _instance = null;
        private static readonly object lockHelper = new object();
        private AsynQueue<DisplayData> displayQueue;

        public static DisplayService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new DisplayService();

                }
            }
            return _instance;
        }

        private DisplayService()
        {
            displayQueue = new AsynQueue<DisplayData>();
            displayQueue.ProcessItemFunction += doShowText;
            displayQueue.ProcessException += doExpection; //new EventHandler<EventArgs<Exception>>(C);
        }


        private void doShowText(DisplayData data)
        {
            try
            {
                if (data != null && data.DisplayLed != null)
                {
                    object obj = null;
                    string sText = data.DisplayText;
                    LEDDisplay led = data.DisplayLed;

                    if (led.sLedModel.Equals("Eq2013"))
                    {
                        Eq2013Display ledModel = new Eq2013Display(led.sDisplayNo, sText);
                        Thread t = new Thread(new ThreadStart(ledModel.ShowLedText));
                        //t.Priority = ThreadPriority.Highest;
                        t.Start();
                    }
                    else if (led.sLedModel.Equals("Pdc102"))
                    {
                        Pdc102LedDisplay led102 = null;
                        obj = SeriportService.CreateInstance().getSerialPort(led.sSerialPort);
                        if (obj == null)
                        {
                            led102 = new Pdc102LedDisplay();
                            if (led102.Open(led.sSerialPort))
                            {
                                SeriportService.CreateInstance().setSerialPort(led.sSerialPort, led102);
                            }
                        }
                        else
                        {
                            if (obj is Pdc102LedDisplay)
                            {
                                led102 = (Pdc102LedDisplay)obj;
                            }
                        }

                        if (led102 != null)
                        {
                            led102.SendData(led.iPhyAddr, sText);
                        }
                    }
                    else if (led.sLedModel.Equals("Pdc101"))
                    {
                        int showMode = 0;
                        int mhandler = 0;
                        if (sText.Length > led.iDisplayLength)
                        {
                            showMode = 1;
                        }

                        Pdc101LedDisplay led101 = new Pdc101LedDisplay();
                        obj = SeriportService.CreateInstance().getSerialPort(led.sSerialPort);
                        if (obj == null)
                        {
                            mhandler = led101.Open(led.sSerialPort, 9600);
                            if (mhandler > 0)
                            {
                                SeriportService.CreateInstance().setSerialPort(led.sSerialPort, mhandler);
                            }
                        }
                        else
                        {
                            if (obj is int)
                            {
                                mhandler = (int)obj;
                            }
                        }
                        if (mhandler > 0)
                        {
                            led101.SendData(mhandler, led.iPhyAddr, sText, 51, showMode, 3, 3);
                        }
                    }
                    Thread.Sleep(500);
                }
            }
            catch (Exception ex) { }
        }

        private void doExpection(object ex, EventArgs<Exception> args)
        {
        }

        public void doInitialLedTip()
        {
            try
            {
                DisplayData dispData = null;
                int count = 0;
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
                LEDDisplayBLL ledBoss = new LEDDisplayBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                LEDDisplayCollections ledColl = ledBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (ledColl != null && ledColl.Count > 0)
                {  
                    foreach (LEDDisplay led in ledColl)
                    {
                        dispData = new DisplayData();
                        dispData.DisplayLed = led;
                        dispData.DisplayText = led.sPowerOnTip;

                        displayQueue.Enqueue(dispData);
                    }
                }
                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "窗口屏初始化完成...");
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "窗口屏初始化时出错，详细：" + ex.Message);
            }
        }

        public void doRefreshLedTip()
        {
            try
            {
                DisplayData dispData = null;
                int count = 0;
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
                LEDDisplayBLL ledBoss = new LEDDisplayBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                LEDDisplayCollections ledColl = ledBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (ledColl != null && ledColl.Count > 0)
                { 
                    foreach (LEDDisplay led in ledColl)
                    {  
                        CounterInfo counter = IPublicHelper.GetCounterByDisplayNo(led.sDisplayNo);

                        TimeSpan timeSpan = DateTime.Now - led.dUpdateTime;
                        if (counter != null && counter.iPauseState == 0 && timeSpan.TotalSeconds > led.iTimeoutSec && led.iUpdateFlag == 1)
                        {
                            led.iUpdateFlag = 0;
                            led.dUpdateTime = DateTime.Now;
                            ledBoss.UpdateRecord(led);
                             
                            dispData = new DisplayData();
                            dispData.DisplayLed = led;
                            dispData.DisplayText = led.sTimeoutTip;

                            displayQueue.Enqueue(dispData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage("LED屏刷新出错：" + ex.Message);
            }
        }

        public bool doDisplayText(string sCounterNo, string sPFlowNo )
        {
            try
            {
                DisplayData dispData = null;
                CounterInfo counter = IPublicHelper.GetCounterByNo(sCounterNo);

                if (counter != null)
                {
                    LEDDisplayBLL ledBoss = new LEDDisplayBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                    LEDDisplay ledInfo = ledBoss.GetRecordByNo(counter.sLedDisplayNo);
                    if (ledInfo != null)
                    {
                        ledInfo.iUpdateFlag = 1;
                        ledInfo.dUpdateTime = DateTime.Now;
                        ledBoss.UpdateRecord(ledInfo);

                        string sDisplayFormat = ledInfo.sDisplayFormat;
                        sDisplayFormat = IPublicHelper.ReplaceVariables(sDisplayFormat, sPFlowNo);

                        dispData = new DisplayData();
                        dispData.DisplayLed = ledInfo;
                        dispData.DisplayText = sDisplayFormat;

                        displayQueue.Enqueue(dispData);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage("LED屏显示出错：" + ex.Message);
                return false;
            }
        }
        public bool doDisplayText(LEDDisplay ledDisplay, string sDisplayText)
        {
            try
            {
                DisplayData dispData = new DisplayData();
                dispData.DisplayLed = ledDisplay;
                dispData.DisplayText = sDisplayText;

                displayQueue.Enqueue(dispData);
                return true;
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage("LED屏显示出错：" + ex.Message);
                return false;
            }
        }
    }
}
