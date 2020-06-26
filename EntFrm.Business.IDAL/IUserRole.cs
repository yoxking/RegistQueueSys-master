using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
    public interface IUserRole
    {
        UserRoleCollections GetAllRecords();

        UserRoleCollections GetRecordsByUserNo(string sNo);

        UserRoleCollections GetRecordsByRoleNo(string sNo);

        UserRoleCollections GetRecordsByUserNoAndRoleNo(string sUserNo, string sRoleNo);

        int AddRecord(UserRole info);

        int UpdateRecord(UserRole info);

        int HardDeleteRecord(string sUserNo, string sRoleNo);

        int DeleteRecordByUserNo(string sNo);

        int DeleteRecordByRoleNo(string sNo);

        UserRoleCollections GetRecords_Paging(SqlModel s_model);

        int GetCountByCondition(string sCondition);
    }
}
