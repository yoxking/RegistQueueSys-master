using System;

namespace EntFrm.Business.Model
{
  public class StafferInfo
  {
     public StafferInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string StafferNo;
       public string sStafferNo
       {
           set { this.StafferNo =value;}
           get { return this.StafferNo;}
        }

       private string StafferName;
       public string sStafferName
       {
           set { this.StafferName =value;}
           get { return this.StafferName;}
        }

       private string LoginId;
       public string sLoginId
       {
           set { this.LoginId =value;}
           get { return this.LoginId;}
        }

       private string Password;
       public string sPassword
       {
           set { this.Password =value;}
           get { return this.Password;}
        }

       private string CounterNo;
       public string sCounterNo
       {
           set { this.CounterNo =value;}
           get { return this.CounterNo;}
        }

       private string OrganizNo;
       public string sOrganizNo
       {
           set { this.OrganizNo =value;}
           get { return this.OrganizNo;}
        }

       private string OrganizName;
       public string sOrganizName
       {
           set { this.OrganizName =value;}
           get { return this.OrganizName;}
        }

       private string StarLevel;
       public string sStarLevel
       {
           set { this.StarLevel =value;}
           get { return this.StarLevel;}
        }

       private string HeadPhoto;
       public string sHeadPhoto
       {
           set { this.HeadPhoto =value;}
           get { return this.HeadPhoto;}
        }

       private string Ranks;
       public string sRanks
       {
           set { this.Ranks =value;}
           get { return this.Ranks;}
        }

       private string Posts;
       public string sPosts
       {
           set { this.Posts =value;}
           get { return this.Posts;}
        }

       private string Summary;
       public string sSummary
       {
           set { this.Summary =value;}
           get { return this.Summary;}
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

