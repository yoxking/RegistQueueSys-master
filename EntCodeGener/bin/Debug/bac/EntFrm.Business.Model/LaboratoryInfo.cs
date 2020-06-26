using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class LaboratoryInfo
  {
     public LaboratoryInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string LabNo;
       public string sLabNo
       {
           set { this.LabNo =value;}
           get { return this.LabNo;}
        }

       private string LabName;
       public string sLabName
       {
           set { this.LabName =value;}
           get { return this.LabName;}
        }

       private string DepNo;
       public string sDepNo
       {
           set { this.DepNo =value;}
           get { return this.DepNo;}
        }

       private int People;
       public int iPeople
       {
           set { this.People =value;}
           get { return this.People;}
        }

       private int Area;
       public int iArea
       {
           set { this.Area =value;}
           get { return this.Area;}
        }

       private string GuardNo;
       public string sGuardNo
       {
           set { this.GuardNo =value;}
           get { return this.GuardNo;}
        }

       private string CameraNo;
       public string sCameraNo
       {
           set { this.CameraNo =value;}
           get { return this.CameraNo;}
        }

       private string Comment;
       public string sComment
       {
           set { this.Comment =value;}
           get { return this.Comment;}
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

