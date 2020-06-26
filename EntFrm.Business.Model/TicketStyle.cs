using System;

namespace EntFrm.Business.Model
{
  public class TicketStyle
  {
     public TicketStyle(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string StyleNo;
       public string sStyleNo
       {
           set { this.StyleNo =value;}
           get { return this.StyleNo;}
        }

       private string StyleName;
       public string sStyleName
       {
           set { this.StyleName =value;}
           get { return this.StyleName;}
        }

       private int IsTemplet;
       public int iIsTemplet
       {
           set { this.IsTemplet =value;}
           get { return this.IsTemplet;}
        }

       private string TicketFormat;
       public string sTicketFormat
       {
           set { this.TicketFormat =value;}
           get { return this.TicketFormat;}
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

