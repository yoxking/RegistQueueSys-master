using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IDeviceClassInfo
  {
        DeviceClassInfoCollections GetAllRecords();
        DeviceClassInfoCollections GetRecordsByClassNo(string sClassNo);
        DeviceClassInfoCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(DeviceClassInfo info);
        int UpdateRecord(DeviceClassInfo info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        DeviceClassInfoCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

