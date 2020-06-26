using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class LabCheckInInfo
  {
     public LabCheckInInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string ChkNo;
       public string sChkNo
       {
           set { this.ChkNo =value;}
           get { return this.ChkNo;}
        }

       private string StuNo;
       public string sStuNo
       {
           set { this.StuNo =value;}
           get { return this.StuNo;}
        }

       private DateTime CkInTime;
       public DateTime dCkInTime
       {
           set { this.CkInTime =value;}
           get { return this.CkInTime;}
        }

       private int IsCkIn;
       public int iIsCkIn
       {
           set { this.IsCkIn =value;}
           get { return this.IsCkIn;}
        }

       private string TermNo;
       public string sTermNo
       {
           set { this.TermNo =value;}
           get { return this.TermNo;}
        }

       private string CourseNo;
       public string sCourseNo
       {
           set { this.CourseNo =value;}
           get { return this.CourseNo;}
        }

       private string TchNo;
       public string sTchNo
       {
           set { this.TchNo =value;}
           get { return this.TchNo;}
        }

       private int Week;
       public int iWeek
       {
           set { this.Week =value;}
           get { return this.Week;}
        }

       private string WeekDay;
       public string sWeekDay
       {
           set { this.WeekDay =value;}
           get { return this.WeekDay;}
        }

       private int Lesson;
       public int iLesson
       {
           set { this.Lesson =value;}
           get { return this.Lesson;}
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

