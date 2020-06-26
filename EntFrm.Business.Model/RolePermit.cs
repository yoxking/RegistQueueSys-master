using System;

namespace EntFrm.Business.Model
{
  public class RolePermit
  {
     public RolePermit(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string RoleNo;
       public string sRoleNo
       {
           set { this.RoleNo =value;}
           get { return this.RoleNo;}
        }

       private string PermitNo;
       public string sPermitNo
       {
           set { this.PermitNo =value;}
           get { return this.PermitNo;}
        }

       private string AppCode;
       public string sAppCode
       {
           set { this.AppCode =value;}
           get { return this.AppCode;}
        }

    }
  }

