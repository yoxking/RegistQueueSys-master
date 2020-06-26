namespace EntFrm.AutoUpdate.Service
{
    public enum UpdateStatus
    {
        /// <summary>
        /// 未检测到新版本
        /// </summary>
        NoVersion,
        /// <summary>
        /// 升级开始
        /// </summary>
        Started,
        /// <summary>
        /// 升级结束
        /// </summary>
        Ended,
        /// <summary>
        /// 正在升级
        /// </summary>
        Upgrading
    }
}