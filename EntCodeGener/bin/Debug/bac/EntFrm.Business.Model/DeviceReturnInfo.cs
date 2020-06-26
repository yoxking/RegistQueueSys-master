using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class DeviceReturnInfo
  {
     public DeviceReturnInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string BroNo;
       public string sBroNo
       {
           set { this.BroNo =value;}
           get { return this.BroNo;}
        }

       private string BroPerson;
       public string sBroPerson
       {
           set { this.BroPerson =value;}
           get { return this.BroPerson;}
        }

       private DateTime BroTime;
       public DateTime dBroTime
       {
           set { this.BroTime =value;}
           get { return this.BroTime;}
        }

       private DateTime PreReTime;
       public DateTime dPreReTime
       {
           set { this.PreReTime =value;}
           get { return this.PreReTime;}
        }

       private string Purpose;
       public string sPurpose
       {
           set { this.Purpose =value;}
           get { return this.Purpose;}
        }

       private int BroAudit;
       public int iBroAudit
       {
           set { this.BroAudit =value;}
           get { return this.BroAudit;}
        }

       private string BroAuditor;
       public string sBroAuditor
       {
           set { this.BroAuditor =value;}
           get { return this.BroAuditor;}
        }

       private DateTime RealReTime;
       public DateTime dRealReTime
       {
           set { this.RealReTime =value;}
           get { return this.RealReTime;}
        }

       private string RePerson;
       public string sRePerson
       {
           set { this.RePerson =value;}
           get { return this.RePerson;}
        }

       private int ReAudit;
       public int iReAudit
       {
           set { this.ReAudit =value;}
           get { return this.ReAudit;}
        }

       private string ReAuditor;
       public string sReAuditor
       {
           set { this.ReAuditor =value;}
           get { return this.ReAuditor;}
        }

       private int IsReturn;
       public int iIsReturn
       {
           set { this.IsReturn =value;}
           get { return this.IsReturn;}
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

