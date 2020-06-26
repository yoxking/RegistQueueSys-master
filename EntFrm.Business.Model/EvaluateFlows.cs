using System;

namespace EntFrm.Business.Model
{
  public class EvaluateFlows
  {
     public EvaluateFlows(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string EFlowNo;
       public string sEFlowNo
       {
           set { this.EFlowNo =value;}
           get { return this.EFlowNo;}
        }

       private string PFlowNo;
       public string sPFlowNo
       {
           set { this.PFlowNo =value;}
           get { return this.PFlowNo;}
        }

       private int DataFlag;
       public int iDataFlag
       {
           set { this.DataFlag =value;}
           get { return this.DataFlag;}
        }

       private int EvaluateFlag;
       public int iEvaluateFlag
       {
           set { this.EvaluateFlag =value;}
           get { return this.EvaluateFlag;}
        }

       private DateTime EvaluateTime;
       public DateTime dEvaluateTime
       {
           set { this.EvaluateTime =value;}
           get { return this.EvaluateTime;}
        }

       private string EvalCounterNo;
       public string sEvalCounterNo
       {
           set { this.EvalCounterNo =value;}
           get { return this.EvalCounterNo;}
        }

       private string EvalStafferNo;
       public string sEvalStafferNo
       {
           set { this.EvalStafferNo =value;}
           get { return this.EvalStafferNo;}
        }

       private int EvalResult;
       public int iEvalResult
       {
           set { this.EvalResult =value;}
           get { return this.EvalResult;}
        }

       private int VeryGood;
       public int iVeryGood
       {
           set { this.VeryGood =value;}
           get { return this.VeryGood;}
        }

       private int Good;
       public int iGood
       {
           set { this.Good =value;}
           get { return this.Good;}
        }

       private int Normal;
       public int iNormal
       {
           set { this.Normal =value;}
           get { return this.Normal;}
        }

       private int Bad;
       public int iBad
       {
           set { this.Bad =value;}
           get { return this.Bad;}
        }

       private int Unknown;
       public int iUnknown
       {
           set { this.Unknown =value;}
           get { return this.Unknown;}
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

