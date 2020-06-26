using System;

namespace EntFrm.Business.Model
{
  public class DsDwloadFlows
  {
     public DsDwloadFlows(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string DFlowNo;
       public string sDFlowNo
       {
           set { this.DFlowNo =value;}
           get { return this.DFlowNo;}
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

       private string PlayerNo;
       public string sPlayerNo
       {
           set { this.PlayerNo =value;}
           get { return this.PlayerNo;}
        }

       private string PublishNo;
       public string sPublishNo
       {
           set { this.PublishNo =value;}
           get { return this.PublishNo;}
        }

       private DateTime DSchedule;
       public DateTime dDSchedule
       {
           set { this.DSchedule =value;}
           get { return this.DSchedule;}
        }

       private int IssueStatus;
       public int iIssueStatus
       {
           set { this.IssueStatus =value;}
           get { return this.IssueStatus;}
        }

       private DateTime IssueDate;
       public DateTime dIssueDate
       {
           set { this.IssueDate =value;}
           get { return this.IssueDate;}
        }

       private int IFailCount;
       public int iIFailCount
       {
           set { this.IFailCount =value;}
           get { return this.IFailCount;}
        }

       private int ISucCount;
       public int iISucCount
       {
           set { this.ISucCount =value;}
           get { return this.ISucCount;}
        }

       private string DloadProgress;
       public string sDloadProgress
       {
           set { this.DloadProgress =value;}
           get { return this.DloadProgress;}
        }

       private string DloadStatus;
       public string sDloadStatus
       {
           set { this.DloadStatus =value;}
           get { return this.DloadStatus;}
        }

       private string PlayRecord;
       public string sPlayRecord
       {
           set { this.PlayRecord =value;}
           get { return this.PlayRecord;}
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

