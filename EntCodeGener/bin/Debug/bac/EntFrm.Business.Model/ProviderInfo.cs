using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class ProviderInfo
  {
     public ProviderInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string PvdNo;
       public string sPvdNo
       {
           set { this.PvdNo =value;}
           get { return this.PvdNo;}
        }

       private string PvdName;
       public string sPvdName
       {
           set { this.PvdName =value;}
           get { return this.PvdName;}
        }

       private string Abstract;
       public string sAbstract
       {
           set { this.Abstract =value;}
           get { return this.Abstract;}
        }

       private string Email;
       public string sEmail
       {
           set { this.Email =value;}
           get { return this.Email;}
        }

       private string Phone;
       public string sPhone
       {
           set { this.Phone =value;}
           get { return this.Phone;}
        }

       private string Contact;
       public string sContact
       {
           set { this.Contact =value;}
           get { return this.Contact;}
        }

       private string Address;
       public string sAddress
       {
           set { this.Address =value;}
           get { return this.Address;}
        }

       private string Postcode;
       public string sPostcode
       {
           set { this.Postcode =value;}
           get { return this.Postcode;}
        }

       private string Fax;
       public string sFax
       {
           set { this.Fax =value;}
           get { return this.Fax;}
        }

       private string BankName;
       public string sBankName
       {
           set { this.BankName =value;}
           get { return this.BankName;}
        }

       private string BankAccount;
       public string sBankAccount
       {
           set { this.BankAccount =value;}
           get { return this.BankAccount;}
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

