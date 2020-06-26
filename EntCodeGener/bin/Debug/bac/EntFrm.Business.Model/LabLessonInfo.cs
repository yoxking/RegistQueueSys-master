using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class LabLessonInfo
  {
     public LabLessonInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string LabLsnNo;
       public string sLabLsnNo
       {
           set { this.LabLsnNo =value;}
           get { return this.LabLsnNo;}
        }

       private string LabNo;
       public string sLabNo
       {
           set { this.LabNo =value;}
           get { return this.LabNo;}
        }

       private string LessonNo;
       public string sLessonNo
       {
           set { this.LessonNo =value;}
           get { return this.LessonNo;}
        }

       private string TermNo;
       public string sTermNo
       {
           set { this.TermNo =value;}
           get { return this.TermNo;}
        }

       private int WeekNo;
       public int iWeekNo
       {
           set { this.WeekNo =value;}
           get { return this.WeekNo;}
        }

       private string TeacherNo;
       public string sTeacherNo
       {
           set { this.TeacherNo =value;}
           get { return this.TeacherNo;}
        }

       private string ClassNo;
       public string sClassNo
       {
           set { this.ClassNo =value;}
           get { return this.ClassNo;}
        }

       private int LabType;
       public int iLabType
       {
           set { this.LabType =value;}
           get { return this.LabType;}
        }

       private int LabState;
       public int iLabState
       {
           set { this.LabState =value;}
           get { return this.LabState;}
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

