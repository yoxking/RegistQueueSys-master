using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IMaterialInfo
  {
        MaterialInfoCollections GetAllRecords();
        MaterialInfoCollections GetRecordsByClassNo(string sClassNo);
        MaterialInfoCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(MaterialInfo info);
        int UpdateRecord(MaterialInfo info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        MaterialInfoCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

