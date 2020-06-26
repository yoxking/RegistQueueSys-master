using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IPlanType
  {
        PlanTypeCollections GetAllRecords();
        PlanTypeCollections GetRecordsByClassNo(string sClassNo);
        PlanTypeCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(PlanType info);
        int UpdateRecord(PlanType info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        PlanTypeCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

