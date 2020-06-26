using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IDeviceUnitInfo
  {
        DeviceUnitInfoCollections GetAllRecords();
        DeviceUnitInfoCollections GetRecordsByClassNo(string sClassNo);
        DeviceUnitInfoCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(DeviceUnitInfo info);
        int UpdateRecord(DeviceUnitInfo info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        DeviceUnitInfoCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

