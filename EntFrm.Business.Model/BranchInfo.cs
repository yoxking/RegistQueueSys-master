using System;

namespace EntFrm.Business.Model
{
  public class BranchInfo
  {
     public BranchInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string BranchNo;
       public string sBranchNo
       {
           set { this.BranchNo =value;}
           get { return this.BranchNo;}
        }

       private string BranchName;
       public string sBranchName
       {
           set { this.BranchName =value;}
           get { return this.BranchName;}
        }

       private string BranchCode;
       public string sBranchCode
       {
           set { this.BranchCode =value;}
           get { return this.BranchCode;}
        }

       private int BranchType;
       public int iBranchType
       {
           set { this.BranchType =value;}
           get { return this.BranchType;}
        }

       private string Contacts;
       public string sContacts
       {
           set { this.Contacts =value;}
           get { return this.Contacts;}
        }

       private string Telphone;
       public string sTelphone
       {
           set { this.Telphone =value;}
           get { return this.Telphone;}
        }

       private string Summary;
       public string sSummary
       {
           set { this.Summary =value;}
           get { return this.Summary;}
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

