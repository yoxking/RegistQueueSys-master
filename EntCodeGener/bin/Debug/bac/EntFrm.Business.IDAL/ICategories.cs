using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface ICategories
  {
        CategoriesCollections GetAllRecords();
        CategoriesCollections GetRecordsByClassNo(string sClassNo);
        CategoriesCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(Categories info);
        int UpdateRecord(Categories info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        CategoriesCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

