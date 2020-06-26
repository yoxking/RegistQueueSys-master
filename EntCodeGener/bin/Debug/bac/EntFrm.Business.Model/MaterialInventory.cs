using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class MaterialInventory
  {
     public MaterialInventory(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string MatInvNo;
       public string sMatInvNo
       {
           set { this.MatInvNo =value;}
           get { return this.MatInvNo;}
        }

       private string MaterialNo;
       public string sMaterialNo
       {
           set { this.MaterialNo =value;}
           get { return this.MaterialNo;}
        }

       private string WHouseNo;
       public string sWHouseNo
       {
           set { this.WHouseNo =value;}
           get { return this.WHouseNo;}
        }

       private int InvNum;
       public int iInvNum
       {
           set { this.InvNum =value;}
           get { return this.InvNum;}
        }

       private string InvUnitNo;
       public string sInvUnitNo
       {
           set { this.InvUnitNo =value;}
           get { return this.InvUnitNo;}
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

