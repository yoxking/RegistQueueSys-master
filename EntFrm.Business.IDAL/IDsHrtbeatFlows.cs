using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IDsHrtbeatFlows
  {
        DsHrtbeatFlowsCollections GetAllRecords();
        DsHrtbeatFlowsCollections GetRecordsByClassNo(string sClassNo);
        DsHrtbeatFlowsCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddNewRecord(DsHrtbeatFlows info);
        int UpdateRecord(DsHrtbeatFlows info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        int HardDeleteByCondition(string sCondition);
        int SoftDeleteByCondition(string sCondition);
        DsHrtbeatFlowsCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

