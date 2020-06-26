using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.Framework.Web
{
    public interface IAuthCookie
    {
        int UserExpiresHours { get; set; }

        string UserName { get; set; }

        int UserId { get; set; }

        Guid UserToken { get; set; }

        string VerifyCode { get; set; }

        int LoginErrorTimes { get; set; }

        bool IsNeedVerifyCode { get; }
    }
}
