using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltSourceBankLibrary;

namespace AltSourceBankMVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.Session["userName"] != null)
            {
               return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(Login login)
        {
            if (login.UserName == null || login.Password == null)
            {
                ViewData["error"] = ("You must supply a user name and password to login. ");
                return View(login);
            }
            if (login.UserName.Length == 0 || login.Password.Length == 0)
            {
                ViewData["error"] = ("You must supply a user name and password to login. ");
                return View(login);
            }
            if (!login.VerifyUserName(login.UserName))
            {
                ViewData["error"] = (login.UserName + " not found. Please click register or try again with a different user. ");
                return View(login);
            }
            if (!login.VerifyPassword(login.UserName, login.Password))
            {
                ViewData["error"] = ("Password does not match for " + login.UserName + ". Please try again or create an account.");
                return View(login);
            }
            else
            {
                System.Web.HttpContext.Current.Session["userName"] = login.UserName;
                return RedirectToAction("Index", "Home");
            }
                
        }
        public ActionResult Register()
        {
            if (System.Web.HttpContext.Current.Session["userName"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Register(Login login)
        {
            if (login.UserName == null || login.Password == null)
            {
                ViewData["error"] = ("You must supply a user name and password to create an account. ");
                return View(login);
            }
            if (login.UserName.Length == 0 || login.Password.Length == 0)
            {
                ViewData["error"] = ("You must supply a user name and password to create an account. ");
                return View(login);
            }
            if(!login.VerifyEmail(login.UserName))
            {
                ViewData["error"] = (login.UserName + " is not a valid email. Please enter a valid email and try again. ");
                return View(login);
            }
            if (!login.VerifyNotDuplicateUser(login.UserName))
            {
                ViewData["error"] = (login.UserName + " already has an account. Please select a different email or login to your account. ");
                return View(login);
            }
            else
            {
                login.CreateUser(login);
                System.Web.HttpContext.Current.Session["userName"] = login.UserName;
                return RedirectToAction("Index", "Home");
            }

        }
        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["userName"] = null;
            return RedirectToAction("Index", "Account");
        }
    }
}