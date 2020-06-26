using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Controllers
{
    public class HandleController : Controller
    {
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadPhoto()
        {
            try
            {
                string sAppUrl = PublicHelper.GetConfigValue("AppUrl");

                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                if (files.Count == 0)
                    return Json("Faild", JsonRequestBehavior.AllowGet);

                MD5 md5Hasher = new MD5CryptoServiceProvider();
                /*计算指定Stream对象的哈希值*/
                byte[] arrbytHashValue = md5Hasher.ComputeHash(files[0].InputStream);
                /*由以连字符分隔的十六进制对构成的String，其中每一对表示value中对应的元素；例如“F-2C-4A”*/
                string strHashData = System.BitConverter.ToString(arrbytHashValue).Replace("-", "");
                string FileEextension = Path.GetExtension(files[0].FileName);
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                string virtualPath = string.Format("/Uploads/Photo/{0}/{1}{2}", uploadDate, strHashData, FileEextension);
                string fullFileName = Server.MapPath(virtualPath);
                //创建文件夹，保存文件
                string path = Path.GetDirectoryName(fullFileName);
                Directory.CreateDirectory(path);
                if (!System.IO.File.Exists(fullFileName))
                {
                    files[0].SaveAs(fullFileName);
                }
                string fileName = files[0].FileName.Substring(files[0].FileName.LastIndexOf("\\") + 1, files[0].FileName.Length - files[0].FileName.LastIndexOf("\\") - 1);
                string fileSize = GetFileSize(files[0].ContentLength);

                string end = "{\"code\": 0,\"msg\": \"成功\",\"data\": {\"src\": \"" + sAppUrl+virtualPath + "\"}}";
                return Json(end, "text/html", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string end = "{\"code\": 0,\"msg\": \"" + ex.Message + "\",\"data\": {\"src\": \"\"}}";
                return Json(end, "text/html", JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult UploadAttach()
        {
            try
            {
                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                if (files.Count == 0)
                    return Json("Faild", JsonRequestBehavior.AllowGet);

                MD5 md5Hasher = new MD5CryptoServiceProvider();
                /*计算指定Stream对象的哈希值*/
                byte[] arrbytHashValue = md5Hasher.ComputeHash(files[0].InputStream);
                /*由以连字符分隔的十六进制对构成的String，其中每一对表示value中对应的元素；例如“F-2C-4A”*/
                string strHashData = System.BitConverter.ToString(arrbytHashValue).Replace("-", "");
                string FileEextension = Path.GetExtension(files[0].FileName);
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                string virtualPath = string.Format("/Uploads/Attachs/{0}/{1}{2}", uploadDate, strHashData, FileEextension);
                string fullFileName = Server.MapPath(virtualPath);
                //创建文件夹，保存文件
                string path = Path.GetDirectoryName(fullFileName);
                Directory.CreateDirectory(path);
                if (!System.IO.File.Exists(fullFileName))
                {
                    files[0].SaveAs(fullFileName);
                }
                string fileName = files[0].FileName.Substring(files[0].FileName.LastIndexOf("\\") + 1, files[0].FileName.Length - files[0].FileName.LastIndexOf("\\") - 1);
                string fileSize = GetFileSize(files[0].ContentLength);

                string end = "{\"code\": 0,\"msg\": \"成功\",\"data\": {\"src\": \"" + virtualPath + "\"}}";
                return Json(end, "text/html", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string end = "{\"code\": 0,\"msg\": \"" + ex.Message + "\",\"data\": {\"src\": \"\"}}";
                return Json(end, "text/html", JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult UploadMaterial()
        {
            try
            {
                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                if (files.Count == 0)
                    return Json("Faild", JsonRequestBehavior.AllowGet);

                MD5 md5Hasher = new MD5CryptoServiceProvider();
                /*计算指定Stream对象的哈希值*/
                byte[] arrbytHashValue = md5Hasher.ComputeHash(files[0].InputStream);
                /*由以连字符分隔的十六进制对构成的String，其中每一对表示value中对应的元素；例如“F-2C-4A”*/
                string strHashData = System.BitConverter.ToString(arrbytHashValue).Replace("-", "");
                string FileEextension = Path.GetExtension(files[0].FileName);
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                string virtualPath = string.Format("/MatStores/{0}/{1}{2}", uploadDate, strHashData, FileEextension);
                string fullFileName = Server.MapPath(virtualPath);
                //创建文件夹，保存文件
                string path = Path.GetDirectoryName(fullFileName);
                Directory.CreateDirectory(path);
                if (!System.IO.File.Exists(fullFileName))
                {
                    files[0].SaveAs(fullFileName);
                }
                string fileName = files[0].FileName.Substring(files[0].FileName.LastIndexOf("\\") + 1, files[0].FileName.Length - files[0].FileName.LastIndexOf("\\") - 1);
                string fileSize = GetFileSize(files[0].ContentLength);

                string end = "{\"code\": 0,\"msg\": \"成功\",\"data\": {\"src\": \"" + virtualPath + "\"}}";
                return Json(end, "text/html", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string end = "{\"code\": 0,\"msg\": \"" + ex.Message + "\",\"data\": {\"src\": \"\"}}";
                return Json(end, "text/html", JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult UploadProgram()
        {
            try
            {
                string sAppUrl = PublicHelper.GetConfigValue("AppUrl");

                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                if (files.Count == 0)
                { return Json("Faild", JsonRequestBehavior.AllowGet); }

                string FileName = Path.GetFileName(files[0].FileName);
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                string virtualPath = string.Format("/PgmStores/{0}/{1}", uploadDate, FileName);
                string fullFileName = Server.MapPath(virtualPath);
                //创建文件夹，保存文件
                string path = Path.GetDirectoryName(fullFileName);
                Directory.CreateDirectory(path);
                if (!System.IO.File.Exists(fullFileName))
                {
                    files[0].SaveAs(fullFileName);
                }

                string end = "{\"code\": 0,\"msg\": \"成功\",\"data\": {\"src\": \"" + sAppUrl+virtualPath + "\"}}";
                return Json(end, "text/html", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string end = "{\"code\": 0,\"msg\": \"" + ex.Message + "\",\"data\": {\"src\": \"\"}}";
                return Json(end, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private string GetFileSize(long bytes)
        {
            long kblength = 1024;
            long mbLength = 1024 * 1024;
            if (bytes < kblength)
                return bytes.ToString() + "B";
            if (bytes < mbLength)
                return decimal.Round(decimal.Divide(bytes, kblength), 2).ToString() + "KB";
            else
                return decimal.Round(decimal.Divide(bytes, mbLength), 2).ToString() + "MB";
        }
    }
}