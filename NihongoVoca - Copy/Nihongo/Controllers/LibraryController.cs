using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nihongo.Models;
using Nihongo.Dal.Dao;
using Ivs.Core.Common;
using Ivs.Core.Web.Attributes;

namespace Nihongo.Controllers
{
    public class LibraryController : BaseController
    {
        //
        // GET: /Library/

        #region Action

        [ActionName("danh-sach")]
        public ActionResult Index()
        {
            string s1 = CommonMethod.EncodeUrl("SelectVocaSetsData");
            string s2 = CommonMethod.EncodeUrl("SelectMostPopularVocaSetsData");
            string s3 = CommonMethod.EncodeUrl("SelectFreeVocaSetsData");
            string s4 = CommonMethod.EncodeUrl("SelectKanjiVocaSetsData");
            return View("Index");
        }

        [ActionName("bo-tu-vung")]
        public ActionResult VocaCate(int id, string urlDisplay)
        {
            MS_VocaSetsModels result = new MS_VocaSetsModels();
            MS_VocaSetsDao dao = new MS_VocaSetsDao();
            int returnCode = dao.SelectVocaSetByID(id, out result);
            if (result != null)
            {
                ViewBag.RemainDays = result.RemainDays;
            }

            TempData["VocaSetID"] = id;
            TempData["VocaSetUrlDisplay"] = urlDisplay;

            return View("VocaCate", result);
        }

        [ActionName("danh-muc")]
        public ActionResult Voca(int id, string urlDisplay)
        {
            ViewBag.VocaID = id;
            ViewBag.VocaUrlDisplay = urlDisplay;

            //if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            //{
            //    return RedirectToAction("RequireLogin", "Account");
            //}
            int returnCode = 0;
            MS_VocaCategoryDao dao = new MS_VocaCategoryDao();
            MS_VocaCategoriesModels result = new MS_VocaCategoriesModels();
            bool isOK = false;
            #region Kiem tra da mua chua

            //List<MS_VocaSetsModels> vocaSets = new List<MS_VocaSetsModels>();
            //returnCode = dao.CheckHasBuyVocaSet(id, out vocaSets);

            //if (returnCode != 0 || vocaSets == null || vocaSets.Count == 0)
            //{
            //    return RedirectToAction("danh-sach", "Library");
            //}
            //else
            //{
            //    var vocaSet = vocaSets.FirstOrDefault();
            //    decimal fee = CommonMethod.ParseDecimal(vocaSet.Fee);
            //    if (fee > 0 && !vocaSets.Any(ss => ss.RemainDays > 0))
            //    {
            //        return RedirectToAction("bo", "Payment", new { urlDisplay = vocaSet.UrlDisplay });
            //    }
            //}

            #endregion

            #region Kiem tra bai trc da hoan tat chua

            //returnCode = dao.CheckCompletedPreCate(id, CommonMethod.ParseString(Session["UserID"]), out isOK);
            //if (!isOK)
            //{
            //    ViewBag.Message = "Bộ tự vựng này yêu cầu bạn phải học thuộc và hoàn thành tuần tự các bài kiểm tra trước.";
            //    ViewBag.ReturnUrl = HttpContext.Request.Url.AbsoluteUri;
            //    return View("Error");
            //}
            #endregion

            returnCode = dao.SelectVocaCateByID(id, out result);

            return View("Voca", result);
        }

        [ActionName("so-tay-tu-vung")]
        public ActionResult Notebook()
        {
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                return RedirectToAction("RequireLogin", "Account");
            }

            return View("Notebook");
        }

