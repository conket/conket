using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nihongo.Models;
using Ivs.Core.Common;
using Nihongo.Dal.Dao;
using Ivs.Core.Interface;
using Ivs.Core.Web.Attributes;

namespace Nihongo.Controllers
{
    
    public class AchievementController : Controller
    {
        //
        // GET: /Achievement/
        public ActionResult Index()
        {
            ViewBag.VocaSets = new SelectList(new List<MS_VocaSetsModels>());
            ViewBag.VocaCates = new SelectList(new List<MS_VocaCategoriesModels>());
            return View();
        }

        [EncryptActionName(Name = ("GetVocaSets"))]
        [OutputCache(CacheProfile = "Cache5MinutesVaryByNone")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetVocaSets()
        {
            List<MS_VocaSetsModels> results = new List<MS_VocaSetsModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserName"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_VocaSetsDao dao = new MS_VocaSetsDao();
                MS_VocaSetsModels model = new MS_VocaSetsModels();
                returnCode = dao.SelectData(model, out results);
            }

            return Json(new { vocaSets = results, returnCode = returnCode }
                , JsonRequestBehavior.AllowGet);
        }

        [EncryptActionName(Name = ("SelectHightestPointData"))]
        [OutputCache(CacheProfile = "Cache5MinutesVaryByNone")]
        public ActionResult SelectHightestPointData(int? page, string sortColumn, string sortOrder)
        {
            IPagedList<MS_UsersModels> lstModels;
            int returnCode = CommonData.DbReturnCode.Succeed;
            int pageIndex = (page ?? 1);
            int pageSize = 10;

            MS_UsersDao dao = new MS_UsersDao();
            MS_UsersModels model = new MS_UsersModels();
            returnCode = dao.SelectHightestPointData(model, pageIndex, pageSize, out lstModels);

            //stt
            for (int i = 0; i < lstModels.Count; i++)
            {
                lstModels[i].No = i + 1;
            }

            //Sorting
            ViewBag.SortOrder = sortOrder;
            ViewBag.SortColumn = sortColumn;
            //GridSetting setting = new GridSetting(pageNumber, recordsPerPage, sortColumn, sortOrder);
            //lstModels = setting.LoadGridData<MS_TestResultModels>(lstModels, true).ToList();
            return PartialView("_HighestPointPartial", lstModels);//lstModels.ToPagedList(pageNumber, recordsPerPage));
            //return PartialView("_HighestPointPartial", result);
        }

        [EncryptActionName(Name = ("SelectHightestAccPointData"))]
        [OutputCache(CacheProfile = "Cache5MinutesVaryByNone")]
        public ActionResult SelectHightestAccPointData(int? page, string sortColumn, string sortOrder)
        {
            IPagedList<MS_UsersModels> lstModels;
            int returnCode = CommonData.DbReturnCode.Succeed;
            int pageIndex = (page ?? 1);
            int pageSize = 10;

            MS_TestResultDao dao = new MS_TestResultDao();
            MS_UsersModels model = new MS_UsersModels();
            returnCode = dao.SelectHightestAccPointData(model, pageIndex, pageSize, out lstModels);

            //stt
            for (int i = 0; i < lstModels.Count; i++)
            {
                lstModels[i].No = i + 1;
            }

            //Sorting
            ViewBag.SortOrder = sortOrder;
            ViewBag.SortColumn = sortColumn;
            //GridSetting setting = new GridSetting(pageNumber, recordsPerPage, sortColumn, sortOrder);
            //lstModels = setting.LoadGridData<MS_TestResultModels>(lstModels, true).ToList();
            return PartialView("_HighestAccPointPartial", lstModels);//lstModels.ToPagedList(pageNumber, recordsPerPage));
            //return PartialView("_HighestPointPartial", result);
        }

        [EncryptActionName(Name = ("SelectSmartestData"))]
        [OutputCache(CacheProfile = "Cache5MinutesVaryByNone")]
        public ActionResult SelectSmartestData(int? page, string sortColumn, string sortOrder)
        {
            IPagedList<MS_UsersModels> lstModels;
            int returnCode = CommonData.DbReturnCode.Succeed;
            int pageIndex = (page ?? 1);
            int pageSize = 10;

            MS_UsersDao dao = new MS_UsersDao();
            MS_UsersModels model = new MS_UsersModels();
            returnCode = dao.SelectSmartestData(model, pageIndex, pageSize, out lstModels);

            //stt
            for (int i = 0; i < lstModels.Count; i++)
            {
                lstModels[i].No = i + 1;
            }

            //Sorting
            ViewBag.SortOrder = sortOrder;
            ViewBag.SortColumn = sortColumn;
            //GridSetting setting = new GridSetting(pageNumber, recordsPerPage, sortColumn, sortOrder);
            //lstModels = setting.LoadGridData<MS_TestResultModels>(lstModels, true).ToList();
            return PartialView("SelectSmartestData", lstModels);//lstModels.ToPagedList(pageNumber, recordsPerPage));
            //return PartialView("_HighestPointPartial", result);
        }

    }
}
