using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class MaterialClassInfo
  {
     public MaterialClassInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string MatClassNo;
       public string sMatClassNo
       {
           set { this.MatClassNo =value;}
           get { return this.MatClassNo;}
        }

       private string MatTypeNo;
       public string sMatTypeNo
       {
           set { this.MatTypeNo =value;}
           get { return this.MatTypeNo;}
        }

       private string MatClassName;
       public string sMatClassName
       {
           set { this.MatClassName =value;}
           get { return this.MatClassName;}
        }

       private string MatClassRename;
       public string sMatClassRename
       {
           set { this.MatClassRename =value;}
           get { return this.MatClassRename;}
        }

       private DateTime UseTime;
       public DateTime dUseTime
       {
           set { this.UseTime =value;}
           get { return this.UseTime;}
        }

       private string ModelNo;
       public string sModelNo
       {
           set { this.ModelNo =value;}
           get { return this.ModelNo;}
        }

       private string MatUntNo;
       public string sMatUntNo
       {
           set { this.MatUntNo =value;}
           get { return this.MatUntNo;}
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

