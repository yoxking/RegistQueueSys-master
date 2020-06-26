using System;

namespace EntFrm.Business.Model
{
  public class ServiceInfo
  {
     public ServiceInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string ServiceNo;
       public string sServiceNo
       {
           set { this.ServiceNo =value;}
           get { return this.ServiceNo;}
        }

       private string ServiceName;
       public string sServiceName
       {
           set { this.ServiceName =value;}
           get { return this.ServiceName;}
        }

       private string ServiceAlias;
       public string sServiceAlias
       {
           set { this.ServiceAlias =value;}
           get { return this.ServiceAlias;}
        }

       private string ServiceType;
       public string sServiceType
       {
           set { this.ServiceType =value;}
           get { return this.ServiceType;}
        }

       private int StartNum;
       public int iStartNum
       {
           set { this.StartNum =value;}
           get { return this.StartNum;}
        }

       private int EndNum;
       public int iEndNum
       {
           set { this.EndNum =value;}
           get { return this.EndNum;}
        }

       private int AlarmNum;
       public int iAlarmNum
       {
           set { this.AlarmNum =value;}
           get { return this.AlarmNum;}
        }

       private int DigitLength;
       public int iDigitLength
       {
           set { this.DigitLength =value;}
           get { return this.DigitLength;}
        }

       private string WorkflowValue;
       public string sWorkflowValue
       {
           set { this.WorkflowValue =value;}
           get { return this.WorkflowValue;}
        }

       private string WorkflowText;
       public string sWorkflowText
       {
           set { this.WorkflowText =value;}
           get { return this.WorkflowText;}
        }

       private string TicketButtonFmt;
       public string sTicketButtonFmt
       {
           set { this.TicketButtonFmt =value;}
           get { return this.TicketButtonFmt;}
        }

       private string TicketStyleNo;
       public string sTicketStyleNo
       {
           set { this.TicketStyleNo =value;}
           get { return this.TicketStyleNo;}
        }

       private int AMLimit;
       public int iAMLimit
       {
           set { this.AMLimit =value;}
           get { return this.AMLimit;}
        }

       private DateTime AMStartTime;
       public DateTime dAMStartTime
       {
           set { this.AMStartTime =value;}
           get { return this.AMStartTime;}
        }

       private DateTime AMEndTime;
       public DateTime dAMEndTime
       {
           set { this.AMEndTime =value;}
           get { return this.AMEndTime;}
        }

       private int AMTotal;
       public int iAMTotal
       {
           set { this.AMTotal =value;}
           get { return this.AMTotal;}
        }

       private int PMLimit;
       public int iPMLimit
       {
           set { this.PMLimit =value;}
           get { return this.PMLimit;}
        }

       private DateTime PMStartTime;
       public DateTime dPMStartTime
       {
           set { this.PMStartTime =value;}
           get { return this.PMStartTime;}
        }

       private DateTime PMEndTime;
       public DateTime dPMEndTime
       {
           set { this.PMEndTime =value;}
           get { return this.PMEndTime;}
        }

       private int PMTotal;
       public int iPMTotal
       {
           set { this.PMTotal =value;}
           get { return this.PMTotal;}
        }

       private int WeekLimit;
       public int iWeekLimit
       {
           set { this.WeekLimit =value;}
           get { return this.WeekLimit;}
        }

       private string WeekDays;
       public string sWeekDays
       {
           set { this.WeekDays =value;}
           get { return this.WeekDays;}
        }

       private int PrintPause;
       public int iPrintPause
       {
           set { this.PrintPause =value;}
           get { return this.PrintPause;}
        }

       private string ParentNo;
       public string sParentNo
       {
           set { this.ParentNo =value;}
           get { return this.ParentNo;}
        }

       private int HaveChild;
       public int iHaveChild
       {
           set { this.HaveChild =value;}
           get { return this.HaveChild;}
        }

       private int IsShowDialog;
       public int iIsShowDialog
       {
           set { this.IsShowDialog =value;}
           get { return this.IsShowDialog;}
        }

       private string ShowDialogName;
       public string sShowDialogName
       {
           set { this.ShowDialogName =value;}
           get { return this.ShowDialogName;}
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

