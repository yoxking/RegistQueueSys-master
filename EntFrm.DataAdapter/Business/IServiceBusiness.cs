using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.DataAdapter.Entities;
using EntFrm.DataAdapter.Pubutils;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntFrm.DataAdapter.Business
{
    public class IServiceBusiness
    {
        private volatile static IServiceBusiness _instance = null;
        private static readonly object lockHelper = new object(); 

        public static IServiceBusiness CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new IServiceBusiness();
                }
            }
            return _instance;
        }

        private IServiceBusiness() { }

        public string getCurrentDatetime()
        {
            WSocketData socketData = new WSocketData();
            socketData.type = "timeinfo";
            socketData.content = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");

            return JsonConvert.SerializeObject(socketData);
        } 
          
    }
}
