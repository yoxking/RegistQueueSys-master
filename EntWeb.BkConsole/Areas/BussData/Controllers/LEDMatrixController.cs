using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using EntFrm.Framework.Web.Controls;

using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.BussData.Controllers
{
    public class LEDMatrixController : frmMainController
    {
        private string sWhere
        {
            set { TempData["Where_" + RouteData.Values["controller"].ToString()] = value; }
            get
            {
                var temp = TempData.Peek("Where_" + RouteData.Values["controller"].ToString());
                if (temp == null)
                {
                    return " 1=1 ";
                }
                return temp.ToString();
            }
        }

        //
        // GET: /System/Role/
        public override ActionResult Index()
        {
            return RedirectToAction("List");
        }
         
        public override ActionResult List()
        {
            try
            {
                PageIndex = int.Parse(Request.Form["pageIndex"] == null ? "1" : Request.Form["pageIndex"].ToString());
                Condition = " BranchNo='" + PublicHelper.Get_BranchNo() + "'";
                if (!string.IsNullOrEmpty(sWhere))
                {
                    Condition += " And " + sWhere;
                }

                LEDMatrixBLL infoBLL = new LEDMatrixBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                LEDMatrixCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
                int totalCount = infoBLL.GetCountByCondition(Condition);

                PagerHelper pager = new PagerHelper(PageIndex, PageSize, totalCount);

                Dictionary<string, object> stackHolder = new Dictionary<string, object>();
                stackHolder.Add("infoList", infoColl);
                stackHolder.Add("pager", pager);

                ViewBag.StackHolder = stackHolder;
            }
            catch (Exception ex)
            { }
            return View();
        }
         
        public override ActionResult Search()
        {
            sWhere = "1=1 ";

            //if (!string.IsNullOrEmpty(sTrueName))
            //{
            //    sWhere += " And (TrueName like '%" + sTrueName + "%'  OR LoginId like '%" + sTrueName + "%' )";
            //}

            return RedirectToAction("List");
        }
         
        public override ActionResult Add()
        {
            try
            {
                LEDMatrix info = new LEDMatrix();
                info.sMatrixNo = CommonHelper.Get_New12ByteGuid(); 
                info.iPhyAddr = 1;
                info.sProtocol = "Eq2013";
                info.sSerialPort = "COM1";
                info.iBaudrate = 9600;
                info.sIpAddress = "192.168.1.1";
                info.sLocalPort = "5555"; 
                info.iTimeoutSec = 3;
                info.iDisplayRows = 5; 

                ViewBag.StackHolder = info;

            }
            catch (Exception ex)
            {
            }
            return View("Edit");
        }
         
        public override ActionResult Edit(string id)
        {
            try
            {
                LEDMatrixBLL infoBLL = new LEDMatrixBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                LEDMatrix info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost] 
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string MatrixNo = Request.Form["MatrixNo"].ToString();
                string MatrixName = Request.Form["MatrixName"].ToString();
                string MatrixModel = Request.Form["MatrixModel"].ToString();
                string ServiceNos = Request.Form["ServiceNos"].ToString();                
                int PhyAddr =int.Parse( Request.Form["PhyAddr"].ToString());
                string Protocol = Request.Form["Protocol"].ToString();
                string SerialPort = Request.Form["SerialPort"].ToString();
                int Baudrate =int.Parse( Request.Form["Baudrate"].ToString());
                string IpAddress = Request.Form["IpAddress"].ToString();
                string LocalPort = Request.Form["LocalPort"].ToString();
                string ParamFormat = Request.Form["ParamFormat"].ToString();
                int TimeoutSec =int.Parse( Request.Form["TimeoutSec"].ToString());
                int DisplayRows =int.Parse( Request.Form["DisplayRows"].ToString());
                string DisplayFormat = Request.Form["DisplayFormat"].ToString();
                string Comments = Request.Form["Comments"].ToString();

                LEDMatrixBLL infoBLL = new LEDMatrixBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                LEDMatrix info = infoBLL.GetRecordByNo(MatrixNo);

                //新增操作
                if (info == null)
                {
                    info = new LEDMatrix();

                    info.sMatrixNo = MatrixNo;
                    info.sMatrixName = MatrixName;
                    info.sMatrixModel = MatrixModel;
                    info.sServiceNos = ServiceNos;
                    info.iPhyAddr = PhyAddr;
                    info.sProtocol = Protocol;
                    info.sSerialPort = SerialPort;
                    info.iBaudrate = Baudrate;
                    info.sIpAddress = IpAddress;
                    info.sLocalPort = LocalPort;
                    info.sParamFormat = ParamFormat;
                    info.iTimeoutSec = TimeoutSec;
                    info.iDisplayRows = DisplayRows;
                    info.sDisplayFormat = DisplayFormat;
                    info.sBranchNo = PublicHelper.Get_BranchNo();
                    info.sComments = Comments;

                    info.sAddOptor = sSuNo;
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = sSuNo;
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sAppCode = PublicHelper.Get_AppCode() + ";";


                    if (infoBLL.AddNewRecord(info))
                    {
                        json.Message = "保存成功";
                        json.Status = "Success";
                    }
                }
                //更新操作
                else
                {
                    info.sMatrixName = MatrixName;
                    info.sMatrixModel = MatrixModel;
                    info.sServiceNos = ServiceNos;
                    info.iPhyAddr = PhyAddr;
                    info.sProtocol = Protocol;
                    info.sSerialPort = SerialPort;
                    info.iBaudrate = Baudrate;
                    info.sIpAddress = IpAddress;
                    info.sLocalPort = LocalPort;
                    info.sParamFormat = ParamFormat;
                    info.iTimeoutSec = TimeoutSec;
                    info.iDisplayRows = DisplayRows;
                    info.sDisplayFormat = DisplayFormat;
                    info.sComments = Comments;

                    info.sModOptor = sSuNo;
                    info.dModDate = DateTime.Now;

                    if (infoBLL.UpdateRecord(info))
                    {
                        json.Message = "保存成功";
                        json.Status = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                json.Message = "保存人员信息发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }
         
        public override ActionResult Delete(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                LEDMatrixBLL infoBLL = new LEDMatrixBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                infoBLL.SoftDeleteRecord(sNos);
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        } 
        public override ActionResult Detail(string id)
        {
            try
            {
                LEDMatrixBLL infoBLL = new LEDMatrixBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                LEDMatrix info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}