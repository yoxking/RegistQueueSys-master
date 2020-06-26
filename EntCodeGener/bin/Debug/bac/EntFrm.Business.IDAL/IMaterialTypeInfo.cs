using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IMaterialTypeInfo
  {
        MaterialTypeInfoCollections GetAllRecords();
        MaterialTypeInfoCollections GetRecordsByClassNo(string sClassNo);
        MaterialTypeInfoCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(MaterialTypeInfo info);
        int UpdateRecord(MaterialTypeInfo info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        MaterialTypeInfoCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

