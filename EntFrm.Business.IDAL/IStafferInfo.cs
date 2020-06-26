using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IStafferInfo
  {
        StafferInfoCollections GetAllRecords();
        StafferInfoCollections GetRecordsByClassNo(string sClassNo);
        StafferInfoCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddNewRecord(StafferInfo info);
        int UpdateRecord(StafferInfo info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        int HardDeleteByCondition(string sCondition);
        int SoftDeleteByCondition(string sCondition);
        StafferInfoCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

