using System;
using System.Linq;
using EntFrm.Core.Config;
using EntFrm.System.IDAL;

namespace EntFrm.Business.DALFactory
{
  public sealed class DeviceIODetailFactory
  {
        public static IDeviceIODetail Create(string sUrl,string sAppCode) 
        {
          try
          {
           string path = ConfigHelper.Current.ParamsConfig.ParamsConfigItems.Single(n => n.Key == "BusinessDAL").Value;
           string className = path + ".DeviceIODetailDAL," + path;

           Type typeofControl = Type.GetType(className,true,true);
           return (IDeviceIODetail)Activator.CreateInstance(typeofControl, new object[] { sUrl, sAppCode });
          }
          catch (Exception ex)
          {
              throw new Exception(" 通过工厂模式创建DAL层时出错;" + ex.Message);
           }
      }
   }
  }

