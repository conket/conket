using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Nihongo.Filters;
using Nihongo.Models;
using Nihongo.Dal.Dao;
using Ivs.Core.Common;
using Ivs.Core.Data;
using Ivs.Core.Web.Attributes;

namespace Nihongo.Controllers
{
    public class AccountController : BaseController
    {
        #region Default

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(LoginModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
        //    {
        //        return RedirectToLocal(returnUrl);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    ModelState.AddModelError("", "The user name or password provided is incorrect.");
        //    return View(model);
        //}

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult RequireLogin()
        {
            return View();
        }

        //
        // POST: /Account/Register

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Attempt to register the user
        //        try
        //        {
        //            WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
        //            WebSecurity.Login(model.UserName, model.Password);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        catch (MembershipCreateUserException e)
        //        {
        //            ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // POST: /Account/Disassociate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // User is new, ask for their desired membership name
                string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
                ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
                ViewBag.ReturnUrl = returnUrl;
                return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider = null;
            string providerUserId = null;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Insert a new user into the database
                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
                    // Check if user already exists
                    if (user == null)
                    {
                        // Insert name into the profile table
                        db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
                        db.SaveChanges();

                        OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
                        OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
                    }
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (OAuthAccount account in accounts)
            {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

        #endregion

        #region Custom

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Add(string UserName, string DisplayName, string Password, string RePassword, bool CreateVoca)
        {
            int returnCode = 0;
            string mess = string.Empty;
            if (CommonMethod.IsNullOrEmpty(UserName))
            {
                return Json(new { ReturnCode = 9, Message = "Nhập [Tên đăng nhập]" });
            }
            if (CommonMethod.IsNullOrEmpty(Password))
            {
                return Json(new { ReturnCode = 9, Message = "Nhập [Mật khẩu]" });
            }
            if ((Password.Length < 6))
            {
                return Json(new { ReturnCode = 9, Message = "[Mật khẩu] phải có độ dài ít nhất 6 kí tự" });
            }
            if (CommonMethod.IsNullOrEmpty(RePassword))
            {
                return Json(new { ReturnCode = 9, Message = "Nhập [Nhập lại mật khẩu]" });
            }
            if ((RePassword != Password))
            {
                return Json(new { ReturnCode = 9, Message = "[Nhập lại mật khẩu] phải giống [Mật khẩu]" });
            }
            //if (ModelState.IsValid)
            //{
                // Attempt to register the user
                try
                {
                    MS_UsersDao dao = new MS_UsersDao();
                    MS_UsersModels model = new MS_UsersModels();
                    model.DisplayName = CommonMethod.IsNullOrEmpty(DisplayName) ? CommonMethod.ParseString(UserName) : DisplayName;
                    model.UserName = CommonMethod.Md5(UserName.ToLower());
                    model.Password = CommonMethod.Md5(CommonMethod.Md5(CommonMethod.Md5(Password)));
                    model.CreateVoca = CreateVoca;
                    returnCode = dao.InsertData(model);
                    if (returnCode == 0)
                    {
                        mess = "Thêm thành công";
                    }
                    else
                    {
                        mess = "Lỗi: " + returnCode;
                    }
                    //return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    mess = (e.InnerException.ToString());
                }
            //}
            //else
            //{
            //    mess = "Lỗi";
            //}

            return Json(new { ReturnCode = returnCode, Message = mess });
        }

        [HttpPost]
        [EncryptActionName(Name = ("ChangePassword"))]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string NewPassword, string ReNewPassword)
        {
            int returnCode = 0;
            string mess = string.Empty;
            if (CommonMethod.IsNullOrEmpty(NewPassword))
            {
                return Json(new { ReturnCode = 9, Message = "Nhập [Mật khẩu mới]" });
            }
            if ((NewPassword.Length < 6))
            {
                return Json(new { ReturnCode = 9, Message = "[Mật khẩu mới] phải có độ dài ít nhất 6 kí tự" });
            }
            if (CommonMethod.IsNullOrEmpty(ReNewPassword))
            {
                return Json(new { ReturnCode = 9, Message = "Nhập [Nhập lại mật khẩu mới]" });
            }
            if ((ReNewPassword != NewPassword))
            {
                return Json(new { ReturnCode = 9, Message = "[Nhập lại mật khẩu mới] phải giống [Mật khẩu mới]" });
            }
            //if (ModelState.IsValid)
            //{
            // Attempt to register the user
            try
            {
                if (!CommonMethod.IsNullOrEmpty(Session["UserName"]))
                {
                    MS_UsersDao dao = new MS_UsersDao();
                    MS_UsersModels model = new MS_UsersModels();
                    model.UserName = CommonMethod.ParseString(Session["UserName"]);
                    model.Password = CommonMethod.Md5(CommonMethod.Md5(CommonMethod.Md5(NewPassword)));
                    returnCode = dao.ChangePassword(model);
                    if (returnCode == 0)
                    {
                        mess = "Đổi mật khẩu thành công";
                    }
                    else
                    {
                        mess = "Lỗi: " + returnCode;
                    }
                }
                
                //return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                mess = (e.InnerException.ToString());
            }
            //}
            //else
            //{
            //    mess = "Lỗi";
            //}

            return Json(new { ReturnCode = returnCode, Message = mess });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ProcessBatch()
        {
            int returnCode = 0;
            string mess = string.Empty;
            try
            {
                MS_UsersDao dao = new MS_UsersDao();
                returnCode = dao.ProcessBatch();
                if (returnCode == 0)
                {
                    mess = "Thêm thành công";
                }
                else
                {
                    mess = "Lỗi: " + returnCode;
                }
            }
            catch (Exception e)
            {
                mess = (e.InnerException.ToString());
            }
            return Json(new { ReturnCode = returnCode, Message = mess });
        }


        [HttpPost]
        [EncryptActionName(Name = ("Login"))]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string UserName, string Password, string ReturnUrl)
        {
            int returnCode = 0;
            MS_UsersModels user = null;

            if (!CommonMethod.IsNullOrEmpty(UserName) && !CommonMethod.IsNullOrEmpty(Password))
            {
                MS_UsersDao dao = new MS_UsersDao();
                string notEncryptPass = Password;
                string notEncryptUsr = UserName.ToLower();
                Password = CommonMethod.Md5(CommonMethod.Md5(CommonMethod.Md5(Password)));
                UserName = CommonMethod.Md5(UserName.ToLower());
                returnCode = dao.SelectDataByUserName(UserName, Password, out user);

                if (returnCode == 0)
                {
                    if (user != null)
                    {
                        //if (RememberMe)
                        //{
                        //    HttpCookie cookie = new HttpCookie("Nihongo");
                        //    cookie.Values.Add("UserName", user.UserName);
                        //    cookie.Values.Add("DisplayName", user.DisplayName);
                        //    cookie.Values.Add("Password", Password);
                        //    cookie.Values.Add("IsAdmin", user.IsAdmin);
                        //    cookie.Expires = DateTime.Now.AddDays(15);
                        //    Response.Cookies.Add(cookie);
                        //}

                        #region Select notification

                        List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
                        MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
                        MS_UserVocabulariesModels model = new MS_UserVocabulariesModels();
                        model.UserName = user.UserName;
                        //returnCode = vocaDao.SelectWeakVocaSummary(model, out results);
                        //Session["Inbox"] = results.Count;
                        Session["UserName"] = user.UserName;
                        Session["DisplayName"] = user.DisplayName;
                        Session["IsAdmin"] = user.IsAdmin;
                        UserSession.UserName = user.UserName;

                        #endregion

                        #region Update state

                        user.LoginState = CommonData.Status.Enable;
                        user.LastVisitedDate = DateTime.Now;
                        returnCode = dao.UpdateState(user);

                        #endregion
                    }
                }
            }

            return Json(new { ReturnCode = returnCode, User = user, ReturnUrl = ReturnUrl });//Request.Url.AbsoluteUri });
        }

        [HttpPost]
        [EncryptActionName(Name = ("FLogin"))]
        [AllowAnonymous]
        public ActionResult FLogin(string id, string email, string name, string ReturnUrl)
        {
            int returnCode = 0;
            MS_UsersModels user = null;

            string mess = string.Empty;
            if (CommonMethod.IsNullOrEmpty(id))
            {
                return Json(new { ReturnCode = 9 });
            }
            if (CommonMethod.IsNullOrEmpty(email))
            {
                return Json(new { ReturnCode = 9 });
            }

            //if (ModelState.IsValid)
            //{
            // Attempt to register the user
            try
            {
                MS_UsersDao dao = new MS_UsersDao();
                MS_UsersModels model = new MS_UsersModels();
                model.DisplayName = name;
                model.UserName = CommonMethod.Md5(id.ToLower());
                model.Email = email;
                model.Password = CommonMethod.Md5(CommonMethod.Md5(CommonMethod.Md5(id)));
                model.CreateVoca = true;

                returnCode = dao.FLogin(model, out user);
                if (returnCode == 0)
                {
                    #region Select notification

                    List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
                    MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
                    MS_UserVocabulariesModels dmodel = new MS_UserVocabulariesModels();
                    dmodel.UserName = user.UserName;
                    //returnCode = vocaDao.SelectWeakVocaSummary(model, out results);
                    //Session["Inbox"] = results.Count;
                    Session["UserName"] = user.UserName;
                    Session["DisplayName"] = user.DisplayName;
                    Session["IsAdmin"] = user.IsAdmin;
                    UserSession.UserName = user.UserName;

                    #endregion

                    #region Update state

                    user.LoginState = CommonData.Status.Enable;
                    user.LastVisitedDate = DateTime.Now;
                    returnCode = dao.UpdateState(user);

                    #endregion
                }
                else
                {
                    mess = "Lỗi: " + returnCode;
                }
                //return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                mess = (e.InnerException.ToString());
            }

            return Json(new { ReturnCode = returnCode, User = user, ReturnUrl = ReturnUrl });//Request.Url.AbsoluteUri });
        }

        [HttpPost]
        [EncryptActionName(Name = ("GLogin"))]
        [AllowAnonymous]
        public ActionResult GLogin(string id, string email, string name, string ReturnUrl)
        {
            int returnCode = 0;
            MS_UsersModels user = null;

            string mess = string.Empty;
            if (CommonMethod.IsNullOrEmpty(id))
            {
                return Json(new { ReturnCode = 9 });
            }
            if (CommonMethod.IsNullOrEmpty(email))
            {
                return Json(new { ReturnCode = 9 });
            }

            //if (ModelState.IsValid)
            //{
            // Attempt to register the user
            try
            {
                MS_UsersDao dao = new MS_UsersDao();
                MS_UsersModels model = new MS_UsersModels();
                model.DisplayName = name;
                model.UserName = CommonMethod.Md5(id.ToLower());
                model.Email = email;
                model.Password = CommonMethod.Md5(CommonMethod.Md5(CommonMethod.Md5(id)));
                model.CreateVoca = true;

                returnCode = dao.FLogin(model, out user);
                if (returnCode == 0)
                {
                    #region Select notification

                    List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
                    MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
                    MS_UserVocabulariesModels dmodel = new MS_UserVocabulariesModels();
                    dmodel.UserName = user.UserName;
                    //returnCode = vocaDao.SelectWeakVocaSummary(model, out results);
                    //Session["Inbox"] = results.Count;
                    Session["UserName"] = user.UserName;
                    Session["DisplayName"] = user.DisplayName;
                    Session["IsAdmin"] = user.IsAdmin;
                    UserSession.UserName = user.UserName;

                    #endregion

                    #region Update state

                    user.LoginState = CommonData.Status.Enable;
                    user.LastVisitedDate = DateTime.Now;
                    returnCode = dao.UpdateState(user);

                    #endregion
                }
                else
                {
                    mess = "Lỗi: " + returnCode;
                }
                //return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                mess = (e.InnerException.ToString());
            }

            return Json(new { ReturnCode = returnCode, User = user, ReturnUrl = ReturnUrl });//Request.Url.AbsoluteUri });
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            Session["Inbox"] = 0;
            Session["UserName"] = CommonData.StringEmpty;
            Session["DisplayName"] = CommonData.StringEmpty;
            Session["IsAdmin"] = false;
            UserSession.UserName = CommonData.StringEmpty;
            
            if (Request.Cookies["Nihongo"] != null)
            {
                int returnCode = 0;
                MS_UsersDao dao = new MS_UsersDao();
                MS_UsersModels user = new MS_UsersModels();
                user.UserName = CommonMethod.ParseString(Request.Cookies["Nihongo"].Values["UserName"]);
                user.Password = CommonMethod.ParseString(Request.Cookies["Nihongo"].Values["Password"]);
                user.LoginState = CommonData.Status.Disable;
                user.LastVisitedDate = DateTime.Now;
                returnCode = dao.UpdateState(user);

                HttpCookie myCookie = new HttpCookie("Nihongo");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Board(string id)
        {
            if (CommonMethod.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.UserName = id;

            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
            MS_UserVocabulariesModels model = new MS_UserVocabulariesModels();
            model.UserName = id;
            int returnCode = vocaDao.SelectWeakVocaSummary(model, out results);

            return View(results);
        }

        [OutputCache(CacheProfile = "Cache1HourVaryByNone")]
        public ActionResult AccessDenied()
        {
            return View();
        }

        [OutputCache(CacheProfile = "Cache1HourVaryByNone")]
        public ActionResult RequireBuy()
        {
            return View();
        }

        public ActionResult UserProfile(string id)
        {
            if (CommonMethod.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.UserName = id;

            return View();
        }

        private void ClearUserCookie()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Nihongo"))
            {
                this.ControllerContext.HttpContext.Request.Cookies.Clear();
                //HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["Nihongo"];
                //cookie.Expires = DateTime.Now.AddDays(-1);
            }
        }

        public ActionResult Admin(string id)
        {
            ViewBag.UserName = id;
            MS_UsersDao dao = new MS_UsersDao();
            MS_UsersModels model = new MS_UsersModels()
            {
                UserName = id,
                Status = CommonData.Status.Enable,
            };
            MS_UsersModels result = new MS_UsersModels();
            int returnCode = dao.SelectDataByCode(model, out result);
            if (result == null || result.IsAdmin != CommonData.Status.Enable)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            return View();
        }

        [OutputCache(CacheProfile = "Cache5MinutesVaryByNone")]
        [HttpPost]
        public ActionResult UserState()
        {
            MS_UsersDao dao = new MS_UsersDao();
            List<MS_UsersModels> results = new List<MS_UsersModels>();
            MS_UsersModels model = new MS_UsersModels()
            {
                Status = CommonData.Status.Enable,
            };
            int returnCode = dao.SelectData(model, out results);
            return PartialView("_UserStatePartial", results);
        }

        #endregion

        public ActionResult HomePage(string id)
        {
            if (CommonMethod.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.UserName = id;

            return View();
        }
    }
}
