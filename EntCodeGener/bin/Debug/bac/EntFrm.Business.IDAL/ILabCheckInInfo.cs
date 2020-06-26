using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface ILabCheckInInfo
  {
        LabCheckInInfoCollections GetAllRecords();
        LabCheckInInfoCollections GetRecordsByClassNo(string sClassNo);
        LabCheckInInfoCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(LabCheckInInfo info);
        int UpdateRecord(LabCheckInInfo info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        LabCheckInInfoCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

