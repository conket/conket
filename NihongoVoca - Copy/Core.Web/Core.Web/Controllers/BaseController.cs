using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading;
using System.Globalization;
using System.Collections;
using Ivs.Core.Interface;
using System.Reflection;
using Ivs.Core.Common;
using System.Data;
using Ivs.Core.Data;
using Ivs.BLL.Common;
using Ivs.Core.Web.Attributes;
using Ivs.Resources.Common;
using System.IO;
using System.Web.UI;
using System.ComponentModel;

namespace Ivs.Core.Web.Controllers
{
    public class BaseController : Controller
    {
        //List of model type
        //public static Dictionary<string, Type> ModelList = new Dictionary<string, Type>();
        //Current 

        #region Properties

        int recordsPerPage = 10;
        protected CommonData.Mode Mode { get; set; }
        protected string SEARCH_CONDITION
        {
            get
            {
                return FunctionGrp + "SEARCH_CONDITION";
            }
        }

        protected string SEARCH_RESULT
        {
            get
            {
                return FunctionGrp + "SEARCH_RESULT";
            }
        }

        protected virtual string FunctionGrp { get; set; }

        protected virtual IService Bl { get; set; }

        //Be overrided from child controller
        protected virtual IModel Model { get; set; }

        protected virtual string PartialViewName { get; set; }

        public Dictionary<CommonData.ButtonCategory, CommonData.IsAuthority> AuthorityDictionary { get; set; }

        public string CurrentController
        {
            get
            {
                return CommonMethod.ParseString(HttpContext.Request.RequestContext.RouteData.Values["controller"]);
            }
        }

        public string CurrentAction
        {
            get
            {
                return CommonMethod.ParseString(HttpContext.Request.RequestContext.RouteData.Values["action"]);
            }
        }

        public virtual string Layout
        {
            get
            {
                string layout = CommonData.StringEmpty;
                switch (this.Mode)
                {
                    case CommonData.Mode.View:
                    case CommonData.Mode.Edit:
                    case CommonData.Mode.Copy:
                    case CommonData.Mode.New:
                        layout = "_MasterEditLayout";
                        break;
                    case CommonData.Mode.Search:
                        layout = "_MasterSearchLayout";
                        break;
                    default:
                        layout = "_MainLayout";
                        break;
                }
                return layout;
            }
        }

        #endregion

