using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class TeachResourceDetail
  {
     public TeachResourceDetail(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string RDetNo;
       public string sRDetNo
       {
           set { this.RDetNo =value;}
           get { return this.RDetNo;}
        }

       private string TchResNO;
       public string sTchResNO
       {
           set { this.TchResNO =value;}
           get { return this.TchResNO;}
        }

       private string ResTitle;
       public string sResTitle
       {
           set { this.ResTitle =value;}
           get { return this.ResTitle;}
        }

       private string ResAbstract;
       public string sResAbstract
       {
           set { this.ResAbstract =value;}
           get { return this.ResAbstract;}
        }

       private string ResTypeNo;
       public string sResTypeNo
       {
           set { this.ResTypeNo =value;}
           get { return this.ResTypeNo;}
        }

       private string ResPath;
       public string sResPath
       {
           set { this.ResPath =value;}
           get { return this.ResPath;}
        }

       private double ResSize;
       public double dResSize
       {
           set { this.ResSize =value;}
           get { return this.ResSize;}
        }

       private int IsDownload;
       public int iIsDownload
       {
           set { this.IsDownload =value;}
           get { return this.IsDownload;}
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

