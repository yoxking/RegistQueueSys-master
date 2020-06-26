using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class LabProcessInfo
  {
     public LabProcessInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string PsNo;
       public string sPsNo
       {
           set { this.PsNo =value;}
           get { return this.PsNo;}
        }

       private string LabOpenNo;
       public string sLabOpenNo
       {
           set { this.LabOpenNo =value;}
           get { return this.LabOpenNo;}
        }

       private string RuNo;
       public string sRuNo
       {
           set { this.RuNo =value;}
           get { return this.RuNo;}
        }

       private string PsName;
       public string sPsName
       {
           set { this.PsName =value;}
           get { return this.PsName;}
        }

       private string PsContent;
       public string sPsContent
       {
           set { this.PsContent =value;}
           get { return this.PsContent;}
        }

       private double PsScore;
       public double dPsScore
       {
           set { this.PsScore =value;}
           get { return this.PsScore;}
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

