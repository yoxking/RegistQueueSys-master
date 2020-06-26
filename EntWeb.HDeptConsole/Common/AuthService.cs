using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using System;

namespace EntWeb.HDeptConsole
{
    public class AuthService
    { 
        public LoginerInfo Login(string loginId, string password)
        {
            try
            {
                LoginerInfo loginInfo = null;

                //password = EncryptHelper.EncryptData(password, encrytpType.MD5, "", "", 128);

                SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                int i = 1;

                SUsersInfo info = null;
                SUsersInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref i, 1, 1, "  BranchNo='" + PublicHelper.Get_BranchNo() + "'  And LoginID='" + loginId + "'");
                if (infoColl != null && infoColl.Count == 1)
                {
                    info = infoColl.GetFirstOne();

                    if (info.sPassword.Equals(password))
                    {
                        loginInfo = new LoginerInfo();

                        loginInfo.LoginId = loginId;
                        loginInfo.UserNo = info.sSuNo;
                        loginInfo.AdminFlag = info.iAdminFlag;
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
                oldPassword = EncryptHelper.EncryptData(oldPassword, encrytpType.MD5, "", "", 128);
                newPassword = EncryptHelper.EncryptData(newPassword, encrytpType.MD5, "", "", 128);

                SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                SUsersInfo info = infoBLL.GetRecordByNo(suNo);

                if (info != null)
                {
                    if (info.sPassword.Equals(oldPassword))
                    {
                        info.sPassword = newPassword;

                        if (infoBLL.UpdateRecord(info))
                        {
                            return false;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("出错提示:修改密码时出错;" + ex.Message);
            }
        }
    }
}