using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class OutlineInfo
  {
     public OutlineInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string OutNo;
       public string sOutNo
       {
           set { this.OutNo =value;}
           get { return this.OutNo;}
        }

       private string OutTitle;
       public string sOutTitle
       {
           set { this.OutTitle =value;}
           get { return this.OutTitle;}
        }

       private string OutTypeNo;
       public string sOutTypeNo
       {
           set { this.OutTypeNo =value;}
           get { return this.OutTypeNo;}
        }

       private string OutText;
       public string sOutText
       {
           set { this.OutText =value;}
           get { return this.OutText;}
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

