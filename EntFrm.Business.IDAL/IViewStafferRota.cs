using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IViewStafferRota
  {
        ViewStafferRotaCollections GetAllRecords();
        ViewStafferRotaCollections GetRecordsByClassNo(string sClassNo);
        ViewStafferRotaCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddNewRecord(ViewStafferRota info);
        int UpdateRecord(ViewStafferRota info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        int HardDeleteByCondition(string sCondition);
        int SoftDeleteByCondition(string sCondition);
        ViewStafferRotaCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

