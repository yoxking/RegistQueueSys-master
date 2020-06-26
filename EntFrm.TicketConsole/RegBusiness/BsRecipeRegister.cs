using EntFrm.TicketConsole.MPublicUtils;

namespace EntFrm.TicketConsole.RegBusiness
{
    public class BsRecipeRegister : IRegisterBusiness
    {
        public string RegisterScanCode(string strCode)
        {
            return IUserContext.OnExecuteCommand_Xp("doRegistScanByRecipeNo", new string[] { strCode }); 
        }
    }
}

