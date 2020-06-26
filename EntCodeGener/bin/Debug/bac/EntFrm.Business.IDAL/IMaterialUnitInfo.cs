using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IMaterialUnitInfo
  {
        MaterialUnitInfoCollections GetAllRecords();
        MaterialUnitInfoCollections GetRecordsByClassNo(string sClassNo);
        MaterialUnitInfoCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(MaterialUnitInfo info);
        int UpdateRecord(MaterialUnitInfo info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        MaterialUnitInfoCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

