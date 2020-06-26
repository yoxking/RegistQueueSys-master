using EntFrm.DataAdapter.Pubutils;
using System;
using System.Collections;
using System.Text;

namespace EntFrm.DataAdapter.HisData
{
    public class HisRequestUtil
    {
        public static string getHisReturnXml(string serviceName,string serviceArgs)
        {
            try
            {
                string weburl = IPublicConsts.HIS_WEBSERVICEURL;
                string accesstoken = IPublicConsts.HIS_ACCESSTOKEN;
                string secretkey = IPublicConsts.HIS_SECRETKEY;
                StringBuilder sb = new StringBuilder();

                sb.Append("<?xml version='1.0' encoding='UTF-8'?>"
                        + "<ROOT><TOKEN><![CDATA[" + IEncryptHelper.Encrypt_AES(accesstoken, secretkey) + "]]></TOKEN>"
                        + "<SERVICE><![CDATA[" + serviceName + "]]></SERVICE>"
                        + "<INSIDEKEY><![CDATA[]]></INSIDEKEY>"
                        + "<DATAPARAM><![CDATA[" + IEncryptHelper.Encrypt_AES(serviceArgs, secretkey) + "]]></DATAPARAM></ROOT>");
                 
                Hashtable pars = new Hashtable(); 
                pars["ReData"] = sb.ToString();
                String result = WsRequestUtil.QueryGetWebService(weburl, "Custom", pars); 

                if (result.IndexOf("ERROR") >= 0)
                {
                    return "";
                }
                else
                {
                    int i = result.IndexOf("<DATAPARAM>") + 11;
                    int j = result.IndexOf("</DATAPARAM>");
                    result = result.Substring(i, j-i).Replace("<![CDATA[", "").Replace("]]>", "");
                }
                String xml = IEncryptHelper.Decrypt_AES(result, secretkey);
                return xml;
            }
            catch(Exception ex)
            {
                return "";
            }
        }
    }
}
