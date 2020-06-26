namespace EntFrm.DataAdapter.Business
{
    public interface IAdapterBusiness
    {
        /// <summary>
        /// 清空心跳数据
        /// </summary>
        /// <returns></returns>
        bool wipeHrtbeatFlows();

        /// <summary>
        /// 从HIS更新门诊科室
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool updateBranchList();

        /// <summary>
        /// 从HIS更新医生
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool updateStafferList();

        /// <summary>
        /// 从HIS更新检查挂号信息
        /// </summary>
        /// <returns></returns>
        bool updatePhexamList();

        /// <summary>
        /// 从HIS更新检验挂号信息
        /// </summary>
        /// <returns></returns>
        bool updateInspectList();

        /// <summary>
        /// 从HIS更新病人挂号信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool updatePatientList();

        /// <summary>
        /// 从HIS更新病人预约挂号信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool updateRegisteList();


        /// <summary>
        /// 从HIS更新医生排班
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool updateStafferRota();


        /// <summary>
        /// 从HIS更新服务排班
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool updateServiceRota();

        /// <summary>
        /// 从HIS更新医技科室
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool updateCounterList();

        /// <summary>
        /// 从HIS更新门诊业务
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool updateServiceList();

        /// <summary>
        /// 从HIS更新手术信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool updateOperateList();

        /// <summary>
        /// 从HIS更新取药信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool updateRecipeList();

        /// <summary>
        /// 从HIS更新处方详单
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool updateRecipeDetail();
    }
}
