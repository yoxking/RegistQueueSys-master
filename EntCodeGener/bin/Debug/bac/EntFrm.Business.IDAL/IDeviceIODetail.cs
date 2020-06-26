using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IDeviceIODetail
  {
        DeviceIODetailCollections GetAllRecords();
        DeviceIODetailCollections GetRecordsByClassNo(string sClassNo);
        DeviceIODetailCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(DeviceIODetail info);
        int UpdateRecord(DeviceIODetail info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        DeviceIODetailCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

