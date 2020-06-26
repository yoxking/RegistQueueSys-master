using System;

namespace EntFrm.Business.Model
{
  public class LogsInfo
  {
     public LogsInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string LogNo;
       public string sLogNo
       {
           set { this.LogNo =value;}
           get { return this.LogNo;}
        }

       private string LogTitle;
       public string sLogTitle
       {
           set { this.LogTitle =value;}
           get { return this.LogTitle;}
        }

       private string ClassNo;
       public string sClassNo
       {
           set { this.ClassNo =value;}
           get { return this.ClassNo;}
        }

       private string GradeNo;
       public string sGradeNo
       {
           set { this.GradeNo =value;}
           get { return this.GradeNo;}
        }

       private string LContent;
       public string sLContent
       {
           set { this.LContent =value;}
           get { return this.LContent;}
        }

       private string IpAddress;
       public string sIpAddress
       {
           set { this.IpAddress =value;}
           get { return this.IpAddress;}
        }

       private string Location;
       public string sLocation
       {
           set { this.Location =value;}
           get { return this.Location;}
        }

       private int CheckState;
       public int iCheckState
       {
           set { this.CheckState =value;}
           get { return this.CheckState;}
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

