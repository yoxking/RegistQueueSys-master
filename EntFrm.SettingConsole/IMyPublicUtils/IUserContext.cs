using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace EntFrm.SettingConsole
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

        public static bool SetParamValue(string sName, string sValue, string sType = "2")
        {
            if (sName.Length > 0 && sValue.Length > 0)
            {
                try
                {
                    string sSuNo = "00000000";

                    int count = 100;
                    SysParams info = null;

                    SysParamsBLL infoBLL = new SysParamsBLL(GetConnStr(), GetAppCode());
                    SysParamsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, "  BranchNo = '" + IUserContext.GetBranchNo() + "' And KeyName='" + sName + "' And KeyType='" + sType + "' ");

                    if (infoColl != null && infoColl.Count > 0)
                    {
                        info = infoColl.GetFirstOne();

                        info.sKeyValue = sValue;
                        info.sBranchNo = IUserContext.GetBranchNo();
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
                        info.sBranchNo = IUserContext.GetBranchNo();

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

        public static string GetParamValue(string sName, string sType = "2")
        {
            if (sName.Length > 0)
            {
                try
                {
                    int count = 0;


                    SysParamsBLL infoBLL = new SysParamsBLL(GetConnStr(), GetAppCode());
                    SysParamsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, "  BranchNo = '" + IUserContext.GetBranchNo() + "' And  KeyName='" + sName + "' And KeyType='" + sType + "' ");
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
                    organizName = "排队叫号";
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
                    string encKey = "CABE123F456C7890ABDF123E456D7890";
                    string HKey = "ffffffff";
                    string LKey = "ffffffff";

                    //这个例子与上面的不同之处是，可以读取非固定长度的字符串，它是先从首地址读取字符串的长度，然后再读取相应的字符串

                    byte[] buf = new byte[4];
                    string outstring = "";
                    short addr = 0;//要读取的地址
                                   //先从地址0读到以前写入的字符串的长度
                    int ret = encDog.YReadEx(buf, addr, 4, HKey, LKey, keyPath);
                    int nlen = BitConverter.ToInt32(buf, 0);
                    if (ret == 0)
                    {
                        //再读取相应长度的字符串
                        ret = encDog.YReadString(ref outstring, addr + 4, nlen, HKey, LKey, keyPath);
                        if (ret == 0)
                        {
                            string s = encDog.StrDec(outstring, encKey);
                            EncDogModel dogCode = JsonConvert.DeserializeObject<EncDogModel>(s);

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
                string encKey = "CABE123F456C7890ABDF123E456D7890";
                string HKey = "ffffffff";
                string LKey = "ffffffff";

                //这个用于判断系统中是否存在着加密锁。不需要是指定的加密锁,
                if (encDog.FindPort(0, ref keyPath) != 0)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        if (encDog != null && dogCode != null)
                        {
                            //设置增强算法密钥二
                            short ver = 0;
                            if (encDog.NT_GetVersionEx(ref ver, keyPath) != 0)
                            {
                                //MessageBox.Show("返回加密锁扩展版本号错误");
                                return false;
                            }

                            if (ver < 10)
                            {
                                //MessageBox.Show("锁的扩展版本少于10,不支持增强算法二");
                                return false;
                            }

                            int ret = encDog.SetCal_New(encKey, keyPath);

                            if (ret == 0)
                            {
                                string InString = JsonConvert.SerializeObject(dogCode);
                                string outstring = encDog.StrEnc(InString, encKey);

                                int addr = 0;//要写入的地址 
                                int nlen = EncryptDogEx.lstrlenA(outstring);
                                byte[] buf = EncryptDogEx.IntConvertByteArray(nlen);

                                ret = encDog.YWriteString(outstring, addr + 4, HKey, LKey, keyPath);
                                if (ret == 0)
                                {
                                    //写入字符串的长度到地址0
                                    ret = encDog.YWriteEx(buf, addr, 4, HKey, LKey, keyPath);

                                    if (ret == 0)
                                    {
                                        return true;
                                    }

                                }
                            }
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
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
