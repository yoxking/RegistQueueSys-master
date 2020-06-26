using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.MainService.Services
{
    public class SeriportService
    {
        private volatile static SeriportService _instance = null;
        private static readonly object lockHelper = new object();
        private Dictionary<string, object> serPortDict;

        public static SeriportService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new SeriportService();

                }
            }
            return _instance;
        }

        private SeriportService()
        {
            serPortDict = new Dictionary<string, object>();
        }
        public bool setSerialPort(string sComName, object obj)
        {
            try
            {
                if (obj != null)
                {
                    serPortDict.Add(sComName, obj);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public object getSerialPort(string sComName)
        {
            if (serPortDict.ContainsKey(sComName))
            {
                return serPortDict[sComName];
            }
            return null;
        }
    }
}
