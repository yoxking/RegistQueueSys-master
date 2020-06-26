using System;

namespace EntFrm.Business.Model
{
  public class ProcessHistory
  {
     public ProcessHistory(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string HFlowNo;
       public string sHFlowNo
       {
           set { this.HFlowNo =value;}
           get { return this.HFlowNo;}
        }

       private int DataFlag;
       public int iDataFlag
       {
           set { this.DataFlag =value;}
           get { return this.DataFlag;}
        }

       private string TicketNo;
       public string sTicketNo
       {
           set { this.TicketNo =value;}
           get { return this.TicketNo;}
        }

       private string RUserNo;
       public string sRUserNo
       {
           set { this.RUserNo =value;}
           get { return this.RUserNo;}
        }

       private string CnName;
       public string sCnName
       {
           set { this.CnName =value;}
           get { return this.CnName;}
        }

       private int Age;
       public int iAge
       {
           set { this.Age =value;}
           get { return this.Age;}
        }

       private int Sex;
       public int iSex
       {
           set { this.Sex =value;}
           get { return this.Sex;}
        }

       private string ServiceNo;
       public string sServiceNo
       {
           set { this.ServiceNo =value;}
           get { return this.ServiceNo;}
        }

       private string CounterNos;
       public string sCounterNos
       {
           set { this.CounterNos =value;}
           get { return this.CounterNos;}
        }

       private string WFlowsNo;
       public string sWFlowsNo
       {
           set { this.WFlowsNo =value;}
           get { return this.WFlowsNo;}
        }

       private int WFlowsIndex;
       public int iWFlowsIndex
       {
           set { this.WFlowsIndex =value;}
           get { return this.WFlowsIndex;}
        }

       private DateTime EnqueueTime;
       public DateTime dEnqueueTime
       {
           set { this.EnqueueTime =value;}
           get { return this.EnqueueTime;}
        }

       private DateTime BeginTime;
       public DateTime dBeginTime
       {
           set { this.BeginTime =value;}
           get { return this.BeginTime;}
        }

       private DateTime FinishTime;
       public DateTime dFinishTime
       {
           set { this.FinishTime =value;}
           get { return this.FinishTime;}
        }

       private int ProcessState;
       public int iProcessState
       {
           set { this.ProcessState =value;}
           get { return this.ProcessState;}
        }

       private string ProcessFormat;
       public string sProcessFormat
       {
           set { this.ProcessFormat =value;}
           get { return this.ProcessFormat;}
        }

       private int ProcessIndex;
       public int iProcessIndex
       {
           set { this.ProcessIndex =value;}
           get { return this.ProcessIndex;}
        }

       private int PriorityType;
       public int iPriorityType
       {
           set { this.PriorityType =value;}
           get { return this.PriorityType;}
        }

       private int OrderWeight;
       public int iOrderWeight
       {
           set { this.OrderWeight =value;}
           get { return this.OrderWeight;}
        }

       private int PauseState;
       public int iPauseState
       {
           set { this.PauseState =value;}
           get { return this.PauseState;}
        }

       private int DelayType;
       public int iDelayType
       {
           set { this.DelayType =value;}
           get { return this.DelayType;}
        }

       private int DelayTimeValue;
       public int iDelayTimeValue
       {
           set { this.DelayTimeValue =value;}
           get { return this.DelayTimeValue;}
        }

       private int DelayStepValue;
       public int iDelayStepValue
       {
           set { this.DelayStepValue =value;}
           get { return this.DelayStepValue;}
        }

       private DateTime ProcessedTime;
       public DateTime dProcessedTime
       {
           set { this.ProcessedTime =value;}
           get { return this.ProcessedTime;}
        }

       private string ProcessedCounterNo;
       public string sProcessedCounterNo
       {
           set { this.ProcessedCounterNo =value;}
           get { return this.ProcessedCounterNo;}
        }

       private string ProcessedStafferNo;
       public string sProcessedStafferNo
       {
           set { this.ProcessedStafferNo =value;}
           get { return this.ProcessedStafferNo;}
        }

       private string BranchNo;
       public string sBranchNo
       {
           set { this.BranchNo =value;}
           get { return this.BranchNo;}
        }

       private string AddOptor;
       public string sAddOptor
       {
           set { this.AddOptor =value;}
           get { return this.AddOptor;}
        }

       private DateTime AddDate;
       public DateTime dAddDate
       {
           set { this.AddDate =value;}
           get { return this.AddDate;}
        }

       private string ModOptor;
       public string sModOptor
       {
           set { this.ModOptor =value;}
           get { return this.ModOptor;}
        }

       private DateTime ModDate;
       public DateTime dModDate
       {
           set { this.ModDate =value;}
           get { return this.ModDate;}
        }

       private int ValidityState;
       public int iValidityState
       {
           set { this.ValidityState =value;}
           get { return this.ValidityState;}
        }

       private string Comments;
       public string sComments
       {
           set { this.Comments =value;}
           get { return this.Comments;}
        }

       private string AppCode;
       public string sAppCode
       {
           set { this.AppCode =value;}
           get { return this.AppCode;}
        }

       private string Version;
       public string sVersion
       {
           set { this.Version =value;}
           get { return this.Version;}
        }

    }
  }

