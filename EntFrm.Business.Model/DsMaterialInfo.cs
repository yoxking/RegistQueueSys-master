using System;

namespace EntFrm.Business.Model
{
  public class DsMaterialInfo
  {
     public DsMaterialInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string MatNo;
       public string sMatNo
       {
           set { this.MatNo =value;}
           get { return this.MatNo;}
        }

       private string MatName;
       public string sMatName
       {
           set { this.MatName =value;}
           get { return this.MatName;}
        }

       private string MClassNo;
       public string sMClassNo
       {
           set { this.MClassNo =value;}
           get { return this.MClassNo;}
        }

       private string MatPoster;
       public string sMatPoster
       {
           set { this.MatPoster =value;}
           get { return this.MatPoster;}
        }

       private string MatType;
       public string sMatType
       {
           set { this.MatType =value;}
           get { return this.MatType;}
        }

       private string FilePath;
       public string sFilePath
       {
           set { this.FilePath =value;}
           get { return this.FilePath;}
        }

       private int FileSize;
       public int iFileSize
       {
           set { this.FileSize =value;}
           get { return this.FileSize;}
        }

       private string Resolution;
       public string sResolution
       {
           set { this.Resolution =value;}
           get { return this.Resolution;}
        }

       private double PlayDuration;
       public double dPlayDuration
       {
           set { this.PlayDuration =value;}
           get { return this.PlayDuration;}
        }

       private int IsTemplet;
       public int iIsTemplet
       {
           set { this.IsTemplet =value;}
           get { return this.IsTemplet;}
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

