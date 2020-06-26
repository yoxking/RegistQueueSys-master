using System;

namespace EntFrm.Business.Model
{
  public class DsPublishFlows
  {
     public DsPublishFlows(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
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

       private string ProgmNo;
       public string sProgmNo
       {
           set { this.ProgmNo =value;}
           get { return this.ProgmNo;}
        }

       private string ProgmType;
       public string sProgmType
       {
           set { this.ProgmType =value;}
           get { return this.ProgmType;}
        }

       private string PlayerNos;
       public string sPlayerNos
       {
           set { this.PlayerNos =value;}
           get { return this.PlayerNos;}
        }

       private string PlayMode;
       public string sPlayMode
       {
           set { this.PlayMode =value;}
           get { return this.PlayMode;}
        }

       private string PlayWeeks;
       public string sPlayWeeks
       {
           set { this.PlayWeeks =value;}
           get { return this.PlayWeeks;}
        }

       private DateTime StartDate;
       public DateTime dStartDate
       {
           set { this.StartDate =value;}
           get { return this.StartDate;}
        }

       private DateTime EnditDate;
       public DateTime dEnditDate
       {
           set { this.EnditDate =value;}
           get { return this.EnditDate;}
        }

       private string StartTime;
       public string sStartTime
       {
           set { this.StartTime =value;}
           get { return this.StartTime;}
        }

       private string EnditTime;
       public string sEnditTime
       {
           set { this.EnditTime =value;}
           get { return this.EnditTime;}
        }

       private int PublishState;
       public int iPublishState
       {
           set { this.PublishState =value;}
           get { return this.PublishState;}
        }

       private string PublishOptor;
       public string sPublishOptor
       {
           set { this.PublishOptor =value;}
           get { return this.PublishOptor;}
        }

       private DateTime PublishDate;
       public DateTime dPublishDate
       {
           set { this.PublishDate =value;}
           get { return this.PublishDate;}
        }

       private int CheckState;
       public int iCheckState
       {
           set { this.CheckState =value;}
           get { return this.CheckState;}
        }

       private string CheckOptor;
       public string sCheckOptor
       {
           set { this.CheckOptor =value;}
           get { return this.CheckOptor;}
        }

       private DateTime CheckDate;
       public DateTime dCheckDate
       {
           set { this.CheckDate =value;}
           get { return this.CheckDate;}
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

