using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class MaterialInfo
  {
     public MaterialInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string MatNo;
       public string sMatNo
       {
           set { this.MatNo =value;}
           get { return this.MatNo;}
        }

       private string MatTypeNo;
       public string sMatTypeNo
       {
           set { this.MatTypeNo =value;}
           get { return this.MatTypeNo;}
        }

       private string MatClassNo;
       public string sMatClassNo
       {
           set { this.MatClassNo =value;}
           get { return this.MatClassNo;}
        }

       private string ProviderNo;
       public string sProviderNo
       {
           set { this.ProviderNo =value;}
           get { return this.ProviderNo;}
        }

       private string MatUniteNo;
       public string sMatUniteNo
       {
           set { this.MatUniteNo =value;}
           get { return this.MatUniteNo;}
        }

       private string MatName;
       public string sMatName
       {
           set { this.MatName =value;}
           get { return this.MatName;}
        }

       private string MatBarcode;
       public string sMatBarcode
       {
           set { this.MatBarcode =value;}
           get { return this.MatBarcode;}
        }

       private string MatGuid;
       public string sMatGuid
       {
           set { this.MatGuid =value;}
           get { return this.MatGuid;}
        }

       private string MatSpec;
       public string sMatSpec
       {
           set { this.MatSpec =value;}
           get { return this.MatSpec;}
        }

       private double MatPrice;
       public double dMatPrice
       {
           set { this.MatPrice =value;}
           get { return this.MatPrice;}
        }

       private int MiniLimit;
       public int iMiniLimit
       {
           set { this.MiniLimit =value;}
           get { return this.MiniLimit;}
        }

       private int MaxiLimit;
       public int iMaxiLimit
       {
           set { this.MaxiLimit =value;}
           get { return this.MaxiLimit;}
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

