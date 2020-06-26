using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IAbstract
  {
        AbstractCollections GetAllRecords();
        AbstractCollections GetRecordsByClassNo(string sClassNo);
        AbstractCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(Abstract info);
        int UpdateRecord(Abstract info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        AbstractCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

