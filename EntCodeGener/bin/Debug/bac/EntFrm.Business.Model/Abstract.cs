using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class Abstract
  {
     public Abstract(){ }

       private int id;
       public int iid
       {
           set { this.id =value;}
           get { return this.id;}
        }

       private string AbsNo;
       public string sAbsNo
       {
           set { this.AbsNo =value;}
           get { return this.AbsNo;}
        }

       private string AbsName;
       public string sAbsName
       {
           set { this.AbsName =value;}
           get { return this.AbsName;}
        }

       private string CateNo;
       public string sCateNo
       {
           set { this.CateNo =value;}
           get { return this.CateNo;}
        }

       private string AbsPic;
       public string sAbsPic
       {
           set { this.AbsPic =value;}
           get { return this.AbsPic;}
        }

       private string AbsAbstract;
       public string sAbsAbstract
       {
           set { this.AbsAbstract =value;}
           get { return this.AbsAbstract;}
        }

       private string AbsContent;
       public string sAbsContent
       {
           set { this.AbsContent =value;}
           get { return this.AbsContent;}
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

