using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class LessonTimesInfo
  {
     public LessonTimesInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string LessonNo;
       public string sLessonNo
       {
           set { this.LessonNo =value;}
           get { return this.LessonNo;}
        }

       private string LessonName;
       public string sLessonName
       {
           set { this.LessonName =value;}
           get { return this.LessonName;}
        }

       private string ProfNo;
       public string sProfNo
       {
           set { this.ProfNo =value;}
           get { return this.ProfNo;}
        }

       private int LessonNum;
       public int iLessonNum
       {
           set { this.LessonNum =value;}
           get { return this.LessonNum;}
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

       private int TheoryTime;
       public int iTheoryTime
       {
           set { this.TheoryTime =value;}
           get { return this.TheoryTime;}
        }

       private int ExperimentTime;
       public int iExperimentTime
       {
           set { this.ExperimentTime =value;}
           get { return this.ExperimentTime;}
        }

       private string LessonType;
       public string sLessonType
       {
           set { this.LessonType =value;}
           get { return this.LessonType;}
        }

       private string LsnAbstract;
       public string sLsnAbstract
       {
           set { this.LsnAbstract =value;}
           get { return this.LsnAbstract;}
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

