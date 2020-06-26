using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IDsPublishFlows
  {
        DsPublishFlowsCollections GetAllRecords();
        DsPublishFlowsCollections GetRecordsByClassNo(string sClassNo);
        DsPublishFlowsCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddNewRecord(DsPublishFlows info);
        int UpdateRecord(DsPublishFlows info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        int HardDeleteByCondition(string sCondition);
        int SoftDeleteByCondition(string sCondition);
        DsPublishFlowsCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

