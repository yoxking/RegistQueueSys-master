using System;

namespace EntFrm.Business.Model
{
  public class SysParams
  {
     public SysParams(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string ParamNo;
       public string sParamNo
       {
           set { this.ParamNo =value;}
           get { return this.ParamNo;}
        }

       private string KeyName;
       public string sKeyName
       {
           set { this.KeyName =value;}
           get { return this.KeyName;}
        }

       private string KeyValue;
       public string sKeyValue
       {
           set { this.KeyValue =value;}
           get { return this.KeyValue;}
        }

       private string KeyType;
       public string sKeyType
       {
           set { this.KeyType =value;}
           get { return this.KeyType;}
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