        #region OnActionExecuting

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!ApplicationState.Contain(this.CurrentController) && this.Model != null)
            {
                ApplicationState.SetValue(this.CurrentController, this.Model.GetType());
            }
        }

        #endregion

        #region Method

        protected override void ExecuteCore()
        {
            string language = Session["Language"] == null ? CommonData.CultureInfo.English : CommonMethod.ParseString(Session["Language"]);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);

            //string language = Session["Language"] == null ? DefaultLanguageSelected : Session["Language"].ToString();
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            //ViewBag.ddpLanguage = new SelectList(lstLanguage, LanguageKey.LanuageCode, LanguageKey.LanguageName, language);

            //Set authorization to show/hide buttons
            this.SetAuthorityControl();
            ViewBag.AuthorityDictionary = this.AuthorityDictionary;

            //Set Mode
            this.SetMode();

            //Set path
            ViewBag.SiteMap = this.GetSiteMap();

            base.ExecuteCore();
        }

        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }

        #endregion

        #region Index

        protected virtual ActionResult BaseIndex(int? page)
        {
            //IvsMessage message = null;
            Model = GetSearchModel<IModel>();
            int pageNumber = (page ?? 1);
            //reload condition to show again
            this.SetSearchControlData();

            //search du lieu neu Model khong rong
            List<IModel> lstModels = new List<IModel>();
            if (!CommonMethod.IsNullOrEmpty(Model))
            {
                int returnCode = Bl.SearchData(Model, out lstModels);

                if (returnCode != CommonData.DbReturnCode.Succeed)
                {
                    TempData[CommonData.MessageArea.MsgContent] = CommonMethod.ProcessDbExcetion(returnCode);
                }
            }

            Session[SEARCH_RESULT] = lstModels;

            return View("Index", this.Layout, ConvertToPageList(lstModels, recordsPerPage, pageNumber));
        }

        protected virtual ActionResult BaseIndex(int? page, string sortColumn, string sortOrder, IModel model)
        {
            IvsMessage message = null;
            List<IModel> lstModels = new List<IModel>();
            int returnCode = CommonData.DbReturnCode.Succeed;
            int pageNumber = (page ?? 1);

            //this.Model = Model;
            this.SetSearchModel<IModel>(page, ref model);

            //reload condition to show again
            this.SetSearchControlData();

            returnCode = Bl.SearchData(model, out lstModels);
            if (returnCode == CommonData.DbReturnCode.Succeed)
            {
                if (Request.Form["btnSearch"] != null || Request.Form["btnRefresh"] != null)
                {
                    message = new IvsMessage(CommonConstantMessage.COM_MSG_ROW_FOUND, lstModels.Count.ToString());
                    TempData[CommonData.MessageArea.MsgContent] = message.MessageText;
                }
            }
            else
            {
                TempData[CommonData.MessageArea.MsgContent] = CommonMethod.ProcessDbExcetion(returnCode);
            }

            Session[SEARCH_RESULT] = lstModels;

            //Sorting
            ViewBag.SortOrder = sortOrder;
            ViewBag.SortColumn = sortColumn;
            GridSetting setting = new GridSetting(pageNumber, recordsPerPage, sortColumn, sortOrder);
            lstModels = setting.LoadGridData<IModel>(lstModels, true).ToList();
            return PartialView(this.PartialViewName, ConvertToPageList(lstModels, recordsPerPage, pageNumber));
        }

        #endregion

        #region Add

        protected virtual ActionResult BaseAdd()
        {
            SetEditControlData();
            return View("Edit", this.Layout, this.Model);
        }

        protected virtual ActionResult BaseAdd(IModel model)
        {
            this.Model = model;
            if (ModelState.IsValid)
            {
                IvsMessage message = null;
                int returnCode = Bl.InsertData(this.Model);
                if (returnCode == CommonData.DbReturnCode.Succeed)
                {
                    message = new IvsMessage(CommonConstantMessage.COM_MSG_INSERT_SUCCESSFULLY);
                    TempData[CommonData.MessageArea.MsgContent] = message.MessageText;
                    return RedirectToAction("Index", this.CurrentController);
                    //return Redirect(string.Format("/{0}/{1}", this.CurrentController, "Index"));
                }
                else
                {
                    ProcessDbExcetion(returnCode);
                    //ModelState.AddModelError(CommonData.MessageArea.MsgContent, CommonMethod.ProcessDbExcetion(returnCode));
                    //TempData[CommonData.MessageArea.MsgContent] = CommonMethod.ProcessDbExcetion(returnCode);
                }
            }

            SetEditControlData();
            return View("Edit", this.Layout, this.Model);
        }
        #endregion Add

        #region Edit

        protected virtual ActionResult BaseEdit(int id)
        {
            int returnCode = CommonData.DbReturnCode.Succeed;
            if (!CommonMethod.IsNullOrEmpty(id))
            {
                IModel model = null;
                returnCode = Bl.SearchData(id, out model);
                if (returnCode == 0)
                {
                    this.Model = model;
                    SetEditControlData();
                }
                else
                {
                    TempData[CommonData.MessageArea.MsgContent] = CommonMethod.ProcessDbExcetion(returnCode);
                    return RedirectToAction("Index", this.CurrentController);
                }
            }
            else
            {
                TempData[CommonData.MessageArea.MsgContent] = CommonMethod.ProcessDbExcetion(CommonData.DbReturnCode.DataNotFound);
                return RedirectToAction("Index", this.CurrentController);
            }

            return View("Edit", this.Layout, this.Model);
        }

        protected virtual ActionResult BaseEdit(IModel model)
        {
            this.Model = model;
            if (ModelState.IsValid)
            {
                IvsMessage message = null;
                int returnCode = Bl.UpdateData(this.Model);
                if (returnCode == CommonData.DbReturnCode.Succeed)
                {
                    message = new IvsMessage(CommonConstantMessage.COM_MSG_UPDATE_SUCCESSFULLY);
                    TempData[CommonData.MessageArea.MsgContent] = message.MessageText;
                    return RedirectToAction("Index", this.CurrentController);
                }
                else
                {
                    ProcessDbExcetion(returnCode);
                    //ModelState.AddModelError(CommonData.MessageArea.MsgContent, CommonMethod.ProcessDbExcetion(returnCode));
                    //TempData[CommonData.MessageArea.MsgContent] = CommonMethod.ProcessDbExcetion(returnCode);
                }
            }

            SetEditControlData();

            return View("Edit", this.Layout, this.Model);
        }
        #endregion

        #region Copy

        protected virtual ActionResult BaseCopy(int id)
        {
            int returnCode = CommonData.DbReturnCode.Succeed;
            if (!CommonMethod.IsNullOrEmpty(id))
            {
                IModel model = null;
                returnCode = Bl.SearchData(id, out model);
                if (returnCode == 0)
                {
                    this.Model = model;
                    SetEditControlData();
                }
                else
                {
                    TempData[CommonData.MessageArea.MsgContent] = CommonMethod.ProcessDbExcetion(returnCode);
                    return RedirectToAction("Index", this.CurrentController);
                }
            }
            else
            {
                TempData[CommonData.MessageArea.MsgContent] = CommonMethod.ProcessDbExcetion(CommonData.DbReturnCode.DataNotFound);
                return RedirectToAction("Index", this.CurrentController);
            }

            return View("Edit", this.Layout, this.Model);
        }

        protected virtual ActionResult BaseCopy(IModel model)
        {
            this.Model = model;
            if (ModelState.IsValid)
            {
                IvsMessage message = null;
                int returnCode = Bl.InsertData(this.Model);
                if (returnCode == CommonData.DbReturnCode.Succeed)
                {
                    message = new IvsMessage(CommonConstantMessage.COM_MSG_UPDATE_SUCCESSFULLY);
                    TempData[CommonData.MessageArea.MsgContent] = message.MessageText;
                    return RedirectToAction("Index", this.CurrentController);
                }
                else
                {
                    ProcessDbExcetion(returnCode);
                    //ModelState.AddModelError(CommonData.MessageArea.MsgContent, CommonMethod.ProcessDbExcetion(returnCode));
                    //TempData[CommonData.MessageArea.MsgContent] = CommonMethod.ProcessDbExcetion(returnCode);
                }
            }

            SetEditControlData();

            return View("Edit", this.Layout, this.Model);
        }
        #endregion

        #region Delete

        protected virtual ActionResult BaseDelete(List<int> listId)
        {
            string message = null;
            int returnCode = CommonData.DbReturnCode.Succeed;
            if (listId != null && listId.Count > 0)
            {
                returnCode = Bl.DeleteData(listId);
                if (returnCode == 0)
                {
                    message = new IvsMessage(CommonConstantMessage.COM_MSG_DELETE_SUCCESSFULLY).MessageText;
                }
                else
                {
                    message = CommonMethod.ProcessDbExcetion(returnCode);
                }
            }
            else
            {
                message = CommonMethod.ProcessDbExcetion(CommonData.DbReturnCode.DataNotFound);
            }

            return Json(new { ReturnCode = returnCode, Message = message });
        }
        #endregion

        #region Detail

        protected virtual ActionResult BaseDetail(int id)
        {
            int returnCode = CommonData.DbReturnCode.Succeed;
            if (Request.UrlReferrer != null)
            {
                if (!CommonMethod.IsNullOrEmpty(id))
                {
                    IModel model = null;
                    returnCode = Bl.SearchData(id, out model);
                    if (returnCode == 0)
                    {
                        this.Model = model;
                        SetEditControlData();
                    }
                    else
                    {
                        TempData[CommonData.MessageArea.MsgContent] = CommonMethod.ProcessDbExcetion(returnCode);
                        return RedirectToAction("Index", this.CurrentController);
                    }
                }
                else
                {
                    TempData[CommonData.MessageArea.MsgContent] = CommonMethod.ProcessDbExcetion(CommonData.DbReturnCode.DataNotFound);
                    return RedirectToAction("Index", this.CurrentController);
                }
            }

            //Mode:
            //1: Copy
            //2: Add
            //3: Edit
            //4: Detail
            ViewBag.Mode = CommonData.Mode.View;
            return View("Edit", this.Layout, this.Model);
        }

        #endregion

        #region Print

        protected virtual ActionResult BasePrint()
        {
            var lstModels = (List<IModel>)Session[SEARCH_RESULT];

            return PartialView(this.PartialViewName, ConvertToPageList(lstModels, lstModels.Count, 1));
        }

        #endregion

        #region Export

        public virtual ActionResult BaseExportXls()
        {
            return ExportFile(CommonData.FileType.Xls);
        }

        private ActionResult ExportFile(string fileType)
        {
            List<IModel> lstModels = (List<IModel>)Session[SEARCH_RESULT];

            string sourceName = "/ExcelPath/Template/Template.xls";
            string targetName = "/ExcelPath/Data/" + this.CurrentController + "." + fileType;
            string _dir = Server.MapPath("..\\");
            int rowStart = 2;
            int columnStart = 1;
            CExcelBase excelBase = null;
            try
            {
                excelBase = new CExcelBase(_dir + sourceName, _dir + targetName);

                //get data from model list
                if (lstModels.Count > 0)
                {
                    //get total column (property)
                    var properties = lstModels.FirstOrDefault().GetType().GetProperties();
                    object[,] data = new object[1 + lstModels.Count, properties.Count()];

                    //Set column's headers
                    int iCol = 0;
                    foreach (var property in properties)
                    {
                        var displayName = property.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                                                .FirstOrDefault() as DisplayNameAttribute;
                        data[0, iCol++] = displayName == null ? property.Name : displayName.DisplayName;
                    }

                    //Set column's values
                    for (int i = 0; i < lstModels.Count; i++)
                    {
                        IModel item = lstModels[i];
                        SetPropertyValue(item, i + 1, data);
                    }

                    //export data to file excel
                    excelBase.IsAutoFixColumn = true;
                    excelBase.IsAutoFixRow = true;
                    excelBase.ExportRange(rowStart, columnStart, (rowStart + data.GetLength(0) - 1), (columnStart + data.GetLength(1)), data);
                }

                int returnCode = excelBase.SaveFile();

                if (returnCode == 0)
                {
                    return Json(new { Url = targetName }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                if (excelBase != null)
                {
                    excelBase.ReleaseExcel();
                }
            }

            return Json(new { Url = string.Empty });
        }

        private void SetPropertyValue(IModel item, int rowIndex, object[,] data)
        {
            Type type = item.GetType();
            System.Reflection.PropertyInfo[] properties = type.GetProperties();
            int iCol = 0;
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                Type propType = property.PropertyType;
                if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    propType = new NullableConverter(propType).UnderlyingType;
                    data[rowIndex, iCol++] = (property.GetValue(item, null) == null) ? null
                                                      : Convert.ChangeType(property.GetValue(item, null), propType);
                    continue;
                }

                data[rowIndex, iCol++] = Convert.ChangeType(property.GetValue(item, null), property.PropertyType);
            }
        }

        public virtual ActionResult BaseExportXlsx()
        {
            return ExportFile(CommonData.FileType.Xlsx);
        }

        #endregion

        #region Private methods

        private T GetSearchModel<T>()
        {
            var model = default(T);
            if (Request.UrlReferrer != null)
            {
                //Kiem tra cung 1 controller thi research, nguoc lai clear
                string controller = this.CurrentController;
                if (Request.UrlReferrer.Segments.Any(ss => ss.Contains(controller)))
                {
                    Hashtable htbCondition = (Hashtable)Session[SEARCH_CONDITION];
                    //Neu da co dieu kien search thi search lai du lieu
                    if (!CommonMethod.IsNullOrEmpty(htbCondition) && !CommonMethod.IsNullOrEmpty(htbCondition[FunctionGrp]))
                    {
                        model = (T)htbCondition[FunctionGrp];
                    }
                    else
                    {
                        Session.Remove(SEARCH_CONDITION);
                        Session.Remove(SEARCH_RESULT);
                    }
                }
                else
                {
                    Session.Remove(SEARCH_CONDITION);
                    Session.Remove(SEARCH_RESULT);
                }
            }
            else
            {
                Session.Remove(SEARCH_CONDITION);
                Session.Remove(SEARCH_RESULT);
            }
            return model;
        }

        private void SetSearchModel<T>(int? page, ref T model)
        {
            if (!CommonMethod.IsNullOrEmpty(Session[SEARCH_CONDITION]))
            {
                Hashtable htbCondition = (Hashtable)Session[SEARCH_CONDITION];
                //khi phan trang se su dung lai dieu kien search
                //khi page.HasValue la su dung phan trang
                if (!CommonMethod.IsNullOrEmpty(htbCondition[FunctionGrp]) && page.HasValue)
                {
                    model = (T)htbCondition[FunctionGrp];
                }
                //khi search, page = null
                //luu lai dieu kien search moi
                else
                {
                    htbCondition[FunctionGrp] = model;
                    Session[SEARCH_CONDITION] = htbCondition;
                }
            }
            //luu lai dieu kien search moi
            else
            {
                Hashtable htbCondition = new Hashtable();
                htbCondition[FunctionGrp] = model;
                Session[SEARCH_CONDITION] = htbCondition;
            }
        }

        private object ConvertToModelList(List<IModel> data)
        {
            if (data == null || data.Count == 0 || this.Model == null)
            {
                return null;
            }
            Type ex = typeof(BaseController);
            MethodInfo mi = ex.GetMethod("ToModelList");
            MethodInfo miConstructed = mi.MakeGenericMethod(this.Model.GetType());
            // Invoke the method.
            object[] args = { data };
            var list = miConstructed.Invoke(null, args);
            return list;
        }

        private object ConvertToPageList(List<IModel> data, int pageSize, int pageNumber)
        {
            if (data == null || data.Count == 0 || this.Model == null)
            {
                return null;
            }
            Type ex = typeof(BaseController);
            MethodInfo mi = ex.GetMethod("ToPageList");
            MethodInfo miConstructed = mi.MakeGenericMethod(this.Model.GetType());
            // Invoke the method.
            object[] args = { data, pageNumber, pageSize, data.Count };
            var list = miConstructed.Invoke(null, args);
            return list;
        }

        private object ConvertToDto(IModel model)
        {
            if (model == null)
            {
                return null;
            }
            Type ex = typeof(BaseController);
            MethodInfo mi = ex.GetMethod("ToDto");
            MethodInfo miConstructed = mi.MakeGenericMethod(this.Model.GetType());
            // Invoke the method.
            object[] args = { model };
            var dto = miConstructed.Invoke(null, args);
            return dto;
        }

        public static IPagedList<T> ToPageList<T>(List<IModel> data, int pageNumber, int pageSize, int dataCount) where T : class
        {
            var result = data.Cast<T>().ToPagedList(pageNumber, pageSize, dataCount);

            return result;
        }

        public static List<T> ToModelList<T>(List<IModel> data) where T : class
        {
            var result = data.Cast<T>().ToList();

            return result;
        }

        public static T ToDto<T>(IModel model) where T : class
        {
            return model.Cast<T>();
        }

        #endregion

        #region Protected methods

        protected virtual void SetSearchControlData()
        {

        }

        protected virtual void SetEditControlData()
        {

        }

        protected virtual string ProcessDbExcetion(int returnCode)
        {
            IvsMessage message = null;
            switch (returnCode)
            {
                case CommonData.DbReturnCode.AccessDenied:
                    message = new IvsMessage(CommonConstantMessage.COM_MSG_ACCESS_DENIED);
                    break;

                case CommonData.DbReturnCode.InvalidHost:
                    message = new IvsMessage(CommonConstantMessage.COM_MSG_INVALID_HOST);
                    break;

                case CommonData.DbReturnCode.InvalidDatabase:
                    message = new IvsMessage(CommonConstantMessage.COM_MSG_INVALID_DATABASE);
                    break;

                case CommonData.DbReturnCode.LostConnection:
                    message = new IvsMessage(CommonConstantMessage.COM_MSG_CONNECTION_LOST);
                    break;

                case CommonData.DbReturnCode.DuplicateKey:
                    message = new IvsMessage(CommonConstantMessage.COM_MSG_DUPPLICATE_KEY);
                    break;

                case CommonData.DbReturnCode.ForgeignKeyNotExist:
                    message = new IvsMessage(CommonConstantMessage.COM_MSG_FOREIGN_KEY_NOT_EXIST);
                    break;

                case CommonData.DbReturnCode.ForeignKeyViolation:
                    message = new IvsMessage(CommonConstantMessage.COM_MSG_FOREIGN_KEY_VIOLATION);
                    break;

                case CommonData.DbReturnCode.DataNotFound:
                    message = new IvsMessage(CommonConstantMessage.COM_MSG_DATA_NOT_FOUND);
                    break;

                case CommonData.DbReturnCode.DeadlockFound:
                    message = new IvsMessage(CommonConstantMessage.COM_MSG_DEADLOCK_FOUND);
                    break;

                case CommonData.DbReturnCode.LockWaitTimeoutExceeded:
                    message = new IvsMessage(CommonConstantMessage.COM_MSG_LOCK_WAIT_TIMEOUT_EXCEEDED);
                    break;

                case CommonData.DbReturnCode.ExceptionOccured:
                    message = new IvsMessage(CommonConstantMessage.COM_MSG_GENERAL_EXCEPTION);
                    break;

                case CommonData.DbReturnCode.ConcurrencyViolation:
                    message = new IvsMessage(CommonConstantMessage.COM_MSG_CONCURRENCY_VIOLATION);
                    break;
            }

            ModelState.AddModelError(CommonData.MessageArea.MsgContent, message.MessageText);
            return message.MessageText;
        }

        protected virtual void SetAuthorityControl()
        {
            CommonBl commonBl = new CommonBl();

            if (!CommonMethod.IsNullOrEmpty(this.FunctionGrp))
            {
                CommonData.IsAuthority viewAuthority = commonBl.IsAuthority(this.FunctionGrp, CommonData.OperId.View);
                CommonData.IsAuthority newAuthority = commonBl.IsAuthority(this.FunctionGrp, CommonData.OperId.Add);
                CommonData.IsAuthority editAuthority = commonBl.IsAuthority(this.FunctionGrp, CommonData.OperId.Edit);
                CommonData.IsAuthority deleteAuthority = commonBl.IsAuthority(this.FunctionGrp, CommonData.OperId.Delete);
                CommonData.IsAuthority exportAuthority = commonBl.IsAuthority(this.FunctionGrp, CommonData.OperId.Export);
                CommonData.IsAuthority printAuthority = commonBl.IsAuthority(this.FunctionGrp, CommonData.OperId.Print);
                CommonData.IsAuthority importAuthority = commonBl.IsAuthority(this.FunctionGrp, CommonData.OperId.Import);

                this.AuthorityDictionary = new Dictionary<CommonData.ButtonCategory, CommonData.IsAuthority>();
                this.AuthorityDictionary.Add(CommonData.ButtonCategory.Search, viewAuthority);
                this.AuthorityDictionary.Add(CommonData.ButtonCategory.Add, newAuthority);
                this.AuthorityDictionary.Add(CommonData.ButtonCategory.Copy, newAuthority);
                this.AuthorityDictionary.Add(CommonData.ButtonCategory.Edit, editAuthority);
                this.AuthorityDictionary.Add(CommonData.ButtonCategory.Delete, deleteAuthority);
                this.AuthorityDictionary.Add(CommonData.ButtonCategory.Export, exportAuthority);
                this.AuthorityDictionary.Add(CommonData.ButtonCategory.Print, printAuthority);
                this.AuthorityDictionary.Add(CommonData.ButtonCategory.Import, importAuthority);
            }
        }

        protected virtual void SetMode()
        {
            switch (this.CurrentAction)
            {
                case "Index":
                    this.Mode = CommonData.Mode.Search;
                    break;
                case "Add":
                    this.Mode = CommonData.Mode.New;
                    break;
                case "Edit":
                    this.Mode = CommonData.Mode.Edit;
                    break;
                case "Copy":
                    this.Mode = CommonData.Mode.Copy;
                    break;
                case "Detail":
                    this.Mode = CommonData.Mode.View;
                    break;
                default:
                    break;
            }

            ViewBag.Mode = this.Mode;
        }

        protected virtual string GetSiteMap()
        {
            string siteMapKey = this.FunctionGrp + "_" + this.Mode.ToString();
            return I18n.GetMessage(siteMapKey);
        }
        #endregion

        
    }
}
