using System;

namespace EntFrm.Business.Model
{
  public class DsHrtbeatFlows
  {
     public DsHrtbeatFlows(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string BFlowNo;
       public string sBFlowNo
       {
           set { this.BFlowNo =value;}
           get { return this.BFlowNo;}
        }

       private string PlayerNo;
       public string sPlayerNo
       {
           set { this.PlayerNo =value;}
           get { return this.PlayerNo;}
        }

       private string PlayerCode;
       public string sPlayerCode
       {
           set { this.PlayerCode =value;}
           get { return this.PlayerCode;}
        }

       private string IpAddress;
       public string sIpAddress
       {
           set { this.IpAddress =value;}
           get { return this.IpAddress;}
        }

       private int LocalPort;
       public int iLocalPort
       {
           set { this.LocalPort =value;}
           get { return this.LocalPort;}
        }

       private DateTime RegistDate;
       public DateTime dRegistDate
       {
           set { this.RegistDate =value;}
           get { return this.RegistDate;}
        }

       private int CheckState;
       public int iCheckState
       {
           set { this.CheckState =value;}
           get { return this.CheckState;}
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

