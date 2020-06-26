using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace EntFrm.DataAdapter
{
    public class IUserContext
    {

        public static string GetAppCode()
        {
            try
            {
                return ConfigurationManager.AppSettings["AppCode"].ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string GetConnStr()
        {
            try
            {
                string connStr = ConfigurationManager.AppSettings["SqlServer"].ToString();

                return EnconfigHelper.Decrypt(connStr);

            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string GetConnStr2()
        {
            try
            {
                string connStr = ConfigurationManager.AppSettings["SqlServer2"].ToString();

                return EnconfigHelper.Decrypt(connStr); 
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string GetOracleStr()
        {
            try
            {
                string connStr = ConfigurationManager.AppSettings["OrlServer"].ToString();

                return EnconfigHelper.Decrypt(connStr); 
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string GetBranchNo()
        {
            try
            {
                return ConfigurationManager.AppSettings["BranchNo"].ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        
        public static string GetWServiceUrl()
        {
            try
            {
                return ConfigurationManager.AppSettings["WService"].ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static void SetConfigValue(string Name, string Value)
        {
            ConfigurationManager.AppSettings.Set(Name, Value);

            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings[Name].Value = Value;
            config.Save(ConfigurationSaveMode.Modified);
            config = null;
        }

        public static string GetConfigValue(string Name)
        {
            try
            {
                return ConfigurationManager.AppSettings[Name].ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        //定义业务、窗口、员工的状态，有更新:11111,无更新:00000
        public static bool SetStateValue(string sBranchNo, string sName, string sValue, int index = -1)
        {
            if (index < 0)
            {
                return SetParamValue(sBranchNo, sName, sValue, "State");
            }
            else
            {
                sValue = sValue.Substring(0, 1);
                string temp = GetParamValue(sBranchNo, sName, "State");
                if (!string.IsNullOrEmpty(temp))
                {
                    temp = temp.Remove(index, 1);
                    temp = temp.Insert(index, sValue);

                    return SetParamValue(sBranchNo, sName, temp, "State");
                }
                else
                {
                    if (sValue.Equals("0"))
                    {
                        return SetParamValue(sBranchNo, sName, "0000000000", "State");
                    }
                    else
                    {
                        return SetParamValue(sBranchNo, sName, "1111111111", "State");
                    }
                }
            }
        }

        public static string GetStateValue(string sBranchNo, string sName, int index = -1)
        {
            string result = "";
            if (index < 0)
            {
                result = GetParamValue(sBranchNo,sName, "State");
            }
            else
            {
                string temp = GetParamValue(sBranchNo,sName, "State");
                if (!string.IsNullOrEmpty(temp) && index < temp.Length)
                {
                    result = temp[index].ToString();
                }
            }

            return string.IsNullOrEmpty(result) ? "0" : result;
        }


        public static bool SetParamValue(string sBranchNo,string sName, string sValue, string sType = "2")
        {
            if (sName.Length > 0 && sValue.Length > 0)
            {
                try
                {
                    string sSuNo = "00000000";

                    int count = 100;
                    SysParams info = null;

                    SysParamsBLL infoBLL = new SysParamsBLL(GetConnStr(), GetAppCode());
                    SysParamsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, " BranchNo='"+ sBranchNo + "' And  KeyName='" + sName + "' And KeyType='" + sType + "' ");

                    if (infoColl != null && infoColl.Count > 0)
                    {
                        info = infoColl.GetFirstOne();

                        info.sKeyValue = sValue;
                        info.sModOptor = sSuNo;
                        info.dModDate = DateTime.Now;

                        return infoBLL.UpdateRecord(info);
                    }
                    else
                    {
                        info = new SysParams();

                        info.sParamNo = CommonHelper.Get_New12ByteGuid();
                        info.sKeyName = sName;
                        info.sKeyValue = sValue;
                        info.sKeyType = sType;
                        info.sBranchNo = sBranchNo;

                        info.sAddOptor = sSuNo;
                        info.dAddDate = DateTime.Now;
                        info.sModOptor = sSuNo;
                        info.dModDate = DateTime.Now;
                        info.iValidityState = 1;
                        info.sComments = "";

                        info.sAppCode = GetAppCode() + ";";

                        return infoBLL.AddNewRecord(info);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return false;
        }

        public static string GetParamValue(string sBranchNo,string sName, string sType = "2")
        {
            if (sName.Length > 0)
            {
                try
                {
                    int count = 0;


                    SysParamsBLL infoBLL = new SysParamsBLL(GetConnStr(), GetAppCode());
                    SysParamsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1,1, " BranchNo='" + sBranchNo + "' And  KeyName='" + sName + "' And KeyType='" + sType + "' ");
                    if (infoColl != null && infoColl.Count > 0)
                    {
                        return infoColl.GetFirstOne().sKeyValue;
                    }

                    return "";
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return "";
        }

        public static RegKeyModel getRegistryKey()
        {
            try
            {
                RegKeyModel regCode = null;
                RegistryKey retkey = Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("QueueSoftWare").CreateSubKey("Register.INI");
                object encDogId = retkey.GetValue("EncDogId");
                object organizName = retkey.GetValue("OrganizName");
                object updateDate = retkey.GetValue("UpdateDate");
                object activeDate = retkey.GetValue("ActiveDate");
                object activeCount = retkey.GetValue("ActiveCount");
                object activeValCode = retkey.GetValue("ActiveValCode");

                if (encDogId != null && organizName != null && activeCount != null && activeValCode != null)
                {
                }
                else
                {
                    encDogId = "000000";
                    organizName = "分诊排队系统";
                    updateDate = DateTime.Now.AddDays(-1).ToString();
                    activeDate = DateTime.Now.AddDays(30).ToString();
                    activeCount = "" + 30;
                    activeValCode = "000000000000000000000000";

                    retkey.SetValue("EncDogId", encDogId);
                    retkey.SetValue("OrganizName", organizName);
                    retkey.SetValue("UpdateDate", updateDate);
                    retkey.SetValue("ActiveDate", activeDate);
                    retkey.SetValue("ActiveCount", activeCount);
                    retkey.SetValue("ActiveValCode", activeValCode);
                }

                regCode = new RegKeyModel(encDogId.ToString(), organizName.ToString(), updateDate.ToString(), activeDate.ToString(), activeCount.ToString(), activeValCode.ToString());

                return regCode;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool setRegistryKey(RegKeyModel regCode)
        {
            try
            {
                if (regCode != null)
                {
                    string encDogId = regCode.EncDogId;
                    string organizName = regCode.OrganizName;
                    string updateDate = regCode.UpdateDate;
                    string activeDate = regCode.ActiveDate;
                    string activeCount = regCode.ActiveCount;
                    string activeValCode = regCode.ActiveValCode;

                    RegistryKey retkey = Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("QueueSoftWare").CreateSubKey("Register.INI");
                    retkey.SetValue("EncDogId", encDogId);
                    retkey.SetValue("OrganizName", organizName);
                    retkey.SetValue("UpdateDate", updateDate);
                    retkey.SetValue("ActiveDate", activeDate);
                    retkey.SetValue("ActiveCount", activeCount);
                    retkey.SetValue("ActiveValCode", activeValCode);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        } 

        public static EncDogModel getEncryptDog()
        {
            try
            {
                const int strLen = 4;
                string keyPath = "";

                //初始化我们的操作加密锁的类
                EncryptDogEx encDog = new EncryptDogEx();

                //这个用于判断系统中是否存在着加密锁。不需要是指定的加密锁, 
                if (encDog.FindPort(0, ref keyPath) != 0)
                {
                    //MessageBox.Show("未找到加密锁,请插入加密锁后，再进行操作。");
                    return null;
                }
                else
                {  
                    int ret, nlen;
                    string outstring = "";
                    byte[] buf = new byte[strLen];

                    //这个例子与上面的不同之处是，可以读取非固定长度的字符串，它是先从首地址读取字符串的长度，然后再读取相应的字符串
                    //先从地址0读到以前写入的字符串的长度
                    ret = encDog.YReadEx(buf, 0, strLen, IPublicConsts.RHKey, IPublicConsts.RLKey, keyPath);
                    if (ret == 0)
                    {
                        nlen = BitConverter.ToInt32(buf, 0);
                        //再读取相应长度的字符串
                        ret = encDog.YReadString(ref outstring, strLen + 1, nlen, IPublicConsts.RHKey, IPublicConsts.RLKey, keyPath);
                        if (ret == 0)
                        {
                            outstring = encDog.StrDec(outstring, IPublicConsts.ENCKEY);
                            EncDogModel dogCode = JsonConvert.DeserializeObject<EncDogModel>(outstring);

                            return dogCode;
                        }

                    }
                }

                return null;
            }
            catch (Exception ex)
            { return null; }
        }

        public static bool setEncryptDog(EncDogModel dogCode)
        {
            if (dogCode == null) { return false; }

            try
            {
                const int strLen = 4;
                string keyPath = "";

                //初始化我们的操作加密锁的类
                EncryptDogEx encDog = new EncryptDogEx();

                //这个用于判断系统中是否存在着加密锁。不需要是指定的加密锁, 
                if (encDog.FindPort(0, ref keyPath) != 0)
                {
                    //MessageBox.Show("未找到加密锁,请插入加密锁后，再进行操作。");
                    return false;
                }
                else
                { 

                    int ret, nlen;
                    byte[] buf = new byte[strLen];
                    string InString = JsonConvert.SerializeObject(dogCode);

                    //加密数据
                    InString = encDog.StrEnc(InString, IPublicConsts.ENCKEY);

                    nlen = EncryptDogEx.lstrlenA(InString);
                    buf = BitConverter.GetBytes(nlen);

                    //写入字符串到地址1
                    ret = encDog.YWriteString(InString, strLen + 1, IPublicConsts.WHKey, IPublicConsts.WLKey, keyPath);
                    if (ret == 0)
                    {
                        //写入字符串的长度到地址0
                        ret = encDog.YWriteEx(buf, 0, strLen, IPublicConsts.WHKey, IPublicConsts.WLKey, keyPath);
                        if (ret == 0)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            { return false; }
        }
        public static int doCheck_EncryptDogStatus(ref string activeDate)
        {
            try
            {
                RegKeyModel regCode = getRegistryKey();
                EncDogModel dogCode = getEncryptDog();

                if (dogCode != null && regCode != null)
                {
                    activeDate = dogCode.ActiveDate;
                    if (regCode.ActiveValCode.Equals(dogCode.ActiveValCode))
                    {
                        int vDays = (DateTime.Parse(dogCode.ActiveDate) - DateTime.Now).Days;

                        if (vDays > 0)
                        {
                            return 0;
                        }
                    }
                }
                return 1;
            }
            catch (Exception ex)
            { return 9; }
        }

        public static int doCheck_TrialRegistStatus()
        {
            DateTime currtDate = DateTime.Now;
            DateTime startDate = DateTime.Now;
            DateTime enditDate = DateTime.Now.AddDays(30);
            double vDays = 30;

            try
            {
                RegKeyModel regCode = getRegistryKey();

                if (regCode != null)
                {
                    startDate = DateTime.Parse(regCode.UpdateDate);
                    enditDate = DateTime.Parse(regCode.ActiveDate);
                    vDays = 30 - (currtDate - startDate).TotalDays;

                    if (vDays > 30)
                    {
                        return 0;
                    }
                }


                return (int)vDays;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
