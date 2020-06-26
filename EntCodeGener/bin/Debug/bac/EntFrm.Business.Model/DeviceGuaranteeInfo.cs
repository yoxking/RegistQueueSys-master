using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class DeviceGuaranteeInfo
  {
     public DeviceGuaranteeInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string GuarntNo;
       public string sGuarntNo
       {
           set { this.GuarntNo =value;}
           get { return this.GuarntNo;}
        }

       private string DevNo;
       public string sDevNo
       {
           set { this.DevNo =value;}
           get { return this.DevNo;}
        }

       private string GuarntPerson;
       public string sGuarntPerson
       {
           set { this.GuarntPerson =value;}
           get { return this.GuarntPerson;}
        }

       private DateTime GuarntTime;
       public DateTime dGuarntTime
       {
           set { this.GuarntTime =value;}
           get { return this.GuarntTime;}
        }

       private string GContent;
       public string sGContent
       {
           set { this.GContent =value;}
           get { return this.GContent;}
        }

       private string Location;
       public string sLocation
       {
           set { this.Location =value;}
           get { return this.Location;}
        }

       private string Charge;
       public string sCharge
       {
           set { this.Charge =value;}
           get { return this.Charge;}
        }

       private int GState;
       public int iGState
       {
           set { this.GState =value;}
           get { return this.GState;}
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

