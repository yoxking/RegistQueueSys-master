using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IViewRecipeFlows
  {
        ViewRecipeFlowsCollections GetAllRecords();
        ViewRecipeFlowsCollections GetRecordsByClassNo(string sClassNo);
        ViewRecipeFlowsCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddNewRecord(ViewRecipeFlows info);
        int UpdateRecord(ViewRecipeFlows info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        int HardDeleteByCondition(string sCondition);
        int SoftDeleteByCondition(string sCondition);
        ViewRecipeFlowsCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

