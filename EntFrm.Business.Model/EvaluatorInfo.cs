using System;

namespace EntFrm.Business.Model
{
  public class EvaluatorInfo
  {
     public EvaluatorInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string EvalorNo;
       public string sEvalorNo
       {
           set { this.EvalorNo =value;}
           get { return this.EvalorNo;}
        }

       private string EvaVCode;
       public string sEvaVCode
       {
           set { this.EvaVCode =value;}
           get { return this.EvaVCode;}
        }

       private DateTime RegistDate;
       public DateTime dRegistDate
       {
           set { this.RegistDate =value;}
           get { return this.RegistDate;}
        }

       private string EvaIpAddr;
       public string sEvaIpAddr
       {
           set { this.EvaIpAddr =value;}
           get { return this.EvaIpAddr;}
        }

       private string EvaLcPort;
       public string sEvaLcPort
       {
           set { this.EvaLcPort =value;}
           get { return this.EvaLcPort;}
        }

       private string BranchNo;
       public string sBranchNo
       {
           set { this.BranchNo =value;}
           get { return this.BranchNo;}
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

