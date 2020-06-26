using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IOutlineType
  {
        OutlineTypeCollections GetAllRecords();
        OutlineTypeCollections GetRecordsByClassNo(string sClassNo);
        OutlineTypeCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(OutlineType info);
        int UpdateRecord(OutlineType info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        OutlineTypeCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

