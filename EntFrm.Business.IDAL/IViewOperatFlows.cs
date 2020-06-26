using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IViewOperatFlows
  {
        ViewOperatFlowsCollections GetAllRecords();
        ViewOperatFlowsCollections GetRecordsByClassNo(string sClassNo);
        ViewOperatFlowsCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddNewRecord(ViewOperatFlows info);
        int UpdateRecord(ViewOperatFlows info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        int HardDeleteByCondition(string sCondition);
        int SoftDeleteByCondition(string sCondition);
        ViewOperatFlowsCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

