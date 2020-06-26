using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
    public interface IPermitInfo
    {
        PermitInfoCollections GetAllRecords();
        PermitInfoCollections GetRecordsByClassNo(string sClassNo);
        PermitInfoCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(PermitInfo info);
        int UpdateRecord(PermitInfo info);
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        PermitInfoCollections GetRecords_Paging(SqlModel s_model);
        PermitInfoCollections GetRecordsByCodeNo(string sCodeNo);
        int GetCountByCondition(string sCondition);
        PermitInfoCollections GetRecordsByUserNo(string sUserNo, string sParentPermitNo);
    }
}
