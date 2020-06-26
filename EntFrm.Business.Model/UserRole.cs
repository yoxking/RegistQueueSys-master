using System;

namespace EntFrm.Business.Model
{
  public class UserRole
  {
     public UserRole(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string UserNo;
       public string sUserNo
       {
           set { this.UserNo =value;}
           get { return this.UserNo;}
        }

       private string RoleNo;
       public string sRoleNo
       {
           set { this.RoleNo =value;}
           get { return this.RoleNo;}
        }

       private string AppCode;
       public string sAppCode
       {
           set { this.AppCode =value;}
           get { return this.AppCode;}
        }

    }
  }

