using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class DeviceIOMaster
  {
     public DeviceIOMaster(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string DevListNo;
       public string sDevListNo
       {
           set { this.DevListNo =value;}
           get { return this.DevListNo;}
        }

       private string DevListType;
       public string sDevListType
       {
           set { this.DevListType =value;}
           get { return this.DevListType;}
        }

       private string ProviderNo;
       public string sProviderNo
       {
           set { this.ProviderNo =value;}
           get { return this.ProviderNo;}
        }

       private DateTime DevIOTime;
       public DateTime dDevIOTime
       {
           set { this.DevIOTime =value;}
           get { return this.DevIOTime;}
        }

       private int DevIONum;
       public int iDevIONum
       {
           set { this.DevIONum =value;}
           get { return this.DevIONum;}
        }

       private string Auditor;
       public string sAuditor
       {
           set { this.Auditor =value;}
           get { return this.Auditor;}
        }

       private string Approver;
       public string sApprover
       {
           set { this.Approver =value;}
           get { return this.Approver;}
        }

       private string Deliverer;
       public string sDeliverer
       {
           set { this.Deliverer =value;}
           get { return this.Deliverer;}
        }

       private string Recipient;
       public string sRecipient
       {
           set { this.Recipient =value;}
           get { return this.Recipient;}
        }

       private string Purposer;
       public string sPurposer
       {
           set { this.Purposer =value;}
           get { return this.Purposer;}
        }

       private string Sender;
       public string sSender
       {
           set { this.Sender =value;}
           get { return this.Sender;}
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

