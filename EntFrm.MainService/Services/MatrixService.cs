using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Threading;

namespace EntFrm.MainService.Services
{
    public class MatrixService
    {
        public static void doShowLedMatrix(string sCounterNo)
        {
            try
            {
                int count = 0;
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
                LEDMatrixBLL ledBoss = new LEDMatrixBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                LEDMatrixCollections ledColl = ledBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (ledColl != null && ledColl.Count > 0)
                {
                    ThreadPool.SetMaxThreads(5, 5);
                    foreach (LEDMatrix led in ledColl)
                    {
                        if (led.sMatrixModel.Equals("Eq2013"))
                        {
                            ThreadPool.QueueUserWorkItem(doRefresh_LedEq2013, led);
                        }
                        else if (led.sMatrixModel.Equals("Pdc101"))
                        {
                            led.sComments = sCounterNo;
                            ThreadPool.QueueUserWorkItem(doRefresh_LedPdc101, led);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "综合屏出错，详细：" + ex.Message);
            }
        }

        private static void doRefresh_LedEq2013(object ledMatrix)
        {
            int port = 1;
            int addr = 1;
            string sText = "";
            LEDMatrix led = null;

            if (ledMatrix != null && (ledMatrix is LEDMatrix))
            {
                try
                {
                    led = (LEDMatrix)ledMatrix;

                    port = int.Parse(led.sSerialPort.Substring(3));
                    addr = led.iPhyAddr; 

                    int screenWidth = 128;
                    int screenHeight = 64;
                    int posX = 0;
                    int posY = 0;
                    int width = 128;
                    int height = 64;
                    int fontSize = 12;
                    int fontAlign = 1;

                    string[] sparam = led.sParamFormat.Split(';');
                    if (sparam.Length == 8)
                    {
                        screenWidth = int.Parse(sparam[0]);
                        screenHeight = int.Parse(sparam[1]);

                        posX = int.Parse(sparam[2]);
                        posY = int.Parse(sparam[3]);
                        width = int.Parse(sparam[4]);
                        height = int.Parse(sparam[5]);
                        fontSize = int.Parse(sparam[6]);
                        fontAlign = int.Parse(sparam[7]); 

                        ViewTicketFlowsBLL ticketBusiness = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                        string serviceNos = "'" + led.sServiceNos.Replace(";", "','") + "'";
                        ViewTicketFlowsCollections InfoList = null;
                        string sCondition = "";
                        int pageSize = led.iDisplayRows;

                        if (serviceNos != null && serviceNos.Length > 0)
                        {
                            sCondition = "  BranchNo = '" + IUserContext.GetBranchNo() + "' And ServiceNo in (" + serviceNos + ") And ProcessStatus=1  And PrintTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'";

                            SqlModel s_model = new SqlModel();

                            s_model.iPageNo = 1;
                            s_model.iPageSize = pageSize;
                            s_model.sFields = "*";
                            s_model.sCondition = sCondition;
                            s_model.sOrderField = "ComeTime";
                            s_model.sOrderType = "Desc";
                            s_model.sTableName = "ViewTicketFlows";

                            InfoList = ticketBusiness.GetRecordsByPaging(s_model);

                            if (InfoList != null && InfoList.Count > 0)
                            {
                                sText = "";
                                foreach (ViewTicketFlows info in InfoList)
                                {
                                    sText += IPublicHelper.ReplaceVariables(led.sDisplayFormat, info.sPFlowNo) + "\n";
                                }

                                Eq2008LedDisplay.SendDatafun(led.iPhyAddr, sText, posX, posY, width, height, fontSize, fontAlign);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "综合屏(" + led.sMatrixName + ")显示出错，详细：" + ex.Message);
                }
            }
        }


        private static void doRefresh_LedPdc101(object ledMatrix)
        {
            int port = 1;
            int addr = 1;
            string counterNo = "";
            string sText = "";
            LEDMatrix led = null;

            if (ledMatrix != null)
            {
                try
                {
                    led = (LEDMatrix)ledMatrix;

                    port = int.Parse(led.sSerialPort.Substring(3));
                    addr = led.iPhyAddr;
                    counterNo = led.sComments;

                    ViewTicketFlowsBLL ticketBusiness = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                    ViewTicketFlowsCollections InfoList = null;
                    string serviceNos = "'" + led.sServiceNos.Replace(";", "','") + "'";
                    string sCondition = "";
                    int pageSize = led.iDisplayRows;

                    if (!string.IsNullOrEmpty(counterNo) && !string.IsNullOrEmpty(serviceNos))
                    {
                        sCondition = "   BranchNo = '" + IUserContext.GetBranchNo() + "' And ServiceNo in (" + serviceNos + ") And ProcessedCounterNo = '" + counterNo + "' And ProcessStatus=1 And PrintTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'";

                        SqlModel s_model = new SqlModel();

                        s_model.iPageNo = 1;
                        s_model.iPageSize = pageSize;
                        s_model.sFields = "*";
                        s_model.sCondition = sCondition;
                        s_model.sOrderField = "ComeTime";
                        s_model.sOrderType = "Desc";
                        s_model.sTableName = "ViewTicketFlows";

                        InfoList = ticketBusiness.GetRecordsByPaging(s_model);

                        if (InfoList != null && InfoList.Count > 0)
                        {
                            sText = IPublicHelper.ReplaceVariables(led.sDisplayFormat, InfoList[0].sPFlowNo);
                             
                            Pdc102LedDisplay led102 = null;
                            object obj = SeriportService.CreateInstance().getSerialPort(led.sSerialPort);
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
                                led102.SendData(addr, sText);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "综合屏(" + led.sMatrixName + ")显示出错，详细：" + ex.Message);
                }
            }
        }
    }
}
