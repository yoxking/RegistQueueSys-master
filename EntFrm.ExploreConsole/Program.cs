using NetDimension.NanUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EntFrm.ExploreConsole
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //指定CEF架构和文件目录结构，并初始化CEF
            if (Bootstrap.Load(settings => {  
                //禁用日志
                settings.LogSeverity = Chromium.CfxLogSeverity.Disable;

                //指定中文为当前CEF环境的默认语言
                settings.AcceptLanguageList = "zh-CN";
                settings.Locale = "zh-CN";
            }, commandLine => {
                //在启动参数中添加disable-web-security开关，禁用跨域安全检测
                commandLine.AppendSwitch("disable-web-security");

            }))
            {
                //注册嵌入资源，并为指定资源指定一个假的域名 my.resource.local
                Bootstrap.RegisterAssemblyResources(System.Reflection.Assembly.GetExecutingAssembly(), domainName: "my.resource.local");

                PublicHelper.DoAutoUpdate();
                Application.Run(new MainFrame());
            }
        }
    }
}
