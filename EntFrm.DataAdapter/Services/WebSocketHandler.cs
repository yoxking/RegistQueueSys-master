using EntFrm.DataAdapter.Business;
using EntFrm.DataAdapter.Entities;
using Newtonsoft.Json;
using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace EntFrm.DataAdapter.Services
{
    public class WebSocketHandler : WebSocketBehavior
    {  
        public WebSocketHandler()
        { 
        }

        protected override void OnOpen()
        { 
        }
        protected override void OnError(ErrorEventArgs e)
        { 
        }
        protected override void OnClose(CloseEventArgs e)
        { 
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {

                CmmdData cmdData = JsonConvert.DeserializeObject<CmmdData>(e.Data);

                if (cmdData != null && cmdData.cmmdType.Equals("IQueueService"))
                {
                    string sResult = "";
                    switch (cmdData.cmmdName)
                    {
                        case "getCurrentDatetime":
                            sResult = IServiceBusiness.CreateInstance().getCurrentDatetime(); break; 
                        default: break;
                    }
                    Send(sResult);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
