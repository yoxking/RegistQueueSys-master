using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class DeviceFailedInfo
  {
     public DeviceFailedInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string DevFldNo;
       public string sDevFldNo
       {
           set { this.DevFldNo =value;}
           get { return this.DevFldNo;}
        }

       private string DevFldName;
       public string sDevFldName
       {
           set { this.DevFldName =value;}
           get { return this.DevFldName;}
        }

       private string Maker;
       public string sMaker
       {
           set { this.Maker =value;}
           get { return this.Maker;}
        }

       private DateTime FailedTime;
       public DateTime dFailedTime
       {
           set { this.FailedTime =value;}
           get { return this.FailedTime;}
        }

       private string FailedReason;
       public string sFailedReason
       {
           set { this.FailedReason =value;}
           get { return this.FailedReason;}
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

