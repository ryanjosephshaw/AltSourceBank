using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltSourceBankMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.Session["userName"] == null)
            {
                return RedirectToAction("Index", "Account");
            } 
            return View();
        }
    }
}