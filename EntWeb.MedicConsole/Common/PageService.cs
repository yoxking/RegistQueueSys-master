using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.MedicConsole.Common
{
    public class PageService
    {
        public static RUsersInfo GetRUserInfoByRiCardNo(string sRiCardNo)
        {
            int count = 0;
            string sWhere = " RiCardNo='" + sRiCardNo + "' ";
            RUsersInfoBLL infoBLL = new RUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            RUsersInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, sWhere);

            if (infoColl != null && infoColl.Count > 0)
            {
                return infoColl.GetFirstOne();
            }

            return null;
        }
    }
}