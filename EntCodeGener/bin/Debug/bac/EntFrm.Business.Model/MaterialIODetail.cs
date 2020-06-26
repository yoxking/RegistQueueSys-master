using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class MaterialIODetail
  {
     public MaterialIODetail(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string IONo;
       public string sIONo
       {
           set { this.IONo =value;}
           get { return this.IONo;}
        }

       private string MatListNo;
       public string sMatListNo
       {
           set { this.MatListNo =value;}
           get { return this.MatListNo;}
        }

       private string MatListType;
       public string sMatListType
       {
           set { this.MatListType =value;}
           get { return this.MatListType;}
        }

       private string MatNo;
       public string sMatNo
       {
           set { this.MatNo =value;}
           get { return this.MatNo;}
        }

       private int MatNum;
       public int iMatNum
       {
           set { this.MatNum =value;}
           get { return this.MatNum;}
        }

       private string MatUnitNo;
       public string sMatUnitNo
       {
           set { this.MatUnitNo =value;}
           get { return this.MatUnitNo;}
        }

       private string WHouseNo;
       public string sWHouseNo
       {
           set { this.WHouseNo =value;}
           get { return this.WHouseNo;}
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

