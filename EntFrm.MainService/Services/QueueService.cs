using EntFrm.Framework.Utility;
using EntFrm.MainService.Business;
using EntFrm.MainService.Services;
using System;

namespace EntFrm.MainService.Services
{
    public class QueueService:IQueueService
    { 
        public string OnExecuteCommand(string methodName, string paramList)
        {
            try
            {
                string[] sParameters = null ;
                string sResult = "";
                //LoggerHelper.CreateInstance().Info(typeof(MainFrame), "命令：" + methodName + "；参数：" + paramList + ";" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), null);
                if (!string.IsNullOrEmpty(paramList))
                {
                    sParameters = paramList.Split('$');
                }
                else
                {
                    sParameters = new string[] { ""};
                }

                switch (methodName)
                {
                    case "HelloQueue":
                        sResult = CommonBusiness.CreateInstance().HelloQueue();
                        break;
                    case "getParamValue":
                        sResult = IUserContext.GetParamValue(sParameters[0], sParameters[1]);
                        break;
                    case "doPrintMessage":
                        sResult = CommonBusiness.CreateInstance().doPrintMessage(sParameters[0]);
                        break;
                    case "getImageFrom":
                        sResult = CommonBusiness.CreateInstance().getImageFrom(sParameters[0]);
                        break;
                    case "getHtmlSource":
                        sResult = CommonBusiness.CreateInstance().getHtmlSource(sParameters[0]);
                        break;
                    case "getVersionInfo":
                        sResult = CommonBusiness.CreateInstance().getVersionInfo(sParameters[0], sParameters[1]);
                        break;
                    case "doClearQueueData":
                        DbaseService.CreateInstance().doClearQueueData();
                        break;
                    case "doClearAllData":
                        DbaseService.CreateInstance().doClearAllData();
                        break;
                    case "getTicketStyle":
                        sResult = TicketStyleBusiness.CreateInstance().getDefaultTicketStyle();
                        break;

                    case "getAllServices":
                        sResult = ServiceInfoBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getServicesByPaging":
                        sResult = ServiceInfoBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getService":
                        sResult = ServiceInfoBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postService":
                        sResult = ServiceInfoBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;

                    case "getAllCounters":
                        sResult = CounterInfoBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getCountersByPaging":
                        sResult = CounterInfoBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getCounter":
                        sResult = CounterInfoBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postCounter":
                        sResult = CounterInfoBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;

                    case "getAllServiceRotas":
                        sResult = ServiceRotaBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getServiceRotasByPaging":
                        sResult = ServiceRotaBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getServiceRota":
                        sResult = ServiceRotaBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postServiceRota":
                        sResult = ServiceRotaBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;

                    case "getAllStaffers":
                        sResult = StafferInfoBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getStaffersByPaging":
                        sResult = StafferInfoBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getStaffer":
                        sResult = StafferInfoBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "getStaff":
                        sResult = StafferInfoBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postStaffer":
                        sResult = StafferInfoBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;

                    case "getAllRusers":
                        sResult = RUsersInfoBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getRusersByPaging":
                        sResult = RUsersInfoBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getRuser":
                        sResult = RUsersInfoBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postRuser":
                        sResult = RUsersInfoBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;

                    case "getAllStafferRotas":
                        sResult = StafferRotaBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getStafferRotasByPaging":
                        sResult = StafferRotaBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getStafferRota":
                        sResult = StafferRotaBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postStafferRota":
                        sResult = StafferRotaBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;


                    case "getAllTickFlows":
                        sResult = TicketFlowsBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getTickFlowsByPaging":
                        sResult = TicketFlowsBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getTickFlow":
                        sResult = TicketFlowsBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postTickFlow":
                        sResult = TicketFlowsBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;

                    case "getAllProcFlows":
                        sResult = ProcessFlowsBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getProcFlowsByPaging":
                        sResult = ProcessFlowsBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getProcFlow":
                        sResult = ProcessFlowsBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postProcFlow":
                        sResult = ProcessFlowsBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;

                    case "getAllEvalFlows":
                        sResult = EvaluateFlowsBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getEvalFlowsByPaging":
                        sResult = EvaluateFlowsBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getEvalFlow":
                        sResult = EvaluateFlowsBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postEvalFlow":
                        sResult = EvaluateFlowsBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;

                    case "getAllRegistFlows":
                        sResult = RegistFlowsBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getEvalRegistFlowsByPaging":
                        sResult = RegistFlowsBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getRegistFlow":
                        sResult = RegistFlowsBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postRegistFlow":
                        sResult = RegistFlowsBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;

                    case "getAllRecipeFlows":
                        sResult = RecipeFlowsBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getEvalRecipeFlowsByPaging":
                        sResult = RecipeFlowsBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getRecipeFlow":
                        sResult = RecipeFlowsBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postRecipeFlow":
                        sResult = RecipeFlowsBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;
                    case "GetRuserNameByRecipeNo":
                        sResult = RecipeFlowsBusiness.CreateInstance().GetRuserNameByRecipeNo(sParameters[0]);
                        break;

                    case "getAllRecipeDetails":
                        sResult = RecipeDetailsBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getEvalRecipeDetailsByPaging":
                        sResult = RecipeDetailsBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getRecipeDetail":
                        sResult = RecipeDetailsBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postRecipeDetail":
                        sResult = RecipeDetailsBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;
                    case "GetRecipeDetailsByRecipeNo":
                        sResult = RecipeDetailsBusiness.CreateInstance().GetRecipeDetailsByRecipeNo(sParameters[0]);
                        break;

                    case "getAllMessages":
                        sResult = MessageInfoBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getEvalMessagesByPaging":
                        sResult = MessageInfoBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getMessage":
                        sResult = MessageInfoBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postMessage":
                        sResult = MessageInfoBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break; 

                    case "doSignIn":
                        sResult = ITicketService.CreateInstance().doSignIn(sParameters[0], sParameters[1], sParameters[2]);
                        break; 
                    case "doSignoutAll":
                        ITicketService.CreateInstance().doSignoutAll();
                        break;
                    case "doSignOut":
                        sResult = ITicketService.CreateInstance().doSignOut(sParameters[0]);
                        break;
                    case "doSeekHelp":
                        sResult = ITicketService.CreateInstance().doSeekHelp(sParameters[0]);
                        break;
                    case "doOutService":
                        sResult = ITicketService.CreateInstance().doOutService(sParameters[0]);
                        break;
                    case "doInService":
                        sResult = ITicketService.CreateInstance().doInService(sParameters[0]);
                        break;
                    case "doCallNextTicket":
                        sResult = ITicketService.CreateInstance().doCallNextTicket(sParameters[0]);
                        break;
                    case "doCallSpecTicket":
                        sResult = ITicketService.CreateInstance().doCallSpecTicket(sParameters[0]);
                        break;
                    case "doSpecialTicket":
                        sResult = ITicketService.CreateInstance().doSpecialTicket(sParameters[0], sParameters[1]);
                        break;
                    case "getTransferList":
                        sResult = ITicketService.CreateInstance().getTransferList();
                        break;
                    case "doTransferTicket":
                        sResult = ITicketService.CreateInstance().doTransferTicket(sParameters[0], sParameters[1]);
                        break;
                    case "doRecallTicket":
                        sResult = ITicketService.CreateInstance().doRecallTicket(sParameters[0]);
                        break;
                    case "doStartTicket":
                        sResult = ITicketService.CreateInstance().doStartTicket(sParameters[0]);
                        break;
                    case "doFinishTicket":
                        sResult = ITicketService.CreateInstance().doFinishTicket(sParameters[0]);
                        break; 
                    case "doNotcomeTicket":
                        sResult = ITicketService.CreateInstance().doNotcomeTicket(sParameters[0]);
                        break;
                    case "doPassedTicket":
                        sResult = ITicketService.CreateInstance().doPassedTicket(sParameters[0],sParameters[1]);
                        break;
                    case "doRediagTicket":
                        sResult = ITicketService.CreateInstance().doRediagTicket(sParameters[0], sParameters[1]);
                        break;
                    case "doCallNextRecipe":
                        sResult = ITicketService.CreateInstance().doCallNextRecipe(sParameters[0]);
                        break;
                    case "doAddNewTicket1":
                        sResult = ITicketService.CreateInstance().doAddNewTicket(sParameters[0], sParameters[1], sParameters[2], sParameters[3], sParameters[4]);
                        break;
                    case "doAddNewTicket2":
                        sResult = ITicketService.CreateInstance().doAddNewTicket(sParameters[0], sParameters[1], sParameters[2], sParameters[3], sParameters[4], sParameters[5], sParameters[6]);
                        break;

                    case "doRegistEvaluator":
                        sResult = EvaluateService.CreateInstance().doRegistEvaluator(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "doFinishEvalutator":
                        sResult = EvaluateService.CreateInstance().doFinishEvaluator(sParameters[0], sParameters[1], sParameters[2]);
                        break;

                    case "doRegistScan":
                        sResult = IRegistService.CreateInstance().doRegistScan(sParameters[0]);
                        break; 
                    case "doRegistScanByRFlowNo": 
                        sResult = IRegistService.CreateInstance().doRegistScanByRFlowNo(sParameters[0]);
                        break;
                    case "doRegistScanByRuserNo":
                        sResult = IRegistService.CreateInstance().doRegistScanByRuserNo(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "doRegistScanByRecipeNo":
                        sResult = IRegistService.CreateInstance().doRegistScanByRecipeNo(sParameters[0]);
                        break;


                    case "getCallingCountByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getCallingCountByCounterNo(sParameters[0]);
                        break;
                    case "getWaitingCountByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getWaitingCountByCounterNo(sParameters[0]);
                        break;
                    case "getQueuingCountByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getQueuingCountByCounterNo(sParameters[0]);
                        break;
                    case "getPassedCountByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getPassedCountByCounterNo(sParameters[0]);
                        break;
                    case "getFinishedCountByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getFinishedCountByCounterNo(sParameters[0]);
                        break;
                    case "getCallingListByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getCallingListByCounterNo(sParameters[0], sParameters[1]);
                        break;
                    case "getWaitingListByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getWaitingListByCounterNo(sParameters[0], sParameters[1]);
                        break;
                    case "getQueuingListByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getQueuingListByCounterNo(sParameters[0], sParameters[1]);
                        break;
                    case "getNonarrivalListByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getNonarrivalListByCounterNo(sParameters[0], sParameters[1]);
                        break;
                    case "getFinishedListByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getFinishedListByCounterNo(sParameters[0], sParameters[1]);
                        break;


                    case "getCallingCountByServiceNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getCallingCountByServiceNo(sParameters[0]);
                        break;
                    case "getWaitingCountByServiceNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getWaitingCountByServiceNo(sParameters[0]);
                        break;
                    case "getQueuingCountByServiceNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getQueuingCountByServiceNo(sParameters[0]);
                        break;
                    case "getCallingListByServiceNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getCallingListByServiceNo(sParameters[0], sParameters[1]);
                        break;
                    case "getWaitingListByServiceNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getWaitingListByServiceNo(sParameters[0], sParameters[1]);
                        break;
                    case "getQueuingListByServiceNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getQueuingListByServiceNo(sParameters[0], sParameters[1]);
                        break;


                    case "getCallingCountByStafferNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getCallingCountByStafferNo(sParameters[0]);
                        break;
                    case "getWaitingCountByStafferNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getWaitingCountByStafferNo(sParameters[0]);
                        break;
                    case "getQueuingCountByStafferNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getQueuingCountByStafferNo(sParameters[0]);
                        break;
                    case "getCallingListByStafferNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getCallingListByStafferNo(sParameters[0], sParameters[1]);
                        break;
                    case "getWaitingListByStafferNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getWaitingListByStafferNo(sParameters[0], sParameters[1]);
                        break;
                    case "getQueuingListByStafferNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getQueuingListByStafferNo(sParameters[0], sParameters[1]);
                        break;


                    case "getTicketFlowByTFlowNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getTicketFlowByTFlowNo(sParameters[0]);
                        break;
                    case "getVRecipeFlowByRFlowNo":
                        sResult = ViewRecipeFlowsBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "getVTicketFlowByPFlowNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketFlowByPFlowNo(sParameters[0]);
                        break;
                    case "getVTicketFlowsByClassNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketFlowsByClassNo(sParameters[0]);
                        break;
                    case "getVTicketFlowsByPaging":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketFlowsByPaging(sParameters[0], sParameters[1], sParameters[2], sParameters[3], sParameters[4]);
                        break;
                    case "getVTicketFlowByTicketNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketFlowByTicketNo(sParameters[0]);
                        break;
                    case "getVTicketFlowByCardNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketFlowByCardNo(sParameters[0]);
                        break;
                    case "getVTicketFlowByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketFlowByCounterNo(sParameters[0], sParameters[1], sParameters[2], sParameters[3], sParameters[4]);
                        break;
                    case "getVTicketFlowByStafferNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketFlowByStafferNo(sParameters[0], sParameters[1], sParameters[2], sParameters[3], sParameters[4]);
                        break;
                    case "getVTicketCountByCondition":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketCountByCondition(sParameters[0]);
                        break;
                    case "getVTicketCountByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketCountByCounterNo(sParameters[0], sParameters[1], sParameters[2], sParameters[3]);
                        break;
                    case "getVTicketCountByServiceNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketCountByServiceNo(sParameters[0], sParameters[1], sParameters[2], sParameters[3]);
                        break;
                    case "getVTicketCountByStafferNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketCountByStafferNo(sParameters[0], sParameters[1], sParameters[2], sParameters[3]);
                        break;
                    case "getVTicketCountByWFlowNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketCountByWFlowNo(sParameters[0], sParameters[1], sParameters[2], sParameters[3]);
                        break; 
                    default:
                        break;
                }
                return sResult;
            }
            catch(Exception ex)
            {
                LoggerHelper.CreateInstance().Error(typeof(MainFrame), ex.Message + ";" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex);
                return "";
            }
        }
    }
}
