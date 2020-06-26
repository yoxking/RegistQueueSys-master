using System;

namespace EntFrm.AutoUpdate.Service
{
    public interface IUpdateService
    {
        event EventHandler UpdateStarted;

        event EventHandler<UpdateProgressArgs> UpdateProgressChanged;

        event EventHandler<UpdateEndedArgs> UpdateEnded;
        string GetVersionCode();

        /// <summary>
        /// 探测是否有新版本
        /// </summary>
        /// <returns></returns>
        bool DetectVersion();
        /// <summary>
        /// 尝试立即升级，如果有新版本
        /// </summary>
        bool TryUpdateNow();

    }

    public class VersionDetectedArgs : EventArgs
    {
        /// <summary>
        /// 随后是否需要进行升级
        /// </summary>
        public bool ThenUpdate { get; set; }
    }

    public class UpdateProgressArgs : EventArgs
    {
        /// <summary>
        /// 进度百分比
        /// </summary>
        public float ProgressPercent { get; internal set; }

    }
    public class UpdateEndedArgs : EventArgs
    {
        /// <summary>
        /// 进度百分比
        /// </summary>
        public UpdateEndedType EndedType { get; }

        /// <summary>
        /// 错误异常, 如果是错误终止有值
        /// </summary>
        public Exception ErrorException { get; }
        /// <summary>
        /// 错误消息, 如果是错误终止有值
        /// </summary>
        public string ErrorMessage { get; }

        public UpdateEndedArgs(string errorMessage, Exception errorException)
        {
            this.EndedType = UpdateEndedType.ErrorAborted;
            this.ErrorMessage = errorMessage;
            this.ErrorException = errorException;
        }

        public UpdateEndedArgs()
        {
            this.EndedType = UpdateEndedType.Completed;
        }
    }

    public enum UpdateEndedType
    {
        /// <summary>
        /// 顺利完成
        /// </summary>
        Completed,
        /// <summary>
        /// 错误终止
        /// </summary>
        ErrorAborted
    }
}