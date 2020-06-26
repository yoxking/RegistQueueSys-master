using System;

namespace EntFrm.Business.Model
{
  public class RecipeFlows
  {
     public RecipeFlows(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string RFlowNo;
       public string sRFlowNo
       {
           set { this.RFlowNo =value;}
           get { return this.RFlowNo;}
        }

       private int DataFlag;
       public int iDataFlag
       {
           set { this.DataFlag =value;}
           get { return this.DataFlag;}
        }

       private string RegistNo;
       public string sRegistNo
       {
           set { this.RegistNo =value;}
           get { return this.RegistNo;}
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

       private string CounterNo;
       public string sCounterNo
       {
           set { this.CounterNo =value;}
           get { return this.CounterNo;}
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

       private int RecipeState;
       public int iRecipeState
       {
           set { this.RecipeState =value;}
           get { return this.RecipeState;}
        }

       private string RecipeOpter;
       public string sRecipeOpter
       {
           set { this.RecipeOpter =value;}
           get { return this.RecipeOpter;}
        }

       private DateTime RecipeDate;
       public DateTime dRecipeDate
       {
           set { this.RecipeDate =value;}
           get { return this.RecipeDate;}
        }

       private int ProcessState;
       public int iProcessState
       {
           set { this.ProcessState =value;}
           get { return this.ProcessState;}
        }

       private DateTime ProcessedTime;
       public DateTime dProcessedTime
       {
           set { this.ProcessedTime =value;}
           get { return this.ProcessedTime;}
        }

       private string PrcsCounterNo;
       public string sPrcsCounterNo
       {
           set { this.PrcsCounterNo =value;}
           get { return this.PrcsCounterNo;}
        }

       private string DataFrom;
       public string sDataFrom
       {
           set { this.DataFrom =value;}
           get { return this.DataFrom;}
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

