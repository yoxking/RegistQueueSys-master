using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class LabOpeningInfo
  {
     public LabOpeningInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string LabOpenNo;
       public string sLabOpenNo
       {
           set { this.LabOpenNo =value;}
           get { return this.LabOpenNo;}
        }

       private string LabLessonNo;
       public string sLabLessonNo
       {
           set { this.LabLessonNo =value;}
           get { return this.LabLessonNo;}
        }

       private string RuNo;
       public string sRuNo
       {
           set { this.RuNo =value;}
           get { return this.RuNo;}
        }

       private int OpenState;
       public int iOpenState
       {
           set { this.OpenState =value;}
           get { return this.OpenState;}
        }

       private string BookTime;
       public string sBookTime
       {
           set { this.BookTime =value;}
           get { return this.BookTime;}
        }

       private DateTime OpenTime;
       public DateTime dOpenTime
       {
           set { this.OpenTime =value;}
           get { return this.OpenTime;}
        }

       private DateTime CloseTime;
       public DateTime dCloseTime
       {
           set { this.CloseTime =value;}
           get { return this.CloseTime;}
        }

       private string IsAudit;
       public string sIsAudit
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

       private DateTime AuditTime;
       public DateTime dAuditTime
       {
           set { this.AuditTime =value;}
           get { return this.AuditTime;}
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

