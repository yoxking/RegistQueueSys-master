using System;

namespace EntFrm.Business.Model
{
  public class DsResultFlows
  {
     public DsResultFlows(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string RFlowNo;
       public string sRFlowNo
       {
           set { this.RFlowNo =value;}
           get { return this.RFlowNo;}
        }

       private string ClassNo;
       public string sClassNo
       {
           set { this.ClassNo =value;}
           get { return this.ClassNo;}
        }

       private string RetCode;
       public string sRetCode
       {
           set { this.RetCode =value;}
           get { return this.RetCode;}
        }

       private string RetError;
       public string sRetError
       {
           set { this.RetError =value;}
           get { return this.RetError;}
        }

       private string IpAddress;
       public string sIpAddress
       {
           set { this.IpAddress =value;}
           get { return this.IpAddress;}
        }

       private string CmdCode;
       public string sCmdCode
       {
           set { this.CmdCode =value;}
           get { return this.CmdCode;}
        }

       private string CmdResult;
       public string sCmdResult
       {
           set { this.CmdResult =value;}
           get { return this.CmdResult;}
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

