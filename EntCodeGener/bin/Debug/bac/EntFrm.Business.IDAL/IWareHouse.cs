using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IWareHouse
  {
        WareHouseCollections GetAllRecords();
        WareHouseCollections GetRecordsByClassNo(string sClassNo);
        WareHouseCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(WareHouse info);
        int UpdateRecord(WareHouse info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        WareHouseCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

