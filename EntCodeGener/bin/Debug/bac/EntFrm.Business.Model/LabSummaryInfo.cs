using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class LabSummaryInfo
  {
     public LabSummaryInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string SmNo;
       public string sSmNo
       {
           set { this.SmNo =value;}
           get { return this.SmNo;}
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

       private string SmName;
       public string sSmName
       {
           set { this.SmName =value;}
           get { return this.SmName;}
        }

       private string SmContent;
       public string sSmContent
       {
           set { this.SmContent =value;}
           get { return this.SmContent;}
        }

       private double SmScore;
       public double dSmScore
       {
           set { this.SmScore =value;}
           get { return this.SmScore;}
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

