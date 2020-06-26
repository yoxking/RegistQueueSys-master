using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class DeviceInfo
  {
     public DeviceInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string DevNo;
       public string sDevNo
       {
           set { this.DevNo =value;}
           get { return this.DevNo;}
        }

       private string DevTypeNo;
       public string sDevTypeNo
       {
           set { this.DevTypeNo =value;}
           get { return this.DevTypeNo;}
        }

       private string DevClassNo;
       public string sDevClassNo
       {
           set { this.DevClassNo =value;}
           get { return this.DevClassNo;}
        }

       private string ProviderNo;
       public string sProviderNo
       {
           set { this.ProviderNo =value;}
           get { return this.ProviderNo;}
        }

       private string DevUniteNo;
       public string sDevUniteNo
       {
           set { this.DevUniteNo =value;}
           get { return this.DevUniteNo;}
        }

       private string DevName;
       public string sDevName
       {
           set { this.DevName =value;}
           get { return this.DevName;}
        }

       private string DevBarcode;
       public string sDevBarcode
       {
           set { this.DevBarcode =value;}
           get { return this.DevBarcode;}
        }

       private string DevGuid;
       public string sDevGuid
       {
           set { this.DevGuid =value;}
           get { return this.DevGuid;}
        }

       private string DevSpec;
       public string sDevSpec
       {
           set { this.DevSpec =value;}
           get { return this.DevSpec;}
        }

       private double DevPrice;
       public double dDevPrice
       {
           set { this.DevPrice =value;}
           get { return this.DevPrice;}
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

