using EntFrm.TicketConsole.MPublicUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.TicketConsole.RegBusiness
{
    public class RegisterFactory
    {
        public static IRegisterBusiness Create()
        {
            try
            {
                string register = IPublicHelper.GetConfigValue("Register").ToString();

                IRegisterBusiness registerBoss = null;

                switch (register)
                {
                    case "WzInspectRegister":
                        registerBoss = new WzInspectRegister();
                        break;
                    case "QhRecipeRegister":
                        registerBoss = new QhRecipeRegister();
                        break;
                    case "BsRecipeRegister":
                        registerBoss = new BsRecipeRegister();
                        break;
                    default:
                        registerBoss = new WzInspectRegister();
                        break;
                }

                return registerBoss;
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过工厂模式创建Adapter时出错;" + ex.Message);
            }
        }
    }
}