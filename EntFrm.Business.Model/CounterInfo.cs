using System;

namespace EntFrm.Business.Model
{
  public class CounterInfo
  {
     public CounterInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string CounterNo;
       public string sCounterNo
       {
           set { this.CounterNo =value;}
           get { return this.CounterNo;}
        }

       private string CounterName;
       public string sCounterName
       {
           set { this.CounterName =value;}
           get { return this.CounterName;}
        }

       private string CounterAlias;
       public string sCounterAlias
       {
           set { this.CounterAlias =value;}
           get { return this.CounterAlias;}
        }

       private string ServiceGroupValue;
       public string sServiceGroupValue
       {
           set { this.ServiceGroupValue =value;}
           get { return this.ServiceGroupValue;}
        }

       private string ServiceGroupText;
       public string sServiceGroupText
       {
           set { this.ServiceGroupText =value;}
           get { return this.ServiceGroupText;}
        }

       private string VoiceStyleNos;
       public string sVoiceStyleNos
       {
           set { this.VoiceStyleNos =value;}
           get { return this.VoiceStyleNos;}
        }

       private string LedDisplayNo;
       public string sLedDisplayNo
       {
           set { this.LedDisplayNo =value;}
           get { return this.LedDisplayNo;}
        }

       private int LedAddress;
       public int iLedAddress
       {
           set { this.LedAddress =value;}
           get { return this.LedAddress;}
        }

       private string CallerNo;
       public string sCallerNo
       {
           set { this.CallerNo =value;}
           get { return this.CallerNo;}
        }

       private int CallerAddress;
       public int iCallerAddress
       {
           set { this.CallerAddress =value;}
           get { return this.CallerAddress;}
        }

       private int IsAutoLogon;
       public int iIsAutoLogon
       {
           set { this.IsAutoLogon =value;}
           get { return this.IsAutoLogon;}
        }

       private int LogonState;
       public int iLogonState
       {
           set { this.LogonState =value;}
           get { return this.LogonState;}
        }

       private string LogonStafferNo;
       public string sLogonStafferNo
       {
           set { this.LogonStafferNo =value;}
           get { return this.LogonStafferNo;}
        }

       private int PauseState;
       public int iPauseState
       {
           set { this.PauseState =value;}
           get { return this.PauseState;}
        }

       private int CalledNum;
       public int iCalledNum
       {
           set { this.CalledNum =value;}
           get { return this.CalledNum;}
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

