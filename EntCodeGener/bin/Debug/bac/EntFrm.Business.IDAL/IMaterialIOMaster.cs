using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IMaterialIOMaster
  {
        MaterialIOMasterCollections GetAllRecords();
        MaterialIOMasterCollections GetRecordsByClassNo(string sClassNo);
        MaterialIOMasterCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(MaterialIOMaster info);
        int UpdateRecord(MaterialIOMaster info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        MaterialIOMasterCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

