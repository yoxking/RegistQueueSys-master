using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IExptProjectInfo
  {
        ExptProjectInfoCollections GetAllRecords();
        ExptProjectInfoCollections GetRecordsByClassNo(string sClassNo);
        ExptProjectInfoCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(ExptProjectInfo info);
        int UpdateRecord(ExptProjectInfo info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        ExptProjectInfoCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

