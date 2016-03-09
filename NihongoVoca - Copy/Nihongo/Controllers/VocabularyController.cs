using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nihongo.Controllers
{
    public class VocabularyController : Controller
    {
        //
        // GET: /Vocabulary/

        public ActionResult Index()
        {
            return View();
        }

    }
}
