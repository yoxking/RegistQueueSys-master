using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IDeviceInventory
  {
        DeviceInventoryCollections GetAllRecords();
        DeviceInventoryCollections GetRecordsByClassNo(string sClassNo);
        DeviceInventoryCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(DeviceInventory info);
        int UpdateRecord(DeviceInventory info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        DeviceInventoryCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

