using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IRankType
  {
        RankTypeCollections GetAllRecords();
        RankTypeCollections GetRecordsByClassNo(string sClassNo);
        RankTypeCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(RankType info);
        int UpdateRecord(RankType info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        RankTypeCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

