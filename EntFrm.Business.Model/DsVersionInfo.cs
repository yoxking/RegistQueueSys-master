using System;

namespace EntFrm.Business.Model
{
  public class DsVersionInfo
  {
     public DsVersionInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string VerNo;
       public string sVerNo
       {
           set { this.VerNo =value;}
           get { return this.VerNo;}
        }

       private string VerName;
       public string sVerName
       {
           set { this.VerName =value;}
           get { return this.VerName;}
        }

       private string VerType;
       public string sVerType
       {
           set { this.VerType =value;}
           get { return this.VerType;}
        }

       private string VerCode;
       public string sVerCode
       {
           set { this.VerCode =value;}
           get { return this.VerCode;}
        }

       private string Platform;
       public string sPlatform
       {
           set { this.Platform =value;}
           get { return this.Platform;}
        }

       private string AppStart;
       public string sAppStart
       {
           set { this.AppStart =value;}
           get { return this.AppStart;}
        }

       private string VerDesc;
       public string sVerDesc
       {
           set { this.VerDesc =value;}
           get { return this.VerDesc;}
        }

       private string UpMode;
       public string sUpMode
       {
           set { this.UpMode =value;}
           get { return this.UpMode;}
        }

       private string FileUrl;
       public string sFileUrl
       {
           set { this.FileUrl =value;}
           get { return this.FileUrl;}
        }

       private string PlayerNos;
       public string sPlayerNos
       {
           set { this.PlayerNos =value;}
           get { return this.PlayerNos;}
        }

       private int DLoadHits;
       public int iDLoadHits
       {
           set { this.DLoadHits =value;}
           get { return this.DLoadHits;}
        }

       private int CheckState;
       public int iCheckState
       {
           set { this.CheckState =value;}
           get { return this.CheckState;}
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

