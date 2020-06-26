using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface ILabOpeningInfo
  {
        LabOpeningInfoCollections GetAllRecords();
        LabOpeningInfoCollections GetRecordsByClassNo(string sClassNo);
        LabOpeningInfoCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(LabOpeningInfo info);
        int UpdateRecord(LabOpeningInfo info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        LabOpeningInfoCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

