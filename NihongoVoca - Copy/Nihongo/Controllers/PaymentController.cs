using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nihongo.Models;
using Nihongo.Dal.Dao;
using Ivs.Core.Common;
using CaptchaMvc.HtmlHelpers;
using Ivs.Core.Attributes;

namespace Nihongo.Controllers
{
    
    public class PaymentController : Controller
    {
        //
        // GET: /Payment/
        [ActionName("bo")]
        [HiddenSetIDAttribute]
        public ActionResult Index(int id)
        {
            if (id == 0)
            {
                ViewBag.Message = "Không tìm thấy dữ liệu!";
                return View("Error");
            }
            MS_VocaSetsModels result = new MS_VocaSetsModels();
            MS_VocaSetsDao dao = new MS_VocaSetsDao();
            int returnCode = dao.SelectVocaSetByID((id), out result);
            ViewBag.VocaSetID = result.ID;
            ViewBag.VocaSetCode = result.Code;
            ViewBag.VocaSetName1 = result.Name1;
            ViewBag.Fee = CommonMethod.ParseDecimal(result.Fee);
            ViewBag.ErrorMessage = string.Empty;
            ViewBag.ID = id;
            ViewBag.UrlDisplay = result.UrlDisplay;

            //RouteData.Values.Remove("id");
            
            return View("Index", new MS_PaymentHistoriesModels() { PaymentMethod = "1", RemainFee = result.Fee });
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CheckValidVoucher(string id, string vocaSetCode)
        {
            int returnCode = 0;
            MS_VoucherModels model = new MS_VoucherModels();
            MS_VoucherModels result = new MS_VoucherModels();
            bool isOK = false;
            bool isUsed = false;
            if (CommonMethod.IsNullOrEmpty(Session["UserName"]))
            {
                returnCode = CommonData.DbReturnCode.AccessDenied;
            }
            else
            {
                MS_VoucherDao dao = new MS_VoucherDao();
                model.Code = id;
                model.VocaSetCode = vocaSetCode;
                model.UserName = CommonMethod.ParseString(Session["UserName"]);
                returnCode = dao.CheckValidVoucher(model, out isOK, out isUsed, out result);
            }

            return Json(new { voucher = result, returnCode = returnCode, isOK = isOK, isUsed = isUsed }
                , JsonRequestBehavior.AllowGet);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(int id, string urlDisplay, MS_PaymentHistoriesModels model)
        {
            int returnCode = 0;
            if (CommonMethod.IsNullOrEmpty(Session["UserName"]))
            {
                return RedirectToAction("Index", "Home", new { @urlDisplay = "tu-vung-tieng-nhat" });
            }

            if (!this.IsCaptchaValid("Mã hình ảnh không đúng"))
            {
                ModelState.AddModelError("CaptchaInputText", "Mã hình ảnh không đúng!");
            }

            MS_VoucherModels vModel = new MS_VoucherModels();
            vModel.ID = CommonMethod.ParseInt(model.VoucherID);
            MS_VoucherModels vResult = new MS_VoucherModels();
            bool isCheckPayment = false;
            if (!CommonMethod.IsNullOrEmpty(model.VoucherCode))
            {
                //load voucher info
                MS_VoucherDao vDao = new MS_VoucherDao();
                returnCode = vDao.SelectData(vModel, out vResult);

                //nếu ko có voucher hoặc voucher ko giảm 100%
                if (vResult == null)
                {
                    isCheckPayment = true;
                }
                else
                {
                    model.DecreaseFee = vResult.DecreaseFee;
                    model.DecreasePercent = vResult.DecreasePercent;
                    model.RemainFee = vResult.RemainFee;
                    
                    if (vResult.DecreasePercent < 100 || vResult.RemainFee > 0)
                    {
                        isCheckPayment = true;
                    }
                }
            }
            else
            {
                isCheckPayment = true;
            }


            if (isCheckPayment)
            {
                switch (model.PaymentMethod)
                {
                    //telephone card
                    case "1":
                    case "2":
                    case "3":
                        if (CommonMethod.IsNullOrEmpty(model.CardCode))
                        {
                            ModelState.AddModelError("CardCode", "Mã số thẻ cào bắt buộc nhập!");
                        }
                        if (CommonMethod.IsNullOrEmpty(model.CardSeri))
                        {
                            ModelState.AddModelError("CardSeri", "Số Seri thẻ cào bắt buộc nhập!");
                        }
                        break;
                    case "4":

                        break;
                    case "5":

                        break;
                    default:
                        break;
                }
            }
            if (ModelState.Count > 0)
            {
                ViewBag.ErrorMessage = "Có lỗi xảy ra trong quá trình nhập liệu!";
            }

            if (ModelState.IsValid)
            {
                model.UserID = CommonMethod.ParseInt(Session["UserID"]);
                //process
                MS_PaymentHistoriesDao paymentDao = new MS_PaymentHistoriesDao();
                returnCode = paymentDao.Process(model);

                //return Library
                return RedirectToAction("Success", "Payment", new { @id = model.VocaSetID, @urlDisplay = "thu-vien-tu-vung-tieng-nhat" });
            }

            
            //Error
            MS_VocaSetsModels result = new MS_VocaSetsModels();
            MS_VocaSetsDao dao = new MS_VocaSetsDao();
            returnCode = dao.SelectVocaSetByID(model.VocaSetID, out result);
            ViewBag.VocaSetID = result.ID;
            ViewBag.VocaSetName1 = result.Name1;
            ViewBag.Fee = CommonMethod.ParseDecimal(result.Fee);

            return View(model);
        }

        public ActionResult Success(int id, string urlDisplay)
        {
            ViewBag.VocaSetID = id;
            ViewBag.UrlDisplay = urlDisplay;
            if (CommonMethod.IsNullOrEmpty(Session["UserName"]))
            {
                return RedirectToAction("Index", "Home", new { @urlDisplay = "tu-vung-tieng-nhat" });
            }
            MS_RegistedVocaModels model = new MS_RegistedVocaModels()
            {
                VocaSetID = id,
                UserName = CommonMethod.ParseString(Session["UserName"]),
            };
            MS_RegistedVocaModels result = new MS_RegistedVocaModels();
            MS_RegistedVocaDao dao = new MS_RegistedVocaDao();
            int returnCode = dao.SelectDataByModel(model, out result);

            return View(result);
        }
    }
}
