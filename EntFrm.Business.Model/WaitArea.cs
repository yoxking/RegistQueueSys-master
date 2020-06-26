using System;

namespace EntFrm.Business.Model
{
  public class WaitArea
  {
     public WaitArea(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string WAreaNo;
       public string sWAreaNo
       {
           set { this.WAreaNo =value;}
           get { return this.WAreaNo;}
        }

       private string WAreaName;
       public string sWAreaName
       {
           set { this.WAreaName =value;}
           get { return this.WAreaName;}
        }

       private int AreaIndex;
       public int iAreaIndex
       {
           set { this.AreaIndex =value;}
           get { return this.AreaIndex;}
        }

       private int AreaScale;
       public int iAreaScale
       {
           set { this.AreaScale =value;}
           get { return this.AreaScale;}
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

