using EntFrm.Business.IDAL;
using System;
using System.Configuration;

namespace EntFrm.Business.DALFactory
{
  public sealed class OperateFlowsFactory
  {
        public static IOperateFlows Create(string sUrl,string sAppCode) 
        {
          try
          {
           string path = ConfigurationManager.AppSettings["BusinessDAL"].ToString();
           string className = path + ".OperateFlowsDAL," + path;

           Type typeofControl = Type.GetType(className,true,true);
           return (IOperateFlows)Activator.CreateInstance(typeofControl, new object[] { sUrl, sAppCode });
          }
          catch (Exception ex)
          {
              throw new Exception(" 通过工厂模式创建DAL层时出错;" + ex.Message);
           }
      }
   }
  }

