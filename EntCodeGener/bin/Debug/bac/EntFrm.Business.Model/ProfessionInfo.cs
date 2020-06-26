using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class ProfessionInfo
  {
     public ProfessionInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string ProfNo;
       public string sProfNo
       {
           set { this.ProfNo =value;}
           get { return this.ProfNo;}
        }

       private string ProfName;
       public string sProfName
       {
           set { this.ProfName =value;}
           get { return this.ProfName;}
        }

       private string DepNo;
       public string sDepNo
       {
           set { this.DepNo =value;}
           get { return this.DepNo;}
        }

       private string Abstract;
       public string sAbstract
       {
           set { this.Abstract =value;}
           get { return this.Abstract;}
        }

       private int State;
       public int iState
       {
           set { this.State =value;}
           get { return this.State;}
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

