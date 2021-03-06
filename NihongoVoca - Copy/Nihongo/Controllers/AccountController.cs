﻿using System;
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
using System.Data.OleDb;

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

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(MS_UsersModels model)
        {
            if (ModelState.IsValid)
            {
                string mess = string.Empty;
                // Attempt to register the user
                try
                {
                    //WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    //WebSecurity.Login(model.UserName, model.Password);
                    MS_UsersDao dao = new MS_UsersDao();
                    //MS_UsersModels userModel = new MS_UsersModels();
                    if (CommonMethod.IsNullOrEmpty(model.DisplayName))
                    {
                        string[] emails = model.Email.Split('@');
                        if (emails.Length > 0)
                        {
                            if (!CommonMethod.IsNullOrEmpty(emails[0]))
                            {
                                model.DisplayName = emails[0];
                            }
                            else
                            {
                                model.DisplayName = model.Email;
                            }
                        }
                        else
                        {
                            model.DisplayName = model.Email;
                        }
                    }
                    model.UserName = CommonMethod.Md5(model.Email.ToLower());
                    //model.Email = model.Email;
                    model.Password = CommonMethod.Md5(CommonMethod.Md5(CommonMethod.Md5(model.Password)));
                    //model.UrlImage = urlImage;
                    //model.CreateVoca = true;
                    MS_UsersModels user = null;
                    int returnCode = dao.Register(model, out user);
                    if (returnCode == 0)
                    {
                        HttpCookie cookie = new HttpCookie("NihongoVoca");
                        cookie.Values.Add("UserID", user.ID.ToString());
                        cookie.Values.Add("UserName", user.UserName);
                        cookie.Values.Add("Email", user.Email);
                        cookie.Values.Add("UrlImage", user.UrlImage);
                        cookie.Values.Add("DisplayName", user.DisplayName);
                        cookie.Values.Add("UrlImage", user.UrlImage);
                        cookie.Values.Add("Password", user.Password);
                        cookie.Values.Add("IsAdmin", user.IsAdmin);
                        cookie.Expires = DateTime.Now.AddDays(15);
                        Response.Cookies.Add(cookie);

                        #region Update state

                        user.LoginState = CommonData.Status.Enable;
                        user.LastVisitedDate = DateTime.Now;
                        returnCode = dao.UpdateState(ref user);

                        #endregion

                        Session["UserID"] = user.ID;
                        Session["UserName"] = user.UserName;
                        Session["Email"] = user.Email;
                        Session["DisplayName"] = user.DisplayName;
                        Session["IsAdmin"] = user.IsAdmin;
                        Session["UrlImage"] = user.UrlImage;
                        Session["VocaPerLearn"] = user.VocaPerLearn;
                        Session["VocaPerReview"] = user.VocaPerReview;
                        Session["SoundEffect"] = user.SoundEffect;

                        UserSession.UserName = user.UserName;
                        UserSession.UserID = user.ID;

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        switch (returnCode)
                        {
                            case CommonData.DbReturnCode.DuplicateKey:
                                TempData["Message"] = "Địa chỉ email đã được đăng ký. Hãy đăng nhập lại.";
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

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

                    int userID = -1;
                    returnCode = dao.InsertData(model, out userID);
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
                if (!CommonMethod.IsNullOrEmpty(Session["UserID"]))
                {
                    MS_UsersDao dao = new MS_UsersDao();
                    MS_UsersModels model = new MS_UsersModels();
                    model.ID = CommonMethod.ParseInt(Session["UserID"]);
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
            string error = string.Empty;

            if (!CommonMethod.IsNullOrEmpty(UserName) && !CommonMethod.IsNullOrEmpty(Password))
            {
                MS_UsersDao dao = new MS_UsersDao();
                string notEncryptPass = Password;
                string notEncryptUsr = UserName.ToLower();
                Password = CommonMethod.Md5(CommonMethod.Md5(CommonMethod.Md5(Password)));
                UserName = CommonMethod.Md5(UserName.ToLower());

                returnCode = dao.SelectDataByUserName(UserName, Password, out user, out error);

                if (returnCode == 0)
                {
                    if (user != null)
                    {
                        //if (RememberMe)
                        //{
                        HttpCookie cookie = new HttpCookie("NihongoVoca");
                        cookie.Values.Add("UserID", user.ID.ToString());
                        cookie.Values.Add("UserName", user.UserName);
                        cookie.Values.Add("Email", user.Email);
                        cookie.Values.Add("UrlImage", user.UrlImage);
                        cookie.Values.Add("DisplayName", user.DisplayName);
                        cookie.Values.Add("Password", Password);
                        cookie.Values.Add("IsAdmin", user.IsAdmin);
                        cookie.Expires = DateTime.Now.AddDays(15);
                        Response.Cookies.Add(cookie);
                        //}

                        #region Select notification

                        //List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
                        //MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
                        //MS_UserVocabulariesModels model = new MS_UserVocabulariesModels();
                        //model.UserName = user.UserName;
                        //model.UserID = user.ID;
                        //returnCode = vocaDao.SelectWeakVocaSummary(model, out results);
                        //Session["Inbox"] = results.Count;
                        
                        #endregion

                        #region Update state

                        user.LoginState = CommonData.Status.Enable;
                        user.LastVisitedDate = DateTime.Now;
                        returnCode = dao.UpdateState(ref user);

                        #endregion

                        Session["UserID"] = user.ID;
                        Session["UserName"] = user.UserName;
                        Session["Email"] = user.Email;
                        Session["DisplayName"] = user.DisplayName;
                        Session["IsAdmin"] = user.IsAdmin;
                        Session["UrlImage"] = user.UrlImage;
                        Session["VocaPerLearn"] = user.VocaPerLearn;
                        Session["VocaPerReview"] = user.VocaPerReview;
                        Session["SoundEffect"] = user.SoundEffect;

                        UserSession.UserName = user.UserName;
                        UserSession.UserID = user.ID;
                    }
                }
            }

            return Json(new { ReturnCode = returnCode, Error = error, ID = user == null ? 0 : user.ID, ReturnUrl = ReturnUrl });//Request.Url.AbsoluteUri });
        }

        [HttpPost]
        [EncryptActionName(Name = ("FLogin"))]
        [AllowAnonymous]
        public ActionResult FLogin(string id, string email, string name, string urlImage, string ReturnUrl)
        {
            int returnCode = 0;
            MS_UsersModels user = null;

            string mess = string.Empty;
            if (CommonMethod.IsNullOrEmpty(id))
            {
                return Json(new { ReturnCode = 9, Message = "ID not found!" });
            }
            //if (CommonMethod.IsNullOrEmpty(email))
            //{
            //    return Json(new { ReturnCode = 9 });
            //}

            //if (ModelState.IsValid)
            //{
            // Attempt to register the user
            try
            {
                MS_UsersDao dao = new MS_UsersDao();
                MS_UsersModels model = new MS_UsersModels();
                model.DisplayName = name;
                model.UserName = CommonMethod.Md5(id.ToLower());
                model.UrlImage = urlImage;
                model.Email = CommonMethod.IsNullOrEmpty(email) ? CommonData.StringEmpty : (email.ToLower());
                model.Password = CommonMethod.Md5(CommonMethod.Md5(CommonMethod.Md5(id)));
                //model.CreateVoca = false;
                //mess = "Insert thành công";
                returnCode = dao.FLogin(model, out user);
                if (returnCode == 0)
                {
                    HttpCookie cookie = new HttpCookie("NihongoVoca");
                    cookie.Values.Add("UserID", user.ID.ToString());
                    cookie.Values.Add("UserName", user.UserName);
                    cookie.Values.Add("Email", user.Email);
                    cookie.Values.Add("UrlImage", user.UrlImage);
                    cookie.Values.Add("DisplayName", user.DisplayName);
                    cookie.Values.Add("Password", user.Password);
                    cookie.Values.Add("IsAdmin", user.IsAdmin);
                    cookie.Expires = DateTime.Now.AddDays(15);
                    Response.Cookies.Add(cookie);

                    #region Select notification

                    //List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
                    //MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
                    //MS_UserVocabulariesModels dmodel = new MS_UserVocabulariesModels();
                    //dmodel.UserName = user.UserName;
                    //returnCode = vocaDao.SelectWeakVocaSummary(model, out results);
                    //Session["Inbox"] = results.Count;
                    
                    #endregion

                    #region Update state

                    user.LoginState = CommonData.Status.Enable;
                    user.LastVisitedDate = DateTime.Now;
                    returnCode = dao.UpdateState(ref user);

                    #endregion

                    Session["UserID"] = user.ID;
                    Session["UserName"] = user.UserName;
                    Session["Email"] = user.Email;
                    Session["DisplayName"] = user.DisplayName;
                    Session["IsAdmin"] = user.IsAdmin;
                    Session["UrlImage"] = user.UrlImage;
                    Session["VocaPerLearn"] = user.VocaPerLearn;
                    Session["VocaPerReview"] = user.VocaPerReview;
                    Session["SoundEffect"] = user.SoundEffect;

                    UserSession.UserName = user.UserName;
                    UserSession.UserID = user.ID;
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

            return Json(new { ReturnCode = returnCode, Message = mess, User = user, ReturnUrl = ReturnUrl });//Request.Url.AbsoluteUri });
        }

        [HttpPost]
        [EncryptActionName(Name = ("GLogin"))]
        [AllowAnonymous]
        public ActionResult GLogin(string id, string email, string name, string urlImage, string ReturnUrl)
        {
            int returnCode = 0;
            MS_UsersModels user = null;

            string mess = string.Empty;
            if (CommonMethod.IsNullOrEmpty(id))
            {
                return Json(new { ReturnCode = 9, Message = "ID not found!" });
            }
            //if (CommonMethod.IsNullOrEmpty(email))
            //{
            //    return Json(new { ReturnCode = 9 });
            //}

            //if (ModelState.IsValid)
            //{
            // Attempt to register the user
            try
            {
                MS_UsersDao dao = new MS_UsersDao();
                MS_UsersModels model = new MS_UsersModels();
                model.DisplayName = name;
                model.UserName = CommonMethod.Md5(id.ToLower());
                model.Email = CommonMethod.IsNullOrEmpty(email) ? CommonData.StringEmpty : (email.ToLower());
                model.Password = CommonMethod.Md5(CommonMethod.Md5(CommonMethod.Md5(id)));
                model.UrlImage = urlImage;
                //model.CreateVoca = true;

                returnCode = dao.GLogin(model, out user);
                if (returnCode == 0)
                {
                    HttpCookie cookie = new HttpCookie("NihongoVoca");
                    cookie.Values.Add("UserID", user.ID.ToString());
                    cookie.Values.Add("UserName", user.UserName);
                    cookie.Values.Add("Email", user.Email);
                    cookie.Values.Add("UrlImage", user.UrlImage);
                    cookie.Values.Add("DisplayName", user.DisplayName);
                    cookie.Values.Add("UrlImage", user.UrlImage);
                    cookie.Values.Add("Password", user.Password);
                    cookie.Values.Add("IsAdmin", user.IsAdmin);
                    cookie.Expires = DateTime.Now.AddDays(15);
                    Response.Cookies.Add(cookie);

                    #region Select notification

                    //List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
                    //MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
                    //MS_UserVocabulariesModels dmodel = new MS_UserVocabulariesModels();
                    //dmodel.UserName = user.UserName;
                    //returnCode = vocaDao.SelectWeakVocaSummary(model, out results);
                    //Session["Inbox"] = results.Count;
                    

                    #endregion

                    #region Update state

                    user.LoginState = CommonData.Status.Enable;
                    user.LastVisitedDate = DateTime.Now;
                    returnCode = dao.UpdateState(ref user);

                    #endregion

                    Session["UserID"] = user.ID;
                    Session["UserName"] = user.UserName;
                    Session["Email"] = user.Email;
                    Session["DisplayName"] = user.DisplayName;
                    Session["IsAdmin"] = user.IsAdmin;
                    Session["UrlImage"] = user.UrlImage;
                    Session["VocaPerLearn"] = user.VocaPerLearn;
                    Session["VocaPerReview"] = user.VocaPerReview;
                    Session["SoundEffect"] = user.SoundEffect;

                    UserSession.UserName = user.UserName;
                    UserSession.UserID = user.ID;
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

            return Json(new { ReturnCode = returnCode, Message = mess, User = user, ReturnUrl = ReturnUrl });//Request.Url.AbsoluteUri });
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            Session["Inbox"] = 0;
            Session["UserID"] = CommonData.StringEmpty;
            Session["UserName"] = CommonData.StringEmpty;
            Session["DisplayName"] = CommonData.StringEmpty;
            Session["UrlImage"] = CommonData.StringEmpty;
            Session["Email"] = CommonData.StringEmpty;
            Session["VocaPerLearn"] = 5;
            Session["VocaPerReview"] = 10;
            Session["SoundEffect"] = CommonData.Status.Enable;

            Session["IsAdmin"] = false;
            UserSession.UserName = CommonData.StringEmpty;
            UserSession.UserID = -1;
            if (Request.Cookies["NihongoVoca"] != null)
            {
                int returnCode = 0;
                MS_UsersDao dao = new MS_UsersDao();
                MS_UsersModels user = new MS_UsersModels();
                user.ID = CommonMethod.ParseInt(Request.Cookies["NihongoVoca"].Values["UserID"]);
                user.UserName = CommonMethod.ParseString(Request.Cookies["NihongoVoca"].Values["UserName"]);
                user.Password = CommonMethod.ParseString(Request.Cookies["NihongoVoca"].Values["Password"]);
                user.LoginState = CommonData.Status.Disable;
                user.LastVisitedDate = DateTime.Now;
                returnCode = dao.UpdateState(ref user);

                HttpCookie myCookie = new HttpCookie("NihongoVoca");
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
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("NihongoVoca"))
            {
                this.ControllerContext.HttpContext.Request.Cookies.Clear();
                //HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["Nihongo"];
                //cookie.Expires = DateTime.Now.AddDays(-1);
            }
        }

        public ActionResult CreateVocaCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateVocaCategory(HttpPostedFileBase file)
        {
            int returnCode = 0;
            if (file != null && file.ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(file.FileName);
                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Upload"), file.FileName);
                if (System.IO.File.Exists(path1))
                    System.IO.File.Delete(path1);

                file.SaveAs(path1);
                //string sqlConnectionString = @"Data Source=LEEDHAR2-PC\SQLEXPRESS;Database=Leedhar_Import;Trusted_Connection=true;Persist Security Info=True";

                List<MS_VocabularyDetailModel> list = new List<MS_VocabularyDetailModel>();
                //Create connection string to Excel work book
                string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=Excel 12.0;Persist Security Info=False";
                //Create Connection to Excel work book
                using (OleDbConnection excelConnection = new OleDbConnection(excelConnectionString))
                {
                    excelConnection.Open();

                    System.Data.DataSet ds = new System.Data.DataSet();
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter("Select * from [Sheet1$]", excelConnection))
                    {
                        dataAdapter.Fill(ds);
                    }

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        MS_VocabularyDetailModel detail = new MS_VocabularyDetailModel();
                        detail.CategoryCode = CommonMethod.ParseString(ds.Tables[0].Rows[i]["CategoryCode"]);
                        detail.VocabularyCode = CommonMethod.ParseString(ds.Tables[0].Rows[i]["VocabularyCode"]);
                        detail.KanjiCode = CommonMethod.ParseString(ds.Tables[0].Rows[i]["KanjiCode"]);
                        detail.LineNumber = CommonMethod.ParseInt(ds.Tables[0].Rows[i]["LineNumber"].ToString());
                        list.Add(detail);
                    }
                    excelConnection.Close();
                }

                using (MS_VocaCategoryDao dao = new MS_VocaCategoryDao())
                {
                    returnCode = dao.CreateVocaCategory(list);
                    if (returnCode == 0)
                    {
                        TempData["ErrorMessage"] = "Tạo bộ từ vựng thành công!!!";
                        return RedirectToAction("CreateVocaCategory");
                    }
                }
            }

            TempData["ErrorMessage"] = "Tạo bộ từ vựng thất bại!!!";
            return View();
        }

        [HttpPost]
        public ActionResult CreateKanjiExample(HttpPostedFileBase file)
        {
            int returnCode = 0;
            if (file != null && file.ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(file.FileName);
                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Upload"), file.FileName);
                if (System.IO.File.Exists(path1))
                    System.IO.File.Delete(path1);

                file.SaveAs(path1);
                //string sqlConnectionString = @"Data Source=LEEDHAR2-PC\SQLEXPRESS;Database=Leedhar_Import;Trusted_Connection=true;Persist Security Info=True";

                List<MS_KanjiExampleModel> list = new List<MS_KanjiExampleModel>();
                //Create connection string to Excel work book
                string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=Excel 12.0;Persist Security Info=False";
                //Create Connection to Excel work book
                using (OleDbConnection excelConnection = new OleDbConnection(excelConnectionString))
                {
                    excelConnection.Open();

                    System.Data.DataSet ds = new System.Data.DataSet();
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter("Select * from [Sheet1$]", excelConnection))
                    {
                        dataAdapter.Fill(ds);
                    }

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        MS_KanjiExampleModel detail = new MS_KanjiExampleModel();
                        detail.KanjiCode = CommonMethod.ParseString(ds.Tables[0].Rows[i]["KanjiCode"]);
                        detail.VocabularyCode = CommonMethod.ParseString(ds.Tables[0].Rows[i]["VocabularyCode"]);
                        detail.Kanji = CommonMethod.ParseString(ds.Tables[0].Rows[i]["Kanji"]);
                        detail.Hiragana = CommonMethod.ParseString(ds.Tables[0].Rows[i]["Hiragana"]);
                        detail.Pinyin = CommonMethod.ParseString(ds.Tables[0].Rows[i]["Pinyin"]);
                        detail.VMeaning = CommonMethod.ParseString(ds.Tables[0].Rows[i]["VMeaning"]);
                        list.Add(detail);
                    }
                    excelConnection.Close();
                }

                using (MS_VocaCategoryDao dao = new MS_VocaCategoryDao())
                {
                    returnCode = dao.CreateKanjiExample(list);
                    if (returnCode == 0)
                    {
                        TempData["ErrorMessage"] = "Tạo bộ từ vựng thành công!!!";
                        return RedirectToAction("CreateVocaCategory");
                    }
                }
            }

            TempData["ErrorMessage"] = "Tạo bộ từ vựng thất bại!!!";
            return View();
        }

        public ActionResult Admin(int id)
        {
            ViewBag.UserName = id;
            MS_UsersDao dao = new MS_UsersDao();
            MS_UsersModels model = new MS_UsersModels()
            {
                ID = id,
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

        [ActionName("HomePage")]
        public ActionResult HomePage()
        {
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                return RedirectToAction("RequireLogin", "Account");
            }

            ViewBag.UserID = Session["UserID"];

            MS_UsersModels userModel = new MS_UsersModels();
            using (MS_UserVocabularyDao dao = new MS_UserVocabularyDao())
            {
                int returnCode = dao.SelectUserHomePageData(CommonMethod.ParseInt(Session["UserID"]), out userModel);
                if (userModel == null)
                {
                    return RedirectToAction("RequireLogin", "Account");
                }
            }

            return View("HomePage", userModel);
        }

        [ActionName("bo-tu-vung")]
        public ActionResult VocaSet()
        {
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                return RedirectToAction("RequireLogin", "Account");
            }

            ViewBag.UserID = Session["UserID"];

            MS_UsersModels userModel = new MS_UsersModels();
            using (MS_UserVocabularyDao dao = new MS_UserVocabularyDao())
            {
                int returnCode = dao.SelectUserVocaSetData(CommonMethod.ParseInt(Session["UserID"]), out userModel);
            }

            return View("VocaSet", userModel);
        }


        [ActionName("danh-sach-bai-hoc")]
        public ActionResult VocaCate(int id, string urlDisplay)
        {
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                return RedirectToAction("RequireLogin", "Account");
            }

            List<MS_UserCategoriesModels> results = new List<MS_UserCategoriesModels>();
            MS_VocaCategoryDao dao = new MS_VocaCategoryDao();
            int returnCode = dao.SelectUserCategoryBySet(id, CommonMethod.ParseInt(Session["UserID"]), out results);

            return View("VocaCate", results);
        }

        [ActionName("danh-muc-tu-vung")]
        public ActionResult Voca(int id, string urlDisplay)
        {
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                return RedirectToAction("RequireLogin", "Account");
            }
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
            int returnCode = dao.SelectUserVocaData(id, CommonMethod.ParseInt(Session["UserID"]), out results);

            if (results.Count > 0 && results.FirstOrDefault().IsKanji == CommonData.Status.Enable)
            {
                return View("VocaKanji", results);
            }
            else
            {
                return View("Voca", results);
            }
        }

        [HttpGet]
        public ActionResult Setting()
        {
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                return RedirectToAction("RequireLogin", "Account");
            }
            SettingModel model = new SettingModel();
            //model.VocaPerReview = CommonMethod.ParseInt(Session["VocaPerReview"]);
            //model.VocaPerLearn = CommonMethod.ParseInt(Session["VocaPerLearn"]);
            //model.SoundEffect = CommonMethod.ParseBool(Session["SoundEffect"]);
            int returnCode = 0;
            MS_UsersDao dao = new MS_UsersDao();
            MS_UsersModels user = new MS_UsersModels();
            user.ID = CommonMethod.ParseInt(Request.Cookies["NihongoVoca"].Values["UserID"]);
            returnCode = dao.SelectUserByID(ref user);
            if (user != null)
            {
                model.SoundEffect = user.SoundEffect == CommonData.Status.Enable ? true : false;
                model.VocaPerLearn = user.VocaPerLearn;
                model.VocaPerReview = user.VocaPerReview;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Setting(SettingModel setting)
        {
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                return RedirectToAction("RequireLogin", "Account");
            }
            if ((setting.VocaPerLearn) < 4)
            {
                ModelState.AddModelError("VocaPerLearn", "[Số từ mỗi lần học] ít nhất là 4");
            }
            else if (setting.VocaPerLearn.ToString().Length > 2)
            {
                ModelState.AddModelError("VocaPerLearn", "[Số từ mỗi lần học] không nhiều hơn 99 từ");
            }

            if ((setting.VocaPerReview) < 4)
            {
                ModelState.AddModelError("VocaPerReview", "[Số từ mỗi lần ôn] ít nhất là 4");
            }
            else if (setting.VocaPerReview.ToString().Length > 2)
            {
                ModelState.AddModelError("VocaPerReview", "[Số từ mỗi lần ôn] không nhiều hơn 99 từ");
            }
            if (ModelState.IsValid)
            {
                using (MS_UsersDao dao = new MS_UsersDao())
                {
                    int returnCode = dao.UpdateSetting(CommonMethod.ParseInt(Session["UserID"]), setting);
                    if (returnCode == CommonData.DbReturnCode.Succeed)
                    {
                        Session["VocaPerLearn"] = setting.VocaPerLearn.ToString();
                        Session["VocaPerReview"] = setting.VocaPerReview.ToString();
                        Session["SoundEffect"] = setting.SoundEffect ? CommonData.Status.Enable : CommonData.Status.Disable;

                        TempData["Message"] = "Lưu cấu hình thành công!";
                    }
                    else
                    {
                        TempData["Message"] = "Có lỗi xảy ra: " + returnCode;

                        //Session["VocaPerLearn"] = 5;
                        //Session["VocaPerReview"] = 10;
                        //Session["SoundEffect"] = true;

                    }
                }
            }

            return View(setting);
        }

        [EncryptActionName(Name = ("GetUsers"))]
        [OutputCache(CacheProfile = "Cache1MinuteVaryByIDClient")]
        public ActionResult GetUsers()
        {
            MS_UsersModels userModel = new MS_UsersModels();
            using (MS_UsersDao dao = new MS_UsersDao())
            {
                List<MS_UsersModels> users = new List<MS_UsersModels>();
                int returnCode = dao.SelectUsersData(CommonMethod.ParseInt(Session["UserID"]), out users);
                return PartialView("_UserPartialView", users);
            }
        }

        [EncryptActionName(Name = ("GetActivities"))]
        [OutputCache(CacheProfile = "Cache1MinuteVaryByIDClient")]
        public ActionResult GetActivities()
        {
            using (MS_UsersDao dao = new MS_UsersDao())
            {
                List<MS_TestResultModels> results = new List<MS_TestResultModels>();
                int returnCode = dao.SelectActivities(CommonMethod.ParseInt(Session["UserID"]), out results);
                return PartialView("_ActivitiesPartial", results);
            }
        }

        [EncryptActionName(Name = ("GetButtons"))]
        [OutputCache(CacheProfile = "Cache1MinuteVaryByIDClient")]
        public ActionResult GetButtons(int id)
        {
            using (MS_UserVocabularyDao dao = new MS_UserVocabularyDao())
            {
                MS_UserVocaSet result = new MS_UserVocaSet();
                int returnCode = dao.SelectUserVocaSetData(CommonMethod.ParseInt(Session["UserID"]), id, out result);
                return PartialView("_CategoryButtonPartial", result);
            }
        }

        [EncryptActionName(Name = ("Follow"))]
        [HttpPost]
        public ActionResult Follow(int followerID, bool followed)
        {
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                using (MS_UsersDao dao = new MS_UsersDao())
                {
                    returnCode = dao.Follow(CommonMethod.ParseInt(Session["UserID"]), followerID, !followed);
                }
            }

            return Json(new { ReturnCode = returnCode });
        }

        [EncryptActionName(Name = ("GetCategoryBySet"))]
        [OutputCache(CacheProfile = "Cache1MinuteVaryByIDClient")]
        public ActionResult GetCategoryBySet(int id)
        {
            List<MS_VocaCategoriesModels> results = new List<MS_VocaCategoriesModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                using (MS_VocaCategoryDao dao = new MS_VocaCategoryDao())
                {
                    returnCode = dao.SelectCategoryBySet(id, CommonMethod.ParseInt(Session["UserID"]), out results);
                }
            }

            return PartialView("_CategoryPartial", results);
        }

        [EncryptActionName(Name = ("GetFollowersByUser"))]
        [OutputCache(CacheProfile = "Cache1MinuteVaryByIDClient")]
        public ActionResult GetFollowersByUser(int id)
        {
            List<MS_UsersModels> results = new List<MS_UsersModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                using (MS_UsersDao dao = new MS_UsersDao())
                {
                    returnCode = dao.SelectFollowersByUser(id, out results);
                }
            }

            return PartialView("_FolllowersPartial", results);
        }

        [EncryptActionName(Name = ("GetFollowingsByUser"))]
        [OutputCache(CacheProfile = "Cache1MinuteVaryByIDClient")]
        public ActionResult GetFollowingsByUser(int id)
        {
            List<MS_UsersModels> results = new List<MS_UsersModels>();
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                using (MS_UsersDao dao = new MS_UsersDao())
                {
                    returnCode = dao.SelectFollowingsByUser(id, out results);
                }
            }

            return PartialView("_FolllowersPartial", results);
        }

        [EncryptActionName(Name = ("IgnoreCategory"))]
        [HttpPost]
        public ActionResult IgnoreCategory(MS_UserCategoriesModels voca)
        {
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                returnCode = dao.IgnoreUserCategory(CommonMethod.ParseInt(Session["UserID"]), ref voca);
            }

            return Json(new { ReturnCode = returnCode, HasLearnt = voca.HasLearnt });
        }

        [EncryptActionName(Name = ("IgnoreVoca"))]
        [HttpPost]
        public ActionResult IgnoreVoca(MS_UserVocabulariesModels voca)
        {
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                returnCode = dao.IgnoreUserVoca(CommonMethod.ParseInt(Session["UserID"]), ref voca);
            }

            return Json(new { ReturnCode = returnCode, HasLearnt = voca.HasLearnt });
        }

        [EncryptActionName(Name = ("MarkVoca"))]
        [HttpPost]
        public ActionResult MarkVoca(MS_UserVocabulariesModels voca)
        {
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserID"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_UserVocabularyDao dao = new MS_UserVocabularyDao();
                returnCode = dao.MarkVoca(CommonMethod.ParseInt(Session["UserID"]), voca);
            }

            return Json(new { ReturnCode = returnCode });
        }
    }
}
