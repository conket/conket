using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nihongo.Models;
using Nihongo.Dal.Dao;
using Ivs.Core.Common;
using Ivs.Core.Data;
using Nihongo.Dal.Data;
using Ivs.Core.Web.Attributes;

namespace Nihongo.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(string id)
        {
            if (!CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                int returnCode = 0;

                //List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
                //MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
                //MS_UserVocabulariesModels model = new MS_UserVocabulariesModels();
                //model.UserID = CommonMethod.ParseInt(Session["UserID"]);
                //model.UserName = CommonMethod.ParseString(Session["UserName"]);
                //returnCode = vocaDao.SelectWeakVocaSummary(model, out results);
                //Session["Inbox"] = results;

                MS_UsersDao dao = new MS_UsersDao();
                MS_UsersModels user = new MS_UsersModels();
                user.ID = CommonMethod.ParseInt(Session["UserID"]);
                user.UserName = CommonMethod.ParseString(Session["UserName"]);
                user.LoginState = CommonData.Status.Enable;
                user.LastVisitedDate = DateTime.Now;
                returnCode = dao.UpdateState(user);

                return RedirectToAction("HomePage", "Account");
            }
            else
            {
                Session["Inbox"] = new List<MS_UserVocabulariesModels>();
                Session["UserID"] = CommonData.StringEmpty;
                Session["UserName"] = CommonData.StringEmpty;
                Session["DisplayName"] = CommonData.StringEmpty;
                Session["UserUrlImage"] = CommonData.StringEmpty;
                Session["IsAdmin"] = false;
                UserSession.UserName = CommonData.StringEmpty;
                UserSession.UserID = -1;
            }

            return View("Index");
        }

        public ActionResult Note(int? id, string urlDisplay)
        {
            ViewBag.NoteID = id;
            ViewBag.NoteUrlDisplay = urlDisplay;

            return View();
        }

        [OutputCache(CacheProfile = "Cache1HourVaryByNone")]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [OutputCache(CacheProfile = "Cache1HourVaryByNone")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [OutputCache(CacheProfile = "Cache1HourVaryByNone")]
        public ActionResult Support()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [EncryptActionName(Name = ("LoadSet"))]
        [OutputCache(CacheProfile = "Cache1HourVaryByNone")]
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadSet()
        {
            List<MS_VocaSetsModels> result = new List<MS_VocaSetsModels>();
            MS_VocaSetsDao dao = new MS_VocaSetsDao();
            MS_VocaSetsModels model = new MS_VocaSetsModels();
            int returnCode = dao.SelectData(model, out result);
            DataSession.VocaSets = result.ToDictionary(ss => ss.UrlDisplay, ss => ss.ID);
            return Json(new { returnCode = returnCode }, JsonRequestBehavior.AllowGet);
        }

        [EncryptActionName(Name = ("LoadCate"))]
        [OutputCache(CacheProfile = "Cache1HourVaryByNone")]
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadCate()
        {
            List<MS_VocaCategoriesModels> result = new List<MS_VocaCategoriesModels>();
            MS_VocaCategoriesModels model = new MS_VocaCategoriesModels();
            MS_VocaCategoryDao dao = new MS_VocaCategoryDao();
            int returnCode = dao.SelectData(model, out result);
            //DataSession.VocaCates = result.ToDictionary(ss => ss.UrlDisplay, ss => ss.ID);
            return Json(new { returnCode = returnCode }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult KeepSessionAlive()
        {
            return new JsonResult { Data = "Success" };
        }
    }
}
