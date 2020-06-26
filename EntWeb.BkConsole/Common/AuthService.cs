using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.BkConsole
{
    public class AuthService
    {
        private string connStr;
        private string appCode;

        public AuthService()
        {
            this.connStr = PublicHelper.Get_ConnStr();
            this.appCode = PublicHelper.Get_AppCode();
        } 

        public LoginerInfo Login(string loginId, string password)
        {
            try
            {
                LoginerInfo loginInfo = null;

                //password = EncryptHelper.EncryptData(password, encrytpType.MD5, "", "", 128);

                SUsersInfoBLL infoBLL = new SUsersInfoBLL(connStr, appCode);

                int i = 1;

                SUsersInfo info = null;
                SUsersInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref i, 1, 1, " LoginID='" + loginId + "' And AdminFlag=1 And LockState=0");
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
            catch(Exception ex)
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

                SUsersInfoBLL infoBLL = new SUsersInfoBLL(connStr, appCode);

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