using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecretChord.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FaqManager()
        {
            return View();
        }

        public ActionResult AppConfigManager()
        {
            return View();
        }

        public ActionResult AboutPageManager()
        {
            return View();
        }
    }
}