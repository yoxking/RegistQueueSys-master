using System;

namespace EntFrm.Business.Model
{
  public class DsQuartzInfo
  {
     public DsQuartzInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string QuartzNo;
       public string sQuartzNo
       {
           set { this.QuartzNo =value;}
           get { return this.QuartzNo;}
        }

       private string QuartzName;
       public string sQuartzName
       {
           set { this.QuartzName =value;}
           get { return this.QuartzName;}
        }

       private string CornExp;
       public string sCornExp
       {
           set { this.CornExp =value;}
           get { return this.CornExp;}
        }

       private string JobTask;
       public string sJobTask
       {
           set { this.JobTask =value;}
           get { return this.JobTask;}
        }

       private int WeekDay;
       public int iWeekDay
       {
           set { this.WeekDay =value;}
           get { return this.WeekDay;}
        }

       private string PlayerNos;
       public string sPlayerNos
       {
           set { this.PlayerNos =value;}
           get { return this.PlayerNos;}
        }

       private string Summary;
       public string sSummary
       {
           set { this.Summary =value;}
           get { return this.Summary;}
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

