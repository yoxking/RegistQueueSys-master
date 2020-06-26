using System;

namespace EntFrm.Business.Model
{
  public class DsProgramInfo
  {
     public DsProgramInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string ProgmNo;
       public string sProgmNo
       {
           set { this.ProgmNo =value;}
           get { return this.ProgmNo;}
        }

       private string ProgmName;
       public string sProgmName
       {
           set { this.ProgmName =value;}
           get { return this.ProgmName;}
        }

       private string PClassNo;
       public string sPClassNo
       {
           set { this.PClassNo =value;}
           get { return this.PClassNo;}
        }

       private string PosterUrl;
       public string sPosterUrl
       {
           set { this.PosterUrl =value;}
           get { return this.PosterUrl;}
        }

       private int IsTemplate;
       public int iIsTemplate
       {
           set { this.IsTemplate =value;}
           get { return this.IsTemplate;}
        }

       private string PFilePath;
       public string sPFilePath
       {
           set { this.PFilePath =value;}
           get { return this.PFilePath;}
        }

       private string PWebUrl;
       public string sPWebUrl
       {
           set { this.PWebUrl =value;}
           get { return this.PWebUrl;}
        }

       private string PContent;
       public string sPContent
       {
           set { this.PContent =value;}
           get { return this.PContent;}
        }

       private int SlideNum;
       public int iSlideNum
       {
           set { this.SlideNum =value;}
           get { return this.SlideNum;}
        }

       private int PVersion;
       public int iPVersion
       {
           set { this.PVersion =value;}
           get { return this.PVersion;}
        }

       private int Duration;
       public int iDuration
       {
           set { this.Duration =value;}
           get { return this.Duration;}
        }

       private string Resolution;
       public string sResolution
       {
           set { this.Resolution =value;}
           get { return this.Resolution;}
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

