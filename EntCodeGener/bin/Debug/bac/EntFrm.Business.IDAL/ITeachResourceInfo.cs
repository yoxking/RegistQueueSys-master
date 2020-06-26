using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface ITeachResourceInfo
  {
        TeachResourceInfoCollections GetAllRecords();
        TeachResourceInfoCollections GetRecordsByClassNo(string sClassNo);
        TeachResourceInfoCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(TeachResourceInfo info);
        int UpdateRecord(TeachResourceInfo info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        TeachResourceInfoCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

