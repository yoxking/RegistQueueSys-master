using EntFrm.ExploreConsole.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace EntFrm.ExploreConsole.Pubutils
{
    public class WServHelper
    { 
        public static RUserData getPatientInfoByRicardId(string ricardId)
        {
            try
            {
                RUserData ruserData = null;

                List<WhereData> whereList = new List<WhereData>();
                WhereData where = new WhereData();
                where.field = "CARD_NO";
                where.operate = "EQ";
                where.relation = "AND";
                where.value = ricardId;

                whereList.Add(where);

                string condition = JsonConvert.SerializeObject(whereList);
                condition = condition.Replace("operate", "operator");

                MessagePackService.MessagePackClient messageService = new MessagePackService.MessagePackClient();
                string result = messageService.getMessage("PATIENT_INFO", condition);


                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);

                XmlNodeList xmlList = xmlDoc.SelectNodes("/msg/data");
                XmlElement item = (XmlElement)xmlList[0];

                if (item != null)
                {
                    ruserData = new RUserData();

                    ruserData.Id = item["PATIENT_INFO_ID"].InnerText;
                    ruserData.Name = item["NAME"].InnerText;
                    ruserData.Age = item["AGE"].InnerText;
                    ruserData.Sex = item["SEX_NAME"].InnerText;
                    ruserData.IdNo = item["ID_NO"].InnerText;
                    ruserData.RiCardNo = item["CARD_NO"].InnerText;
                    ruserData.Telphone = item["TEL_NO"].InnerText;

                }
                return ruserData;
            }
            catch (Exception ex)
            {
                //MainFrame.PrintMessage("错误:" + ex.Message);
                return null;
            }
        }
        public static RUserData getPatientInfoByPatientId(string patientId)
        {
            try
            {
                RUserData ruserData = null;

                List<WhereData> whereList = new List<WhereData>();
                WhereData where = new WhereData();
                where.field = "PATIENT_INFO_ID";
                where.operate = "EQ";
                where.relation = "AND";
                where.value = patientId;

                whereList.Add(where);

                string condition = JsonConvert.SerializeObject(whereList);
                condition = condition.Replace("operate", "operator");

                MessagePackService.MessagePackClient messageService = new MessagePackService.MessagePackClient();
                string result = messageService.getMessage("PATIENT_INFO", condition);


                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);

                XmlNodeList xmlList = xmlDoc.SelectNodes("/msg/data");
                XmlElement item = (XmlElement)xmlList[0];

                if (item != null)
                {
                    ruserData = new RUserData();

                    ruserData.Id = item["PATIENT_INFO_ID"].InnerText;
                    ruserData.Name = item["NAME"].InnerText;
                    ruserData.Age = item["AGE"].InnerText;
                    ruserData.Sex = item["SEX_NAME"].InnerText;
                    ruserData.IdNo = item["ID_NO"].InnerText;
                    ruserData.RiCardNo = item["CARD_NO"].InnerText;
                    ruserData.Telphone = item["TEL_NO"].InnerText;

                }
                return ruserData;
            }
            catch (Exception ex)
            {
                //MainFrame.PrintMessage("错误:" + ex.Message);
                return null;
            }
        }
    }
}
