using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class TeachResourceInfo
  {
     public TeachResourceInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string TchResNO;
       public string sTchResNO
       {
           set { this.TchResNO =value;}
           get { return this.TchResNO;}
        }

       private string TchResTitle;
       public string sTchResTitle
       {
           set { this.TchResTitle =value;}
           get { return this.TchResTitle;}
        }

       private string TchResTyNo;
       public string sTchResTyNo
       {
           set { this.TchResTyNo =value;}
           get { return this.TchResTyNo;}
        }

       private string TchResText;
       public string sTchResText
       {
           set { this.TchResText =value;}
           get { return this.TchResText;}
        }

       private string TchResAbstract;
       public string sTchResAbstract
       {
           set { this.TchResAbstract =value;}
           get { return this.TchResAbstract;}
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

       private int VisitTimes;
       public int iVisitTimes
       {
           set { this.VisitTimes =value;}
           get { return this.VisitTimes;}
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

