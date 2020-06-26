using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface ITeachResourceDetail
  {
        TeachResourceDetailCollections GetAllRecords();
        TeachResourceDetailCollections GetRecordsByClassNo(string sClassNo);
        TeachResourceDetailCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(TeachResourceDetail info);
        int UpdateRecord(TeachResourceDetail info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        TeachResourceDetailCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

