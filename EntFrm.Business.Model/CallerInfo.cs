using System;

namespace EntFrm.Business.Model
{
  public class CallerInfo
  {
     public CallerInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string CallerNo;
       public string sCallerNo
       {
           set { this.CallerNo =value;}
           get { return this.CallerNo;}
        }

       private string CallerName;
       public string sCallerName
       {
           set { this.CallerName =value;}
           get { return this.CallerName;}
        }

       private string Protocol;
       public string sProtocol
       {
           set { this.Protocol =value;}
           get { return this.Protocol;}
        }

       private string SerialPort;
       public string sSerialPort
       {
           set { this.SerialPort =value;}
           get { return this.SerialPort;}
        }

       private string CommMode;
       public string sCommMode
       {
           set { this.CommMode =value;}
           get { return this.CommMode;}
        }

       private int Baudrate;
       public int iBaudrate
       {
           set { this.Baudrate =value;}
           get { return this.Baudrate;}
        }

       private int PhyAddr;
       public int iPhyAddr
       {
           set { this.PhyAddr =value;}
           get { return this.PhyAddr;}
        }

       private string EvalorNo;
       public string sEvalorNo
       {
           set { this.EvalorNo =value;}
           get { return this.EvalorNo;}
        }

       private int TimeoutSec;
       public int iTimeoutSec
       {
           set { this.TimeoutSec =value;}
           get { return this.TimeoutSec;}
        }

       private int UpdateFlag;
       public int iUpdateFlag
       {
           set { this.UpdateFlag =value;}
           get { return this.UpdateFlag;}
        }

       private DateTime UpdateTime;
       public DateTime dUpdateTime
       {
           set { this.UpdateTime =value;}
           get { return this.UpdateTime;}
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

