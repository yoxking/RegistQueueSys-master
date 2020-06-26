using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.MedicConsole.Common
{
    public class AuthService
    {
        public LoginerInfo Login(string loginId, string password)
        {
            try
            {
                LoginerInfo loginInfo = null;

                //password = EncryptHelper.EncryptData(password, encrytpType.MD5, "", "", 128);

                StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                int i = 1;

                StafferInfo info = null;
                StafferInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref i, 1, 1, "  BranchNo='" + PublicHelper.Get_BranchNo() + "'  And LoginID='" + loginId + "'");
                if (infoColl != null && infoColl.Count == 1)
                {
                    info = infoColl.GetFirstOne();

                    if (info.sPassword.Equals(password))
                    {
                        loginInfo = new LoginerInfo();

                        loginInfo.LoginId = info.sStafferName;
                        loginInfo.UserNo = info.sStafferNo;
                        loginInfo.AdminFlag = 0;
                        loginInfo.AppCode = info.sAppCode.Substring(0, info.sAppCode.Length - 1);
                    }
                }

                return loginInfo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Logout(Guid token)
        {
        }

        public bool ModifyPassword(string suNo, string oldPassword, string newPassword)
        {
            try
            {  
                StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                StafferInfo info = infoBLL.GetRecordByNo(suNo);

                if (info != null)
                {
                    if (info.sPassword.Equals(oldPassword))
                    {
                        info.sPassword = newPassword;

                        if (infoBLL.UpdateRecord(info))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                //throw new Exception("出错提示:修改密码时出错;" + ex.Message);
                return false;
            }
        }
    }
}