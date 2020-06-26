using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class MaterialIOMaster
  {
     public MaterialIOMaster(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string MatListNo;
       public string sMatListNo
       {
           set { this.MatListNo =value;}
           get { return this.MatListNo;}
        }

       private string MatListType;
       public string sMatListType
       {
           set { this.MatListType =value;}
           get { return this.MatListType;}
        }

       private string ProviderNo;
       public string sProviderNo
       {
           set { this.ProviderNo =value;}
           get { return this.ProviderNo;}
        }

       private DateTime MatIOTime;
       public DateTime dMatIOTime
       {
           set { this.MatIOTime =value;}
           get { return this.MatIOTime;}
        }

       private int MatIONum;
       public int iMatIONum
       {
           set { this.MatIONum =value;}
           get { return this.MatIONum;}
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

