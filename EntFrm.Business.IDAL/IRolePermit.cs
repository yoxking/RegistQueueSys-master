using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
    public interface IRolePermit
    { 
        RolePermitCollections GetAllRecords();

        RolePermitCollections GetRecordsByRoleNo(string sNo);

        RolePermitCollections GetRecordsByPermitNo(string sNo);

        RolePermitCollections GetRecordsByRoleNoAndPermitNo(string sRoleNo, string sPermitNo);

        int AddRecord(RolePermit info);

        int UpdateRecord(RolePermit info);

        int HardDeleteRecord(string sRoleNo, string sPermitNo);

        int DeleteRecordByRoleNo(string sNo);

        int DeleteRecordByPermitNo(string sNo);

        RolePermitCollections GetRecords_Paging(SqlModel s_model);

        int GetCountByCondition(string sCondition);
    }
}
