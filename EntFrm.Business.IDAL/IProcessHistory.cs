using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IProcessHistory
  {
        ProcessHistoryCollections GetAllRecords();
        ProcessHistoryCollections GetRecordsByClassNo(string sClassNo);
        ProcessHistoryCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddNewRecord(ProcessHistory info);
        int UpdateRecord(ProcessHistory info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        int HardDeleteByCondition(string sCondition);
        int SoftDeleteByCondition(string sCondition);
        ProcessHistoryCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

