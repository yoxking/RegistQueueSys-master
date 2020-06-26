using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface IDeviceGuaranteeInfo
  {
        DeviceGuaranteeInfoCollections GetAllRecords();
        DeviceGuaranteeInfoCollections GetRecordsByClassNo(string sClassNo);
        DeviceGuaranteeInfoCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(DeviceGuaranteeInfo info);
        int UpdateRecord(DeviceGuaranteeInfo info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        DeviceGuaranteeInfoCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

