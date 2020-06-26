using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class TermInfo
  {
     public TermInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string TermNo;
       public string sTermNo
       {
           set { this.TermNo =value;}
           get { return this.TermNo;}
        }

       private string TermName;
       public string sTermName
       {
           set { this.TermName =value;}
           get { return this.TermName;}
        }

       private DateTime BeginTime;
       public DateTime dBeginTime
       {
           set { this.BeginTime =value;}
           get { return this.BeginTime;}
        }

       private DateTime EndTime;
       public DateTime dEndTime
       {
           set { this.EndTime =value;}
           get { return this.EndTime;}
        }

       private int Week;
       public int iWeek
       {
           set { this.Week =value;}
           get { return this.Week;}
        }

       private int State;
       public int iState
       {
           set { this.State =value;}
           get { return this.State;}
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

