using System;

namespace EntFrm.Business.Model
{
  public class SUsersInfo
  {
     public SUsersInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string SuNo;
       public string sSuNo
       {
           set { this.SuNo =value;}
           get { return this.SuNo;}
        }

       private string LoginID;
       public string sLoginID
       {
           set { this.LoginID =value;}
           get { return this.LoginID;}
        }

       private string Password;
       public string sPassword
       {
           set { this.Password =value;}
           get { return this.Password;}
        }

       private string DeptNo;
       public string sDeptNo
       {
           set { this.DeptNo =value;}
           get { return this.DeptNo;}
        }

       private string TrueName;
       public string sTrueName
       {
           set { this.TrueName =value;}
           get { return this.TrueName;}
        }

       private int Sex;
       public int iSex
       {
           set { this.Sex =value;}
           get { return this.Sex;}
        }

       private int UIDType;
       public int iUIDType
       {
           set { this.UIDType =value;}
           get { return this.UIDType;}
        }

       private string UIDNo;
       public string sUIDNo
       {
           set { this.UIDNo =value;}
           get { return this.UIDNo;}
        }

       private string EMail;
       public string sEMail
       {
           set { this.EMail =value;}
           get { return this.EMail;}
        }

       private string Telphone;
       public string sTelphone
       {
           set { this.Telphone =value;}
           get { return this.Telphone;}
        }

       private int LockState;
       public int iLockState
       {
           set { this.LockState =value;}
           get { return this.LockState;}
        }

       private int AdminFlag;
       public int iAdminFlag
       {
           set { this.AdminFlag =value;}
           get { return this.AdminFlag;}
        }

       private int CheckState;
       public int iCheckState
       {
           set { this.CheckState =value;}
           get { return this.CheckState;}
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

