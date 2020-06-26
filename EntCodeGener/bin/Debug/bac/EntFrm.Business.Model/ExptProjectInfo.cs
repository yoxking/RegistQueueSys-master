using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class ExptProjectInfo
  {
     public ExptProjectInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string ExptProNo;
       public string sExptProNo
       {
           set { this.ExptProNo =value;}
           get { return this.ExptProNo;}
        }

       private string ExptProName;
       public string sExptProName
       {
           set { this.ExptProName =value;}
           get { return this.ExptProName;}
        }

       private string ExptProType;
       public string sExptProType
       {
           set { this.ExptProType =value;}
           get { return this.ExptProType;}
        }

       private string ExptGoal;
       public string sExptGoal
       {
           set { this.ExptGoal =value;}
           get { return this.ExptGoal;}
        }

       private string ExptAbstract;
       public string sExptAbstract
       {
           set { this.ExptAbstract =value;}
           get { return this.ExptAbstract;}
        }

       private string ExptContent;
       public string sExptContent
       {
           set { this.ExptContent =value;}
           get { return this.ExptContent;}
        }

       private int ExptState;
       public int iExptState
       {
           set { this.ExptState =value;}
           get { return this.ExptState;}
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

