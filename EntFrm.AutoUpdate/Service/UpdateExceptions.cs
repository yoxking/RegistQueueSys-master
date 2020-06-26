using System;

namespace EntFrm.AutoUpdate.Service
{
    public class RollbackException : UpdateException
    {
        public RollbackException(string message, Exception inner) : base(message, inner)
        {
        }
    }
    public class DeleteTempFileException : UpdateException
    {
        public DeleteTempFileException(string message, Exception inner) : base(message, inner)
        {
        }
    }
    public class ReplaceVersionException : UpdateException
    {
        public ReplaceVersionException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    /// <summary>
    /// 非法文件
    /// </summary>
    public class UnlawfulException : UpdateException
    {
        public UnlawfulException(string message, Exception inner) : base(message, inner)
        {
        }
    }
    public class DownFileException : UpdateException
    {
        public DownFileException(string message, Exception inner) : base(message, inner)
        {
        }
    }
    public class BackupFileException : UpdateException
    {
        public BackupFileException(string message, Exception inner) : base(message, inner)
        {
        }
    }
    public class CreateVersionInfoException : UpdateException
    {
        public CreateVersionInfoException(string message, Exception inner) : base(message, inner)
        {
        }
    }
    public class UpdateException : Exception
    {
        public UpdateException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}