using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class DeviceIODetail
  {
     public DeviceIODetail(){ }

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

       private string DevListNo;
       public string sDevListNo
       {
           set { this.DevListNo =value;}
           get { return this.DevListNo;}
        }

       private string DevListType;
       public string sDevListType
       {
           set { this.DevListType =value;}
           get { return this.DevListType;}
        }

       private string DevNo;
       public string sDevNo
       {
           set { this.DevNo =value;}
           get { return this.DevNo;}
        }

       private int DevNum;
       public int iDevNum
       {
           set { this.DevNum =value;}
           get { return this.DevNum;}
        }

       private string DevUnitNo;
       public string sDevUnitNo
       {
           set { this.DevUnitNo =value;}
           get { return this.DevUnitNo;}
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

