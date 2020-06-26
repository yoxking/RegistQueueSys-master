using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class DeviceRepairInfo
  {
     public DeviceRepairInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string RepairNo;
       public string sRepairNo
       {
           set { this.RepairNo =value;}
           get { return this.RepairNo;}
        }

       private string DevNo;
       public string sDevNo
       {
           set { this.DevNo =value;}
           get { return this.DevNo;}
        }

       private DateTime RepairTime;
       public DateTime dRepairTime
       {
           set { this.RepairTime =value;}
           get { return this.RepairTime;}
        }

       private int RPrice;
       public int iRPrice
       {
           set { this.RPrice =value;}
           get { return this.RPrice;}
        }

       private string RContent;
       public string sRContent
       {
           set { this.RContent =value;}
           get { return this.RContent;}
        }

       private int RState;
       public int iRState
       {
           set { this.RState =value;}
           get { return this.RState;}
        }

       private string Charger;
       public string sCharger
       {
           set { this.Charger =value;}
           get { return this.Charger;}
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

