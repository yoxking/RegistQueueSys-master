using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class LaboratoryTable
  {
     public LaboratoryTable(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string LabLocNo;
       public string sLabLocNo
       {
           set { this.LabLocNo =value;}
           get { return this.LabLocNo;}
        }

       private string LabLocName;
       public string sLabLocName
       {
           set { this.LabLocName =value;}
           get { return this.LabLocName;}
        }

       private string LabNo;
       public string sLabNo
       {
           set { this.LabNo =value;}
           get { return this.LabNo;}
        }

       private string ElecLocation;
       public string sElecLocation
       {
           set { this.ElecLocation =value;}
           get { return this.ElecLocation;}
        }

       private int State;
       public int iState
       {
           set { this.State =value;}
           get { return this.State;}
        }

       private string Reason;
       public string sReason
       {
           set { this.Reason =value;}
           get { return this.Reason;}
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

