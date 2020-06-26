using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface ITeachResourceType
  {
        TeachResourceTypeCollections GetAllRecords();
        TeachResourceTypeCollections GetRecordsByClassNo(string sClassNo);
        TeachResourceTypeCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(TeachResourceType info);
        int UpdateRecord(TeachResourceType info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        TeachResourceTypeCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

