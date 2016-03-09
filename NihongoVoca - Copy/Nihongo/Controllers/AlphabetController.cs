using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nihongo.Controllers
{
    public class AlphabetController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Hiragana()
        {
            return View();
        }

        public ActionResult Katakana()
        {
            return View();
        }

        [ActionName("luyen-tap-hiragana")]
        public ActionResult HiraganaPractice()
        {
            return View("HiraganaPractice");
        }

        [ActionName("luyen-tap-katakana")]
        public ActionResult KatakanaPractice()
        {
            return View("KatakanaPractice");
        }
    }
}
