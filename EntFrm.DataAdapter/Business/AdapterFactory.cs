using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.DataAdapter.Business
{
    public class AdapterFactory
    {
        public static IAdapterBusiness Create()
        {
            try
            { 
                string adapter = IUserContext.GetConfigValue("Adapter").ToString(); 

                IAdapterBusiness adapterBoss = null;

                switch (adapter)
                {
                    case "WzfuyouAdapter":
                        adapterBoss = new WzfuyouAdapter();
                        break;
                    case "YcrenminAdapter":
                        adapterBoss = new YcrenminAdapter();
                        break;
                    case "BsfuyouAdapter":
                        adapterBoss = new BsfuyouAdapter();
                        break;
                    case "Qh5thrmAdapter":
                        adapterBoss = new Qh5thrmAdapter();
                        break;
                    case "CqEyeykAdapter":
                        adapterBoss = new CqEyeykAdapter();
                        break;
                    case "YyshuangAdapter":
                        adapterBoss = new YyshuangAdapter();
                        break;
                    case "SantaizyyAdapter":
                        adapterBoss = new SantaizyyAdapter();
                        break;
                    case "PingtangAdapter":
                        adapterBoss = new PingtangAdapter();
                        break;
                    case "LzfuyouAdapter":
                        adapterBoss = new LzfuyouAdapter();
                        break;
                    default:
                        adapterBoss = new WzfuyouAdapter();
                        break;
                }

                return adapterBoss;
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过工厂模式创建Adapter时出错;" + ex.Message);
            }
        }
    }
}