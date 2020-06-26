using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class DeviceClassInfo
  {
     public DeviceClassInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string DevClassNo;
       public string sDevClassNo
       {
           set { this.DevClassNo =value;}
           get { return this.DevClassNo;}
        }

       private string DevTypeNo;
       public string sDevTypeNo
       {
           set { this.DevTypeNo =value;}
           get { return this.DevTypeNo;}
        }

       private string DevClassName;
       public string sDevClassName
       {
           set { this.DevClassName =value;}
           get { return this.DevClassName;}
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

       private string DevUntNo;
       public string sDevUntNo
       {
           set { this.DevUntNo =value;}
           get { return this.DevUntNo;}
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

