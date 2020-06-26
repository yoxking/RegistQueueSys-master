using System;

namespace EntFrm.Business.Model
{
  public class VoiceInfo
  {
     public VoiceInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string TtsNo;
       public string sTtsNo
       {
           set { this.TtsNo =value;}
           get { return this.TtsNo;}
        }

       private string TtsName;
       public string sTtsName
       {
           set { this.TtsName =value;}
           get { return this.TtsName;}
        }

       private string Voice;
       public string sVoice
       {
           set { this.Voice =value;}
           get { return this.Voice;}
        }

       private int Rate;
       public int iRate
       {
           set { this.Rate =value;}
           get { return this.Rate;}
        }

       private int Volume;
       public int iVolume
       {
           set { this.Volume =value;}
           get { return this.Volume;}
        }

       private string FormatCalling;
       public string sFormatCalling
       {
           set { this.FormatCalling =value;}
           get { return this.FormatCalling;}
        }

       private string FormatWaiting;
       public string sFormatWaiting
       {
           set { this.FormatWaiting =value;}
           get { return this.FormatWaiting;}
        }

       private string PreMusic;
       public string sPreMusic
       {
           set { this.PreMusic =value;}
           get { return this.PreMusic;}
        }

       private string PostMusic;
       public string sPostMusic
       {
           set { this.PostMusic =value;}
           get { return this.PostMusic;}
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

