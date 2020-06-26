using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface ILaboratoryInfo
  {
        LaboratoryInfoCollections GetAllRecords();
        LaboratoryInfoCollections GetRecordsByClassNo(string sClassNo);
        LaboratoryInfoCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(LaboratoryInfo info);
        int UpdateRecord(LaboratoryInfo info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        LaboratoryInfoCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

