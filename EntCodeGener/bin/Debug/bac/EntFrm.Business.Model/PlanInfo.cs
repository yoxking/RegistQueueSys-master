using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class PlanInfo
  {
     public PlanInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string PlanNo;
       public string sPlanNo
       {
           set { this.PlanNo =value;}
           get { return this.PlanNo;}
        }

       private string PlanTitle;
       public string sPlanTitle
       {
           set { this.PlanTitle =value;}
           get { return this.PlanTitle;}
        }

       private string PlanTypeNo;
       public string sPlanTypeNo
       {
           set { this.PlanTypeNo =value;}
           get { return this.PlanTypeNo;}
        }

       private string PlanText;
       public string sPlanText
       {
           set { this.PlanText =value;}
           get { return this.PlanText;}
        }

       private int IsAudit;
       public int iIsAudit
       {
           set { this.IsAudit =value;}
           get { return this.IsAudit;}
        }

       private string Auditor;
       public string sAuditor
       {
           set { this.Auditor =value;}
           get { return this.Auditor;}
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
