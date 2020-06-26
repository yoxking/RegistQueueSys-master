using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IRecipeHistory
  {
        RecipeHistoryCollections GetAllRecords();
        RecipeHistoryCollections GetRecordsByClassNo(string sClassNo);
        RecipeHistoryCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddNewRecord(RecipeHistory info);
        int UpdateRecord(RecipeHistory info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        int HardDeleteByCondition(string sCondition);
        int SoftDeleteByCondition(string sCondition);
        RecipeHistoryCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

