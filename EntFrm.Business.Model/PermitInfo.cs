using System;

namespace EntFrm.Business.Model
{
  public class PermitInfo
  {
     public PermitInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string PermitNo;
       public string sPermitNo
       {
           set { this.PermitNo =value;}
           get { return this.PermitNo;}
        }

       private string PermitName;
       public string sPermitName
       {
           set { this.PermitName =value;}
           get { return this.PermitName;}
        }

       private string PermitCode;
       public string sPermitCode
       {
           set { this.PermitCode =value;}
           get { return this.PermitCode;}
        }

       private string ParentNo;
       public string sParentNo
       {
           set { this.ParentNo =value;}
           get { return this.ParentNo;}
        }

       private int OrderNo;
       public int iOrderNo
       {
           set { this.OrderNo =value;}
           get { return this.OrderNo;}
        }

       private string PFunction;
       public string sPFunction
       {
           set { this.PFunction =value;}
           get { return this.PFunction;}
        }

       private string PPicture;
       public string sPPicture
       {
           set { this.PPicture =value;}
           get { return this.PPicture;}
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

