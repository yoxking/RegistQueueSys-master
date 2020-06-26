using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class UploadFileInfo
  {
     public UploadFileInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string FileNo;
       public string sFileNo
       {
           set { this.FileNo =value;}
           get { return this.FileNo;}
        }

       private string CntNo;
       public string sCntNo
       {
           set { this.CntNo =value;}
           get { return this.CntNo;}
        }

       private string FileName;
       public string sFileName
       {
           set { this.FileName =value;}
           get { return this.FileName;}
        }

       private string FileType;
       public string sFileType
       {
           set { this.FileType =value;}
           get { return this.FileType;}
        }

       private string FileURL;
       public string sFileURL
       {
           set { this.FileURL =value;}
           get { return this.FileURL;}
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

