using System;

namespace EntFrm.Business.Model
{
  public class ContentInfo
  {
     public ContentInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string ContNo;
       public string sContNo
       {
           set { this.ContNo =value;}
           get { return this.ContNo;}
        }

       private string ClassNo;
       public string sClassNo
       {
           set { this.ClassNo =value;}
           get { return this.ClassNo;}
        }

       private string Title;
       public string sTitle
       {
           set { this.Title =value;}
           get { return this.Title;}
        }

       private string Author;
       public string sAuthor
       {
           set { this.Author =value;}
           get { return this.Author;}
        }

       private DateTime PublicDate;
       public DateTime dPublicDate
       {
           set { this.PublicDate =value;}
           get { return this.PublicDate;}
        }

       private string PostPicture;
       public string sPostPicture
       {
           set { this.PostPicture =value;}
           get { return this.PostPicture;}
        }

       private string Abstract;
       public string sAbstract
       {
           set { this.Abstract =value;}
           get { return this.Abstract;}
        }

       private string NContent;
       public string sNContent
       {
           set { this.NContent =value;}
           get { return this.NContent;}
        }

       private int IsTop;
       public int iIsTop
       {
           set { this.IsTop =value;}
           get { return this.IsTop;}
        }

       private int IsHot;
       public int iIsHot
       {
           set { this.IsHot =value;}
           get { return this.IsHot;}
        }

       private int IsPop;
       public int iIsPop
       {
           set { this.IsPop =value;}
           get { return this.IsPop;}
        }

       private int HitCount;
       public int iHitCount
       {
           set { this.HitCount =value;}
           get { return this.HitCount;}
        }

       private DateTime AuditDate;
       public DateTime dAuditDate
       {
           set { this.AuditDate =value;}
           get { return this.AuditDate;}
        }

       private string Auditor;
       public string sAuditor
       {
           set { this.Auditor =value;}
           get { return this.Auditor;}
        }

       private int CheckState;
       public int iCheckState
       {
           set { this.CheckState =value;}
           get { return this.CheckState;}
        }

       private string BranchNos;
       public string sBranchNos
       {
           set { this.BranchNos =value;}
           get { return this.BranchNos;}
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

