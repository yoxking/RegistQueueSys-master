using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntWeb.BkConsole.Common;
using EntWeb.BkConsole.Entities;
using EntWeb.BkConsole.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Controllers
{
    public class IShowerController : Controller
    {
        // GET: IShower
        public ActionResult Index()
        {
            return View();
        }

        /**
       * 返回所有素材分类
       */
        public string getAllMateClasses()
        {

            CodeData codeData = new CodeData();
            codeData.code = "200";
            codeData.msg = ("success");
            codeData.data = ("");

            DsMaterialClassBLL infoBoss = new DsMaterialClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

            DsMaterialClassCollections infoList = infoBoss.GetAllRecords();
            List<VmMatClass> clssList = new List<VmMatClass>();
            VmMatClass clss = new VmMatClass();

            clss.classId = ("1");
            clss.className = ("所有素材");
            clssList.Add(clss);

            if (infoList != null && infoList.Count > 0)
            {
                foreach (DsMaterialClass info in infoList)
                {
                    clss = new VmMatClass();

                    clss.classId = (info.sClassNo);
                    clss.className = (info.sClassName);

                    clssList.Add(clss);
                }
            }

            codeData.data = JsonConvert.SerializeObject(clssList);

            return JsonConvert.SerializeObject(codeData);
        }

        /**
         * 返回素材信息
         */
        public string getMatInfosTotal( string condition)
        {

            CodeData codeData = new CodeData();
            codeData.code = "200";
            codeData.msg = "success";
            codeData.data = "0"; 

            DsMaterialInfoBLL infoBoss = new DsMaterialInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            int count = infoBoss.GetCountByCondition(condition); 

            codeData.data = count.ToString();
            return JsonConvert.SerializeObject(codeData);
        }

        /**
         * 返回素材信息
         */
        public string getMatInfosByPage(string pageindex, string pagesize, string condition)
        {

            CodeData codeData = new CodeData();
            codeData.code = "200";
            codeData.msg = "success";
            codeData.data = "";

            int count = 0;
            int pageIndex = int.Parse(pageindex);
            int pageSize = int.Parse(pagesize);

            DsMaterialInfoBLL infoBoss = new DsMaterialInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

            DsMaterialInfoCollections infoList = infoBoss.GetRecordsByPaging(ref count, pageIndex, pageSize, condition);

            List<VmMatInfo> matList = new List<VmMatInfo>();
            VmMatInfo mat = null;

            if (infoList != null && infoList.Count > 0)
            {
                foreach (DsMaterialInfo info in infoList)
                {
                    mat = new VmMatInfo();

                    mat.matId = (info.sMatNo);
                    mat.matName = (info.sMatName);
                    mat.matPoster = (info.sMatPoster);
                    mat.matType = info.sMatType;
                    mat.matUrl = (info.sFilePath);
                    mat.matSize = (info.iFileSize.ToString());
                    mat.mDuration = (info.dPlayDuration.ToString("0.00"));

                    matList.Add(mat);
                }

                codeData.data = JsonConvert.SerializeObject(matList);
            } 
             

            return JsonConvert.SerializeObject(codeData);
        }

        /**
         * 返回素材信息
         */
        public string getMatInfoById(string id)
        {

            CodeData codeData = new CodeData();
            codeData.code = "200";
            codeData.msg = ("success");
            codeData.data = ("");

            DsMaterialInfoBLL infoBoss = new DsMaterialInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); 

            DsMaterialInfo info = infoBoss.GetRecordByNo(id);
            VmMatInfo mat = null;

            if (info != null)
            {
                mat = new VmMatInfo();

                mat.matId = (info.sMatNo);
                mat.matName = (info.sMatName);
                mat.matPoster = (info.sMatPoster);
                mat.matType = info.sMatType;
                mat.matUrl = (info.sFilePath);
                mat.matSize = (info.iFileSize.ToString());
                mat.mDuration = (info.dPlayDuration.ToString("0.00"));
            }

            return JsonConvert.SerializeObject(codeData);
        }

        /**
       * 返回所有模板节目
       */
        public string getAllTemplates()
        {

            CodeData codeData = new CodeData();
            codeData.code = "200";
            codeData.msg = ("success");
            codeData.data = ("");

            int count = 0;
            DsProgramInfoBLL infoBoss = new DsProgramInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

            DsProgramInfoCollections infoList = infoBoss.GetRecordsByPaging(ref count, 1, 100, " IsTemplate=1");
            List<VmProgram> programList = new List<VmProgram>();
            VmProgram program = null;

            if (infoList != null && infoList.Count > 0)
            {
                foreach (DsProgramInfo info in infoList)
                {
                    program = new VmProgram();

                    program.programId = (info.sProgmNo);
                    program.programName = (info.sProgmName);
                    program.pcontent = info.sPContent;

                    programList.Add(program);
                }
            }

            codeData.data = JsonConvert.SerializeObject(programList);

            return JsonConvert.SerializeObject(codeData);
        }

        /**
         * 返回素材信息
         */
        public string getProgramInfoById(string id)
        {

            CodeData codeData = new CodeData();
            codeData.code = "200";
            codeData.msg = ("success");
            codeData.data = ("");

            DsProgramInfoBLL infoBoss = new DsProgramInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

            DsProgramInfo info = infoBoss.GetRecordByNo(id);
            VmProgram program = null;

            if (info != null)
            {

                program = new VmProgram();

                program.programId = (info.sProgmNo);
                program.programName = (info.sProgmName);
                program.pcontent = info.sPContent;
                 
                codeData.data = JsonConvert.SerializeObject(program);
            }

            return JsonConvert.SerializeObject(codeData);
        }

        /**
         * 返回素材信息
         */
        public string getProgramByTempId(string tempId)
        {

            CodeData codeData = new CodeData();
            codeData.code = "200";
            codeData.msg = ("success");
            codeData.data = ("");

            DsProgramInfoBLL infoBoss = new DsProgramInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

            DsProgramInfo info = infoBoss.GetRecordByNo(tempId);
            VmProgram program = null;

            if (info != null)
            {

                program = new VmProgram();

                program.programId = (info.sProgmNo);
                program.programName = (info.sProgmName);
                program.pcontent = info.sPContent;

                codeData.data = JsonConvert.SerializeObject(program);
            }

            return JsonConvert.SerializeObject(codeData);
        }

        /**
         * 返回素材信息
         */
        [HttpPost]
        public string postProgramInfo(string programid, string pcontent)
        {

            CodeData codeData = new CodeData();
            codeData.code = "200";
            codeData.msg = ("success");
            codeData.data = ("");

            string sAppUrl = PublicHelper.GetConfigValue("AppUrl");

            DsProgramInfoBLL infoBoss = new DsProgramInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

            DsProgramInfo info = infoBoss.GetRecordByNo(programid); 

            if (info != null)
            {
                info.sPFilePath = sAppUrl + packageProgram(programid, pcontent);
                info.sPWebUrl = sAppUrl+"/DesignWeb/#/preview?id=" +programid;
                info.sPContent = pcontent;
                info.dModDate = DateTime.Now;
                infoBoss.UpdateRecord(info);

                codeData.data = ("true");
            }

            return JsonConvert.SerializeObject(codeData);
        }

        /**
         * 返回素材信息
         */
        [HttpPost]
        public string postProgramPoster(string programid, string posterSrc)
        {

            CodeData codeData = new CodeData();
            codeData.code = "200";
            codeData.msg = ("success");
            codeData.data = ("");

            string sAppUrl = PublicHelper.GetConfigValue("AppUrl");

            DsProgramInfoBLL infoBoss = new DsProgramInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

            DsProgramInfo info = infoBoss.GetRecordByNo(programid);

            if (info != null)
            {
                info.sPosterUrl = "xxxx";
                info.dModDate = DateTime.Now;
                infoBoss.UpdateRecord(info);

                codeData.data = ("true");
            }

            return JsonConvert.SerializeObject(codeData);
        }

        private string packageProgram(string programid, string pcontent)
        {
            try
            {
                string message = "";
                string jsonFile = Server.MapPath("/DesignWeb") + "/static/data.json";
                string dirPath = Server.MapPath("/DesignWeb");
                string zipPath = Server.MapPath("/Uploads/temps/" + programid + ".zip");

                MyFileHelper.Write(jsonFile, pcontent);

                SharpzipUtils sharpZip = new SharpzipUtils();
                if (sharpZip.ZipFolder(dirPath, zipPath, out message))
                {
                    return "/Uploads/temps/" + programid + ".zip";
                }

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}