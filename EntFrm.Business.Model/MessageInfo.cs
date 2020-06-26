using System;

namespace EntFrm.Business.Model
{
  public class MessageInfo
  {
     public MessageInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string MessNo;
       public string sMessNo
       {
           set { this.MessNo =value;}
           get { return this.MessNo;}
        }

       private string MSender;
       public string sMSender
       {
           set { this.MSender =value;}
           get { return this.MSender;}
        }

       private string MReceiver;
       public string sMReceiver
       {
           set { this.MReceiver =value;}
           get { return this.MReceiver;}
        }

       private int MType;
       public int iMType
       {
           set { this.MType =value;}
           get { return this.MType;}
        }

       private string MTitle;
       public string sMTitle
       {
           set { this.MTitle =value;}
           get { return this.MTitle;}
        }

       private string MContent;
       public string sMContent
       {
           set { this.MContent =value;}
           get { return this.MContent;}
        }

       private string AttachFile;
       public string sAttachFile
       {
           set { this.AttachFile =value;}
           get { return this.AttachFile;}
        }

       private DateTime SendDate;
       public DateTime dSendDate
       {
           set { this.SendDate =value;}
           get { return this.SendDate;}
        }

       private DateTime ReceiveDate;
       public DateTime dReceiveDate
       {
           set { this.ReceiveDate =value;}
           get { return this.ReceiveDate;}
        }

       private int ReadState;
       public int iReadState
       {
           set { this.ReadState =value;}
           get { return this.ReadState;}
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

