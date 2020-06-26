using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Framework.Utility;
using System;

namespace EntFrm.MainService.Services
{
    public class Eq2013Display
    {
        private string LedDisplayNo;
        private string DisplayText;

        public Eq2013Display(string sLedDisplayNo, string sDisplayText)
        {
            LedDisplayNo = sLedDisplayNo;
            DisplayText = sDisplayText;
        }

        public void ShowLedText()
        {
            try
            {
                LEDDisplayBLL ledBoss = new LEDDisplayBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  

                LEDDisplay led = ledBoss.GetRecordByNo(LedDisplayNo);
                if (led != null && led.sLedModel.Equals("Eq2013"))
                {
                    string[] sparam = led.sParamFormat.Split(';');
                    if (sparam.Length == 8)
                    {
                        int posX = int.Parse(sparam[2]);
                        int posY = int.Parse(sparam[3]);
                        int width = int.Parse(sparam[4]);
                        int height = int.Parse(sparam[5]);
                        int fontSize = int.Parse(sparam[6]);
                        int fontAlign = int.Parse(sparam[7]);

                        Eq2008LedDisplay.SendDatafun(led.iPhyAddr, DisplayText, posX, posY, width, height, fontSize, fontAlign);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