        [ActionName("hoc-thu")]
        public ActionResult TrialVoca(int id, string urlDisplay)
        {
            ViewBag.VocaID = id;
            ViewBag.VocaUrlDisplay = urlDisplay;

            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                return RedirectToAction("RequireLogin", "Account");
            }
            int returnCode = 0;
            MS_VocaCategoryDao dao = new MS_VocaCategoryDao();
            MS_VocaCategoriesModels result = new MS_VocaCategoriesModels();
            returnCode = dao.SelectVocaCateByID(id, out result);
            return View("TrialVoca", result);
        }

        [ActionName("hoc-tu-vung")]
        public ActionResult Learning(int id, string urlDisplay)
        {
            ViewBag.CategoryID = id;
            ViewBag.CategoryUrlDisplay = urlDisplay;

            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                return RedirectToAction("RequireLogin", "Account");
            }
            //List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            //MS_UserVocabulariesModels result = new MS_UserVocabulariesModels();
            //MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
            //int returnCode = dao.SelectUserVocaData(id, "chkien", out results);
            //MS_VocaCategoryDao dao = new MS_VocaCategoryDao();
            //MS_VocaCategoriesModels result = new MS_VocaCategoriesModels();
            //int returnCode = dao.SelectVocaCateByID(id, out result);
            //List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            MS_UserVocabulariesModels model = new MS_UserVocabulariesModels();
            MS_VocaCategoryDao dao = new MS_VocaCategoryDao();
            MS_VocaSetsModels result = new MS_VocaSetsModels();
            int returnCode = dao.SelectVocaSetByID(id, CommonMethod.ParseInt(Session["UserID"]), out result);
            //model.HasLearnt = CommonData.Status.Disable;
            //using (MS_UserVocabularyDao dao = new MS_UserVocabularyDao())
            //{
            //    int returnCode = dao.SelectSessionUserVocaData(model, out results);
            //}

            return View("LearningSession2", result);
        }

        [ActionName("hoc-so-tay")]
        public ActionResult LearningNotebook()
        {
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                return RedirectToAction("RequireLogin", "Account");
            }
            
            return View("LearningNotebook");
        }

        [ActionName("luyen-tap")]
        public ActionResult Practice(int id, string urlDisplay)
        {
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                return RedirectToAction("RequireLogin", "Account");
            }
            ViewBag.CategoryID = id;
            ViewBag.CategoryUrlDisplay = urlDisplay;
            MS_VocaCategoryDao dao = new MS_VocaCategoryDao();
            MS_VocaCategoriesModels result = new MS_VocaCategoriesModels();
            int returnCode = dao.SelectVocaCateByID(id, out result);

            return View("Practice", result);
        }

        [ActionName("luyen-tap-so-tay")]
        public ActionResult PracticeNotebook()
        {
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                return RedirectToAction("RequireLogin", "Account");
            }
            return View("PracticeNotebook");
        }

        [OutputCache(CacheProfile = "Cache1HourVaryByID")]
        //[OutputCache(CacheProfile = "Cache1HourVaryByID")]
        [ActionName("kiem-tra")]
        public ActionResult Test(int id, string urlDisplay)
        {
            ViewBag.CategoryID = id;
            ViewBag.CategoryUrlDisplay = urlDisplay;

            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                return RedirectToAction("RequireLogin", "Account");
            }
            //List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            //MS_UserVocabulariesModels result = new MS_UserVocabulariesModels();
            //MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
            //int returnCode = dao.SelectUserVocaData(id, "chkien", out results);
            MS_VocaCategoriesModels result = new MS_VocaCategoriesModels();
            MS_VocaCategoryDao dao = new MS_VocaCategoryDao();
            int returnCode = dao.SelectVocaCateByID(id, out result);
            return View("Test", result);
        }

        //[OutputCache(CacheProfile = "Cache1HourVaryByID")]
        [ActionName("kiem-tra-nhanh")]
        public ActionResult FastTest(int id, string urlDisplay)
        {
            ViewBag.CategoryID = id;
            ViewBag.CategoryUrlDisplay = urlDisplay;

            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                return RedirectToAction("RequireLogin", "Account");
            }

            int returnCode = 0;
            MS_VocaCategoryDao dao = new MS_VocaCategoryDao();
            MS_VocaCategoriesModels result = new MS_VocaCategoriesModels();
            bool isOK = false;
            #region Kiem tra da mua chua

            List<MS_VocaSetsModels> vocaSets = new List<MS_VocaSetsModels>();
            returnCode = dao.CheckHasBuyVocaSet(id, out vocaSets);

            if (returnCode != 0 || vocaSets == null || vocaSets.Count == 0)
            {
                return RedirectToAction("danh-sach", "Library");
            }
            else
            {
                var vocaSet = vocaSets.FirstOrDefault();
                decimal fee = CommonMethod.ParseDecimal(vocaSet.Fee);
                if (fee > 0 && !vocaSets.Any(ss => ss.RemainDays > 0))
                {
                    return RedirectToAction("bo", "Payment", new { urlDisplay = vocaSet.UrlDisplay });
                }
            }

            #endregion

            #region Kiem tra bai trc da hoan tat chua

            returnCode = dao.CheckCompletedPreCate(id, CommonMethod.ParseInt(Session["UserID"]), out isOK);
            if (!isOK)
            {
                ViewBag.Message = "Bộ tự vựng này yêu cầu bạn phải học thuộc và hoàn thành tuần tự các bài kiểm tra trước.";
                ViewBag.ReturnUrl = HttpContext.Request.Url.AbsoluteUri;
                return View("Error");
            }
            #endregion

            returnCode = dao.SelectVocaCateByID(id, out result);
            return View("FastTest", result);
        }

        //[OutputCache(CacheProfile = "Cache1HourVaryByID")]
        [ActionName("kiem-tra-thu-nhanh")]
        public ActionResult TrialFastTest(int id, string urlDisplay)
        {
            ViewBag.CategoryID = id;
            ViewBag.CategoryUrlDisplay = urlDisplay;

            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                return RedirectToAction("RequireLogin", "Account");
            }

            int returnCode = 0;
            MS_VocaCategoryDao dao = new MS_VocaCategoryDao();
            MS_VocaCategoriesModels result = new MS_VocaCategoriesModels();
            returnCode = dao.SelectVocaCateByID(id, out result);
            return View("TrialFastTest", result);
        }

        //[OutputCache(CacheProfile = "Cache1HourVaryByNone")]
        public ActionResult Drawing()
        {
            return View();
        }

        #endregion

        #region Partial

        #region Voca set

        //[EncryptedActionParameter]
        //[OutputCache(CacheProfile = "Cache1HourVaryByNone")]
        [EncryptActionName(Name = ("SelectVocaSetsData"))]
        public ActionResult SelectVocaSetsData()
        {
            List<MS_VocaSetsModels> result = new List<MS_VocaSetsModels>();
            MS_VocaSetsDao dao = new MS_VocaSetsDao();
            MS_VocaSetsModels model = new MS_VocaSetsModels();
            int returnCode = dao.SelectData(model, out result);
            return PartialView("_VocaSetPartial", result);
        }

        [EncryptActionName(Name = ("SelectMostPopularVocaSetsData"))]
        [OutputCache(CacheProfile = "Cache5MinutesVaryByNone")]
        public ActionResult SelectMostPopularVocaSetsData()
        {
            List<MS_VocaSetsModels> result = new List<MS_VocaSetsModels>();
            MS_VocaSetsDao dao = new MS_VocaSetsDao();
            MS_VocaSetsModels model = new MS_VocaSetsModels();
            int returnCode = dao.SelectMostPopularVocaSetsData(model, out result);
            return PartialView("_VocaSetPartial", result);
        }

        [ChildActionOnly]
        public ActionResult SelectPopularVocaSet()
        {
            List<MS_VocaSetsModels> result = new List<MS_VocaSetsModels>();
            MS_VocaSetsDao dao = new MS_VocaSetsDao();
            MS_VocaSetsModels model = new MS_VocaSetsModels();
            int returnCode = dao.SelectMostPopularVocaSetsData(model, out result);
            return PartialView("_PopularVocaSetPartial", result);
        }

        [EncryptActionName(Name = ("SelectFreeVocaSetsData"))]
        [OutputCache(CacheProfile = "Cache1HourVaryByNone")]
        public ActionResult SelectFreeVocaSetsData()
        {
            List<MS_VocaSetsModels> result = new List<MS_VocaSetsModels>();
            MS_VocaSetsDao dao = new MS_VocaSetsDao();
            MS_VocaSetsModels model = new MS_VocaSetsModels();
            int returnCode = dao.SelectFreeVocaSetsData(model, out result);
            return PartialView("_VocaSetPartial", result);
        }

        [EncryptActionName(Name = ("SelectKanjiVocaSetsData"))]
        [OutputCache(CacheProfile = "Cache1HourVaryByNone")]
        public ActionResult SelectKanjiVocaSetsData()
        {
            List<MS_VocaSetsModels> result = new List<MS_VocaSetsModels>();
            MS_VocaSetsDao dao = new MS_VocaSetsDao();
            MS_VocaSetsModels model = new MS_VocaSetsModels();
            int returnCode = dao.SelectKanjiVocaSetsData(model, out result);
            return PartialView("_VocaSetPartial", result);
        }

        #endregion

        #region Voca cate

        [EncryptActionName(Name = ("SelectVocaCategoryBySet"))]
        [OutputCache(CacheProfile = "Cache5MinutesVaryByIDClient")]
        public ActionResult SelectVocaCategoryBySet(int id)
        {
            List<MS_VocaCategoriesModels> result = new List<MS_VocaCategoriesModels>();
            MS_VocaCategoriesModels model = new MS_VocaCategoriesModels();
            model.VocaSetID = id;
            MS_VocaCategoryDao dao = new MS_VocaCategoryDao();
            int returnCode = dao.SelectVocaCategoryBySet(model, out result);
            return PartialView("_VocaCategoryPartial", result);
        }

        //[OutputCache(CacheProfile = "Cache1HourVaryByID")]
        [ChildActionOnly]
        public ActionResult CheckCompletedCate(int id)
        {
            bool isOK = false;
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_VocaCategoryDao dao = new MS_VocaCategoryDao();
                returnCode = dao.CheckCompletedCate(id, CommonMethod.ParseInt(Session["UserID"]), out isOK);
            }
            return Content(isOK ? "Đã hoàn thành" : "Chưa hoàn thành", "text/html");
        }

        [ChildActionOnly]
        public ActionResult SelectOtherVocaCategoryData(int id)
        {
            List<MS_VocaCategoriesModels> result = new List<MS_VocaCategoriesModels>();
            MS_VocaCategoriesModels model = new MS_VocaCategoriesModels();
            model.VocaSetID = id;
            MS_VocaCategoryDao dao = new MS_VocaCategoryDao();
            int returnCode = dao.SelectVocaCategoryBySet(model, out result);
            return PartialView("_OtherVocaCategoryPartial", result);
        }

        #endregion

        #region Voca

        [OutputCache(CacheProfile = "Cache5MinutesVaryByIDClient")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTopCompleted(int id)
        {
            List<MS_TestResultModels> results = new List<MS_TestResultModels>();
            int returnCode = 0;
            MS_TestResultModels model = new MS_TestResultModels();
            model.CategoryID = id;
            MS_TestResultDao dao = new MS_TestResultDao();
            returnCode = dao.SelectTopCompleted(model, out results);

            return Json(new { members = results }, JsonRequestBehavior.AllowGet);
        }

        [EncryptActionName(Name = ("SelectVocasByCate"))]
        [OutputCache(CacheProfile = "Cache1MinuteVaryByIDClient")]
        public ActionResult SelectVocasByCate(int id)
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
            int returnCode = dao.SelectUserVocaData(id, CommonMethod.ParseInt(Session["UserID"]), out results);

            if (results.Count > 0 && results.FirstOrDefault().IsKanji == CommonData.Status.Enable)
            {
                return PartialView("_VocaKanjiPartial", results);
            }
            else
            {
                return PartialView("_VocaPartial", results);
            }
        }

        [EncryptActionName(Name = ("SelectNotebookVocas"))]
        //[OutputCache(CacheProfile = "Cache1MinuteVaryByIDClient")]
        public ActionResult SelectNotebookVocas()
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
            int returnCode = dao.SelectNotebookVocas(CommonMethod.ParseInt(Session["UserID"]), out results);

            return PartialView("_NotebookVocaPartial", results);
        }

        [EncryptActionName(Name = ("GetPracticeSessionVocas"))]
        [OutputCache(CacheProfile = "Cache1MinuteVaryByIDClient")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPracticeSessionVocas(int id)
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                //MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                //returnCode = dao.SelectUserVocaData(id, CommonMethod.ParseString(Session["UserID"]), out results);
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                MS_UserVocabulariesModels model = new MS_UserVocabulariesModels();
                model.VocaSetID = id;
                //model.CategoryID = id;
                model.Type = CommonData.VocaType.Word;
                model.UserID = CommonMethod.ParseInt(Session["UserID"]);
                //model.HasLearnt = CommonData.Status.Disable;
                returnCode = dao.SelectPracticeSessionUserVocaData(model, out results);
            }

            return Json(new { vocabularies = ((results)) }, JsonRequestBehavior.AllowGet);
        }

        [EncryptActionName(Name = ("GetSessionVocas"))]
        [OutputCache(CacheProfile = "Cache1MinuteVaryByIDClient")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetSessionVocas(int id)
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                //MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                //returnCode = dao.SelectUserVocaData(id, CommonMethod.ParseString(Session["UserID"]), out results);
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                MS_UserVocabulariesModels model = new MS_UserVocabulariesModels();
                model.VocaSetID = id;
                //model.CategoryID = id;
                model.Type = CommonData.VocaType.Word;
                model.UserID = CommonMethod.ParseInt(Session["UserID"]);
                //model.HasLearnt = CommonData.Status.Disable;
                returnCode = dao.SelectSessionUserVocaData(model, out results);
            }

            return Json(new { vocabularies = ((results)) }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Pratice

        [EncryptActionName(Name = ("GetVocas"))]
        //[OutputCache(CacheProfile = "Cache5MinutesVaryByIDClient")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetVocas(int id, string type)
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                MS_UserVocabulariesModels model = new MS_UserVocabulariesModels()
                {
                    CategoryID = id,
                    UserID = CommonMethod.ParseInt(Session["UserID"]),
                    //2: weak
                    HasLearnt = type == "2" ? CommonData.StringEmpty : type,
                    VocaGetType = type == "2"? type : CommonData.StringEmpty,
                };
                returnCode = dao.SelectUserVocaData(model, out results);
            }

            return Json(new { vocabularies = (results), returnCode = returnCode, numOfHasLearnt = results.Count(ss => ss.HasLearnt == CommonData.Status.Enable) }
                , JsonRequestBehavior.AllowGet);
        }

        [EncryptActionName(Name = ("GetNoteboolVocas"))]
        //[OutputCache(CacheProfile = "Cache5MinutesVaryByIDClient")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetNoteboolVocas()
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                returnCode = dao.SelectNotebookVocas(CommonMethod.ParseInt(Session["UserID"]), out results);
            }

            return Json(new { vocabularies = (results), returnCode = returnCode, numOfHasLearnt = results.Count(ss => ss.HasLearnt == CommonData.Status.Enable) }
                , JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(CacheProfile = "Cache5MinutesVaryByIDClient")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CheckCompletedPreCate(int id)
        {
            bool isOK = false;
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_VocaCategoryDao dao = new MS_VocaCategoryDao();
                returnCode = dao.CheckCompletedPreCate(id, CommonMethod.ParseInt(Session["UserID"]), out isOK);
            }

            return Json(new { isOK = isOK, returnCode = returnCode }, JsonRequestBehavior.AllowGet);
        }

        [EncryptActionName(Name = ("GetReadingVocas"))]
        [OutputCache(CacheProfile = "Cache1MinuteVaryByIDClient")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetReadingVocas(int id, string type)
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                MS_UserVocabulariesModels model = new MS_UserVocabulariesModels()
                {
                    CategoryID = id,
                    UserID = CommonMethod.ParseInt(Session["UserID"]),
                    //2: weak
                    HasLearnt = type == "2" ? CommonData.StringEmpty : type,
                    VocaGetType = type == "2" ? type : CommonData.StringEmpty,
                    Type = CommonData.VocaType.Word,
                };
                returnCode = dao.SelectUserVocaData(model, out results);
            }

            return Json(new { vocabularies = RandomList(results, results.Count), returnCode = returnCode }, JsonRequestBehavior.AllowGet);
        }

        [EncryptActionName(Name = ("GetChoosingVocas"))]
        [OutputCache(CacheProfile = "Cache1MinuteVaryByIDClient")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetChoosingVocas(int id, string type)
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                MS_UserVocabulariesModels model = new MS_UserVocabulariesModels()
                {
                    CategoryID = id,
                    UserID = CommonMethod.ParseInt(Session["UserID"]),
                    //2: weak
                    HasLearnt = type == "2" ? CommonData.StringEmpty : type,
                    VocaGetType = type == "2" ? type : CommonData.StringEmpty,
                    Type = CommonData.VocaType.Word,
                };
                returnCode = dao.SelectUserVocaData(model, out results);
            }

            return Json(new { vocabularies = RandomList(CreateChoosingList(results), results.Count), returnCode = returnCode }, JsonRequestBehavior.AllowGet);
        }

        [EncryptActionName(Name = ("GetTranslatingVocas"))]
        [OutputCache(CacheProfile = "Cache1MinuteVaryByIDClient")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTranslatingVocas(int id, string type)
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                MS_UserVocabulariesModels model = new MS_UserVocabulariesModels()
                {
                    CategoryID = id,
                    UserID = CommonMethod.ParseInt(Session["UserID"]),
                    //2: weak
                    HasLearnt = type == "2" ? CommonData.StringEmpty : type,
                    VocaGetType = type == "2" ? type : CommonData.StringEmpty,
                    Type = CommonData.VocaType.Word,
                };
                returnCode = dao.SelectUserVocaData(model, out results);
            }

            return Json(new { vocabularies = RandomList(CreateTranslatingList(results), results.Count), returnCode = returnCode }, JsonRequestBehavior.AllowGet);
        }

        [EncryptActionName(Name = ("GetNotebookReadingVocas"))]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetNotebookReadingVocas()
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                returnCode = dao.SelectNotebookVocas(CommonMethod.ParseInt(Session["UserID"]), out results);
            }

            return Json(new { vocabularies = RandomList(results, results.Count), returnCode = returnCode }, JsonRequestBehavior.AllowGet);
        }

        [EncryptActionName(Name = ("GetNotebookChoosingVocas"))]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetNotebookChoosingVocas()
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                returnCode = dao.SelectNotebookVocas(CommonMethod.ParseInt(Session["UserID"]), out results);
            }

            return Json(new { vocabularies = RandomList(CreateChoosingList(results), results.Count), returnCode = returnCode }, JsonRequestBehavior.AllowGet);
        }

        [EncryptActionName(Name = ("GetNotebookTranslatingVocas"))]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetNotebookTranslatingVocas()
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                returnCode = dao.SelectNotebookVocas(CommonMethod.ParseInt(Session["UserID"]), out results);
            }

            return Json(new { vocabularies = RandomList(CreateTranslatingList(results), results.Count), returnCode = returnCode }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Choosing popup

        [EncryptActionName(Name = ("GetChoosingVocabularies"))]
        //[OutputCache(CacheProfile = "Cache1HourVaryByIDClient")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetChoosingVocabularies(int id)
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                returnCode = dao.SelectUserVocaData(id, CommonMethod.ParseInt(Session["UserID"]), out results);
            }

            return PartialView("_VocaWordListPartial", results);
            //return Json(new { vocabularies = (results), returnCode = returnCode }, JsonRequestBehavior.AllowGet);
        }

        [EncryptActionName(Name = ("GetNotebookVocabularies"))]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetNotebookVocabularies()
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                returnCode = dao.SelectNotebookVocas(CommonMethod.ParseInt(Session["UserID"]), out results);
            }

            return PartialView("_VocaWordListPartial", results);
            //return Json(new { vocabularies = (results), returnCode = returnCode }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Test
        [EncryptActionName(Name = ("GetTestVocas"))]
        [OutputCache(CacheProfile = "Cache5MinutesVaryByIDClient")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTestVocas(int id)
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                //MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                //returnCode = dao.SelectUserVocaData(id, CommonMethod.ParseString(Session["UserID"]), out results);
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                MS_UserVocabulariesModels model = new MS_UserVocabulariesModels();
                model.CategoryID = id;
                model.Type = CommonData.VocaType.Word;
                model.UserID = CommonMethod.ParseInt(Session["UserID"]);
                //model.HasLearnt = CommonData.Status.Disable;
                returnCode = dao.SelectUserVocaData(model, out results);
            }

            return Json(new { vocabularies = (CreateTestList(results)) }, JsonRequestBehavior.AllowGet);
        }

        [EncryptActionName(Name = ("GetFastTestVocas"))]
        [OutputCache(CacheProfile = "Cache5MinutesVaryByIDClient")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetFastTestVocas(int id)
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                MS_UserVocabulariesModels model = new MS_UserVocabulariesModels();
                model.CategoryID = id;
                model.Type = CommonData.VocaType.Word;
                model.UserID = CommonMethod.ParseInt(Session["UserID"]);
                //model.HasLearnt = CommonData.Status.Disable;
                returnCode = dao.SelectUserVocaData(model, out results);
            }

            return Json(new { vocabularies = (CreateFastTestList(results)) }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Post

        [EncryptActionName(Name = ("UpdateHasMarked"))]
        [HttpPost]
        public ActionResult UpdateHasMarked(MS_UserVocabulariesModels voca)
        {
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                returnCode = dao.UpdateHasMarked(voca);
            }

            return Json(new { ReturnCode = returnCode });
        }

        [EncryptActionName(Name = ("UpdateUserVocas"))]
        [HttpPost]
        public ActionResult UpdateUserVocas(List<MS_UserVocabulariesModels> vocas)
        {
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                returnCode = dao.UpdateUserVocas(CommonMethod.ParseInt(Session["UserID"]), vocas);
            }

            return Json(new { ReturnCode = returnCode });
        }

        [EncryptActionName(Name = ("UpdateTestResult"))]
        [HttpPost]
        public ActionResult UpdateTestResult(List<MS_UserVocabulariesModels> vocas)
        {
            int returnCode = 0;
            int accumulatedPoint = 1;
            if (!CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                returnCode = dao.UpdateTestResult(vocas, out accumulatedPoint);
            }

            return Json(new { ReturnCode = returnCode, accumulatedPoint = accumulatedPoint });
        }


        [EncryptActionName(Name = ("UpdateSessionResult"))]
        [HttpPost]
        public ActionResult UpdateSessionResult(List<MS_UserVocabulariesModels> vocas)
        {
            int returnCode = 0;
            if (!CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                returnCode = dao.UpdateSessionResult(CommonMethod.ParseInt(Session["UserID"]), vocas);
            }

            return Json(new { ReturnCode = returnCode });
        }

        [EncryptActionName(Name = ("UpdateFastTestVoca"))]
        [HttpPost]
        public ActionResult UpdateFastTestVoca(MS_UserVocabulariesModels voca)
        {
            int returnCode = 0;
            if (!CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                returnCode = dao.UpdateFastTestVoca(voca);
            }

            return Json(new { ReturnCode = returnCode });
        }

        #endregion

        #region Methods

        private List<MS_UserVocabulariesModels> RandomList(List<MS_UserVocabulariesModels> list, int length)
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();

            int numOfWords = list.Count;
            while (list.Count > 0)
            {
                //get random position
                Random random = new Random();
                int number = random.Next(list.Count);

                //add the selected item to result list
                results.Add(list[number]);

                //remove the selected item
                list.RemoveAt(number);
            }
            return results;
        }

        private List<MS_UserVocabulariesModels> CreateFastTestList(List<MS_UserVocabulariesModels> results)
        {
            //choosing
            List<MS_UserVocabulariesModels> choosingList = results;
            for (int i = 0; i < choosingList.Count; i++)
            {
                choosingList[i].TestType = "1";
            }
            choosingList = CreateChoosingList(choosingList);

            return choosingList.Where(ss => ss.HasLearnt == CommonData.Status.Disable).ToList();
        }

        private List<MS_UserVocabulariesModels> CreateTestList(List<MS_UserVocabulariesModels> list)
        {
            //create random list
            var results = RandomList(list, list.Count);

            //choosing
            int halfList = results.Count / 2;
            List<MS_UserVocabulariesModels> choosingList = new List<MS_UserVocabulariesModels>();
            for (int i = 0; i < halfList; i++)
            {
                results[i].TestType = "1";
                choosingList.Add(results[i]);
            }
            choosingList = CreateChoosingList(choosingList);
            int halfChoosingList = choosingList.Count / 3;
            for (int i = 0; i < halfChoosingList; i++)
            {
                choosingList[i].TestSkill = CommonData.LanguageSkill.Reading;
            }
            for (int i = halfChoosingList; i < (halfChoosingList * 2); i++)
            {
                choosingList[i].TestSkill = CommonData.LanguageSkill.Translating;
            }
            for (int i = (halfChoosingList * 2); i < choosingList.Count; i++)
            {
                choosingList[i].TestSkill = CommonData.LanguageSkill.Listening;
            }

            //input romaji           
            List<MS_UserVocabulariesModels> writingList = new List<MS_UserVocabulariesModels>();
            for (int i = halfList; i < results.Count; i++)
            {
                results[i].TestType = "2";
                writingList.Add(results[i]);
            }
            int halfWritingList = writingList.Count / 3;
            for (int i = 0; i < halfWritingList; i++)
            {
                writingList[i].TestSkill = CommonData.LanguageSkill.Reading;
            }
            for (int i = halfWritingList; i < (halfWritingList * 2); i++)
            {
                writingList[i].TestSkill = CommonData.LanguageSkill.Translating;
            }
            for (int i = (halfWritingList * 2); i < writingList.Count; i++)
            {
                writingList[i].TestSkill = CommonData.LanguageSkill.Listening;
            }

            choosingList.AddRange(writingList);
            return choosingList;
        }

        private List<MS_UserVocabulariesModels> CreateChoosingList(List<MS_UserVocabulariesModels> list)
        {
            if (list.Count < 4)
            {
                return list;
            }
            List<MS_UserVocabulariesModels> results = list;
            Random random = new Random();
            Random r2 = new Random();
            Random r3 = new Random();
            Random r4 = new Random();
            int number = 0;
            int n2 = 0;
            int n3 = 0;
            int n4 = 0;
            foreach (var item in results)
            {
                //tìm vị trí đặt kết quả đúng (1 -> 4)
                number = random.Next(0, 5);
                switch (number)
                {
                    case 1:
                        item.CorrectResult = 1;
                        item.CorrectUrlAudio = item.UrlAudio;
                        item.Hiragana1 = item.Hiragana;
                        item.Katakana1 = item.Katakana;
                        item.Kanji1 = item.Kanji;
                        item.Result1 = item.DisplayType == "3"
                                        ? item.Kanji
                                        : (CommonMethod.IsNullOrEmpty(item.Hiragana) ? CommonData.StringEmpty : item.Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(item.Katakana) ? CommonData.StringEmpty : " 。 " + item.Katakana);
                        item.ResultUrlAudio1 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Hiragana2 = results[n2].Hiragana;
                        item.Katakana2 = results[n2].Katakana;
                        item.Kanji2 = results[n2].Kanji;
                        item.Result2 = results[n2].DisplayType == "3" ? results[n2].Kanji
                                        : (CommonMethod.IsNullOrEmpty(results[n2].Hiragana) ? CommonData.StringEmpty : results[n2].Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(results[n2].Katakana) ? CommonData.StringEmpty : " 。 " + results[n2].Katakana);
                        item.ResultUrlAudio2 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Hiragana3 = results[n3].Hiragana;
                        item.Katakana3 = results[n3].Katakana;
                        item.Kanji3 = results[n3].Kanji;
                        item.Result3 = results[n3].DisplayType == "3" ? results[n3].Kanji
                                        : (CommonMethod.IsNullOrEmpty(results[n3].Hiragana) ? CommonData.StringEmpty : results[n3].Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(results[n3].Katakana) ? CommonData.StringEmpty : " 。 " + results[n3].Katakana);
                        item.ResultUrlAudio3 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Hiragana4 = results[n4].Hiragana;
                        item.Katakana4 = results[n4].Katakana;
                        item.Kanji4 = results[n4].Kanji;
                        item.Result4 = results[n4].DisplayType == "3" ? results[n4].Kanji
                                        : (CommonMethod.IsNullOrEmpty(results[n4].Hiragana) ? CommonData.StringEmpty : results[n4].Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(results[n4].Katakana) ? CommonData.StringEmpty : " 。 " + results[n4].Katakana);
                        item.ResultUrlAudio4 = results[n4].UrlAudio;
                        break;
                    case 2:
                        item.CorrectResult = 2;
                        item.CorrectUrlAudio = item.UrlAudio;
                        item.Hiragana2 = item.Hiragana;
                        item.Katakana2 = item.Katakana;
                        item.Kanji2 = item.Kanji;
                        item.Result2 = item.DisplayType == "3" ? item.Kanji
                                        : (CommonMethod.IsNullOrEmpty(item.Hiragana) ? CommonData.StringEmpty : item.Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(item.Katakana) ? CommonData.StringEmpty : " 。 " + item.Katakana);
                        item.ResultUrlAudio2 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Hiragana1 = results[n2].Hiragana;
                        item.Katakana1 = results[n2].Katakana;
                        item.Kanji1 = results[n2].Kanji;
                        item.Result1 = results[n2].DisplayType == "3" ? results[n2].Kanji
                                        : (CommonMethod.IsNullOrEmpty(results[n2].Hiragana) ? CommonData.StringEmpty : results[n2].Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(results[n2].Katakana) ? CommonData.StringEmpty : " 。 " + results[n2].Katakana);
                        item.ResultUrlAudio1 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Hiragana3 = results[n3].Hiragana;
                        item.Katakana3 = results[n3].Katakana;
                        item.Kanji3 = results[n3].Kanji;
                        item.Result3 = results[n3].DisplayType == "3" ? results[n3].Kanji
                                        : (CommonMethod.IsNullOrEmpty(results[n3].Hiragana) ? CommonData.StringEmpty : results[n3].Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(results[n3].Katakana) ? CommonData.StringEmpty : " 。 " + results[n3].Katakana);
                        item.ResultUrlAudio3 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Hiragana4 = results[n4].Hiragana;
                        item.Katakana4 = results[n4].Katakana;
                        item.Kanji4 = results[n4].Kanji;
                        item.Result4 = results[n4].DisplayType == "3" ? results[n4].Kanji
                                        : (CommonMethod.IsNullOrEmpty(results[n4].Hiragana) ? CommonData.StringEmpty : results[n4].Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(results[n4].Katakana) ? CommonData.StringEmpty : " 。 " + results[n4].Katakana);
                        item.ResultUrlAudio4 = results[n4].UrlAudio;
                        break;
                    case 3:
                        item.CorrectResult = 3;
                        item.CorrectUrlAudio = item.UrlAudio;
                        item.Hiragana3 = item.Hiragana;
                        item.Katakana3 = item.Katakana;
                        item.Kanji3 = item.Kanji;
                        item.Result3 = item.DisplayType == "3" ? item.Kanji
                                        : (CommonMethod.IsNullOrEmpty(item.Hiragana) ? CommonData.StringEmpty : item.Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(item.Katakana) ? CommonData.StringEmpty : " 。 " + item.Katakana);
                        item.ResultUrlAudio3 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Hiragana1 = results[n2].Hiragana;
                        item.Katakana1 = results[n2].Katakana;
                        item.Kanji1 = results[n2].Kanji;
                        item.Result1 = results[n2].DisplayType == "3" ? results[n2].Kanji
                                        : (CommonMethod.IsNullOrEmpty(results[n2].Hiragana) ? CommonData.StringEmpty : results[n2].Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(results[n2].Katakana) ? CommonData.StringEmpty : " 。 " + results[n2].Katakana);
                        item.ResultUrlAudio1 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Hiragana2 = results[n3].Hiragana;
                        item.Katakana2 = results[n3].Katakana;
                        item.Kanji2 = results[n3].Kanji;
                        item.Result2 = results[n3].DisplayType == "3" ? results[n3].Kanji
                                        : (CommonMethod.IsNullOrEmpty(results[n3].Hiragana) ? CommonData.StringEmpty : results[n3].Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(results[n3].Katakana) ? CommonData.StringEmpty : " 。 " + results[n3].Katakana);
                        item.ResultUrlAudio2 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Hiragana4 = results[n4].Hiragana;
                        item.Katakana4 = results[n4].Katakana;
                        item.Kanji4 = results[n4].Kanji;
                        item.Result4 = results[n4].DisplayType == "3" ? results[n4].Kanji
                                        : (CommonMethod.IsNullOrEmpty(results[n4].Hiragana) ? CommonData.StringEmpty : results[n4].Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(results[n4].Katakana) ? CommonData.StringEmpty : " 。 " + results[n4].Katakana);
                        item.ResultUrlAudio4 = results[n4].UrlAudio;
                        break;
                    case 4:
                        item.CorrectResult = 4;
                        item.CorrectUrlAudio = item.UrlAudio;
                        item.Hiragana4 = item.Hiragana;
                        item.Katakana4 = item.Katakana;
                        item.Kanji4 = item.Kanji;
                        item.Result4 = item.DisplayType == "3" ? item.Kanji
                                        : (CommonMethod.IsNullOrEmpty(item.Hiragana) ? CommonData.StringEmpty : item.Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(item.Katakana) ? CommonData.StringEmpty : " 。 " + item.Katakana);
                        item.ResultUrlAudio4 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Hiragana1 = results[n2].Hiragana;
                        item.Katakana1 = results[n2].Katakana;
                        item.Kanji1 = results[n2].Kanji;
                        item.Result1 = results[n2].DisplayType == "3" ? results[n2].Kanji
                                        : (CommonMethod.IsNullOrEmpty(results[n2].Hiragana) ? CommonData.StringEmpty : results[n2].Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(results[n2].Katakana) ? CommonData.StringEmpty : " 。 " + results[n2].Katakana);
                        item.ResultUrlAudio1 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Hiragana2 = results[n3].Hiragana;
                        item.Katakana2 = results[n3].Katakana;
                        item.Kanji2 = results[n3].Kanji;
                        item.Result2 = results[n3].DisplayType == "3" ? results[n3].Kanji
                                        : (CommonMethod.IsNullOrEmpty(results[n3].Hiragana) ? CommonData.StringEmpty : results[n3].Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(results[n3].Katakana) ? CommonData.StringEmpty : " 。 " + results[n3].Katakana);
                        item.ResultUrlAudio2 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Hiragana3 = results[n4].Hiragana;
                        item.Katakana3 = results[n4].Katakana;
                        item.Kanji3 = results[n4].Kanji;
                        item.Result3 = results[n4].DisplayType == "3" ? results[n4].Kanji
                                        : (CommonMethod.IsNullOrEmpty(results[n4].Hiragana) ? CommonData.StringEmpty : results[n4].Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(results[n4].Katakana) ? CommonData.StringEmpty : " 。 " + results[n4].Katakana);
                        item.ResultUrlAudio3 = results[n4].UrlAudio;
                        break;
                    default:
                        item.CorrectResult = 1;
                        item.CorrectUrlAudio = item.UrlAudio;
                        item.Hiragana1 = item.Hiragana;
                        item.Katakana1 = item.Katakana;
                        item.Kanji1 = item.Kanji;
                        item.Result1 = item.DisplayType == "3" ? item.Kanji
                                        : (CommonMethod.IsNullOrEmpty(item.Hiragana) ? CommonData.StringEmpty : item.Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(item.Katakana) ? CommonData.StringEmpty : " 。 " + item.Katakana);
                        item.ResultUrlAudio1 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Hiragana2 = results[n2].Hiragana;
                        item.Katakana2 = results[n2].Katakana;
                        item.Kanji2 = results[n2].Kanji;
                        item.Result2 = results[n2].DisplayType == "3" ? results[n2].Kanji
                                        : (CommonMethod.IsNullOrEmpty(results[n2].Hiragana) ? CommonData.StringEmpty : results[n2].Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(results[n2].Katakana) ? CommonData.StringEmpty : " 。 " + results[n2].Katakana);
                        item.ResultUrlAudio2 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Hiragana3 = results[n3].Hiragana;
                        item.Katakana3 = results[n3].Katakana;
                        item.Kanji3 = results[n3].Kanji;
                        item.Result3 = results[n3].DisplayType == "3" ? results[n3].Kanji
                                        : (CommonMethod.IsNullOrEmpty(results[n3].Hiragana) ? CommonData.StringEmpty : results[n3].Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(results[n3].Katakana) ? CommonData.StringEmpty : " 。 " + results[n3].Katakana);
                        item.ResultUrlAudio3 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Hiragana4 = results[n4].Hiragana;
                        item.Katakana4 = results[n4].Katakana;
                        item.Kanji4 = results[n4].Kanji;
                        item.Result4 = results[n4].DisplayType == "3" ? results[n4].Kanji
                                        : (CommonMethod.IsNullOrEmpty(results[n4].Hiragana) ? CommonData.StringEmpty : results[n4].Hiragana)
                                            + (CommonMethod.IsNullOrEmpty(results[n4].Katakana) ? CommonData.StringEmpty : " 。 " + results[n4].Katakana);
                        item.ResultUrlAudio4 = results[n4].UrlAudio;
                        break;
                }
            }

            return results;
        }

        private List<MS_UserVocabulariesModels> CreateTranslatingList(List<MS_UserVocabulariesModels> list)
        {
            if (list.Count < 4)
            {
                return list;
            }
            List<MS_UserVocabulariesModels> results = list;
            Random random = new Random();
            Random r2 = new Random();
            Random r3 = new Random();
            Random r4 = new Random();
            int number = 0;
            int n2 = 0;
            int n3 = 0;
            int n4 = 0;
            foreach (var item in results)
            {
                //tìm vị trí đặt kết quả đúng (1 -> 4)
                number = random.Next(0, 5);
                switch (number)
                {
                    case 1:
                        item.CorrectResult = 1;
                        item.CorrectUrlAudio = item.UrlAudio;
                        item.Result1 = CommonMethod.IsNullOrEmpty(item.Pinyin) ? item.VMeaning : item.Pinyin + " 。 " + item.VMeaning;
                        item.ResultUrlAudio1 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Result2 = CommonMethod.IsNullOrEmpty(results[n2].Pinyin) ? results[n2].VMeaning : results[n2].Pinyin + " 。 " + results[n2].VMeaning;
                        item.ResultUrlAudio2 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Result3 = CommonMethod.IsNullOrEmpty(results[n3].Pinyin) ? results[n3].VMeaning : results[n3].Pinyin + " 。 " + results[n3].VMeaning;
                        item.ResultUrlAudio3 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Result4 = CommonMethod.IsNullOrEmpty(results[n4].Pinyin) ? results[n4].VMeaning : results[n4].Pinyin + " 。 " + results[n4].VMeaning;
                        item.ResultUrlAudio4 = results[n4].UrlAudio;
                        break;
                    case 2:
                        item.CorrectResult = 2;
                        item.CorrectUrlAudio = item.UrlAudio;
                        item.Result2 = CommonMethod.IsNullOrEmpty(item.Pinyin) ? item.VMeaning : item.Pinyin + " 。 " + item.VMeaning;
                        item.ResultUrlAudio2 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Result1 = CommonMethod.IsNullOrEmpty(results[n2].Pinyin) ? results[n2].VMeaning : results[n2].Pinyin + " 。 " + results[n2].VMeaning;
                        item.ResultUrlAudio1 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Result3 = CommonMethod.IsNullOrEmpty(results[n3].Pinyin) ? results[n3].VMeaning : results[n3].Pinyin + " 。 " + results[n3].VMeaning;
                        item.ResultUrlAudio3 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Result4 = CommonMethod.IsNullOrEmpty(results[n4].Pinyin) ? results[n4].VMeaning : results[n4].Pinyin + " 。 " + results[n4].VMeaning;
                        item.ResultUrlAudio4 = results[n4].UrlAudio;
                        break;
                    case 3:
                        item.CorrectResult = 3;
                        item.CorrectUrlAudio = item.UrlAudio;
                        item.Result3 = CommonMethod.IsNullOrEmpty(item.Pinyin) ? item.VMeaning : item.Pinyin + " 。 " + item.VMeaning;
                        item.ResultUrlAudio3 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Result1 = CommonMethod.IsNullOrEmpty(results[n2].Pinyin) ? results[n2].VMeaning : results[n2].Pinyin + " 。 " + results[n2].VMeaning;
                        item.ResultUrlAudio1 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Result2 = CommonMethod.IsNullOrEmpty(results[n3].Pinyin) ? results[n3].VMeaning : results[n3].Pinyin + " 。 " + results[n3].VMeaning;
                        item.ResultUrlAudio2 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Result4 = CommonMethod.IsNullOrEmpty(results[n4].Pinyin) ? results[n4].VMeaning : results[n4].Pinyin + " 。 " + results[n4].VMeaning;
                        item.ResultUrlAudio4 = results[n4].UrlAudio;
                        break;
                    case 4:
                        item.CorrectResult = 4;
                        item.CorrectUrlAudio = item.UrlAudio;
                        item.Result4 = CommonMethod.IsNullOrEmpty(item.Pinyin) ? item.VMeaning : item.Pinyin + " 。 " + item.VMeaning;
                        item.ResultUrlAudio4 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Result1 = CommonMethod.IsNullOrEmpty(results[n2].Pinyin) ? results[n2].VMeaning : results[n2].Pinyin + " 。 " + results[n2].VMeaning;
                        item.ResultUrlAudio1 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Result2 = CommonMethod.IsNullOrEmpty(results[n3].Pinyin) ? results[n3].VMeaning : results[n3].Pinyin + " 。 " + results[n3].VMeaning;
                        item.ResultUrlAudio2 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Result3 = CommonMethod.IsNullOrEmpty(results[n4].Pinyin) ? results[n4].VMeaning : results[n4].Pinyin + " 。 " + results[n4].VMeaning;
                        item.ResultUrlAudio3 = results[n4].UrlAudio;
                        break;
                    default:
                        item.CorrectResult = 1;
                        item.CorrectUrlAudio = item.UrlAudio;
                        item.Result1 = CommonMethod.IsNullOrEmpty(item.Pinyin) ? item.VMeaning : item.Pinyin + " 。 " + item.VMeaning;
                        item.ResultUrlAudio1 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Result2 = CommonMethod.IsNullOrEmpty(results[n2].Pinyin) ? results[n2].VMeaning : results[n2].Pinyin + " 。 " + results[n2].VMeaning;
                        item.ResultUrlAudio2 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Result3 = CommonMethod.IsNullOrEmpty(results[n3].Pinyin) ? results[n3].VMeaning : results[n3].Pinyin + " 。 " + results[n3].VMeaning;
                        item.ResultUrlAudio3 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Result4 = CommonMethod.IsNullOrEmpty(results[n4].Pinyin) ? results[n4].VMeaning : results[n4].Pinyin + " 。 " + results[n4].VMeaning;
                        item.ResultUrlAudio4 = results[n4].UrlAudio;
                        break;
                }
            }

            return results;
        }

        private List<MS_UserVocabulariesModels> CreateSessionLearningList(List<MS_UserVocabulariesModels> list)
        {
            //create random list
            //var results = RandomList(list, list.Count);

            //choosing
            //int halfList = list.Count / 2;
            List<MS_UserVocabulariesModels> choosingList = new List<MS_UserVocabulariesModels>();
            for (int i = 0; i < list.Count; i++)
            {
                list[i].TestType = "1";
                choosingList.Add(list[i]);
            }
            choosingList = CreateChoosingList(choosingList);
            int halfChoosingList = choosingList.Count / 2;
            for (int i = 0; i < halfChoosingList; i++)
            {
                choosingList[i].TestSkill = CommonData.LanguageSkill.Reading;
            }
            for (int i = halfChoosingList; i < choosingList.Count; i++)
            {
                choosingList[i].TestSkill = CommonData.LanguageSkill.Translating;
            }
            //for (int i = (halfChoosingList * 2); i < choosingList.Count; i++)
            //{
            //    choosingList[i].TestSkill = CommonData.LanguageSkill.Listening;
            //}

            //input romaji           
            //List<MS_UserVocabulariesModels> writingList = new List<MS_UserVocabulariesModels>();
            //for (int i = halfList; i < results.Count; i++)
            //{
            //    results[i].TestType = "2";
            //    writingList.Add(results[i]);
            //}
            //int halfWritingList = writingList.Count / 3;
            //for (int i = 0; i < halfWritingList; i++)
            //{
            //    writingList[i].TestSkill = CommonData.LanguageSkill.Reading;
            //}
            //for (int i = halfWritingList; i < (halfWritingList * 2); i++)
            //{
            //    writingList[i].TestSkill = CommonData.LanguageSkill.Translating;
            //}
            //for (int i = (halfWritingList * 2); i < writingList.Count; i++)
            //{
            //    writingList[i].TestSkill = CommonData.LanguageSkill.Listening;
            //}

            //choosingList.AddRange(writingList);
            return choosingList;
        }


        #endregion
    }
}
