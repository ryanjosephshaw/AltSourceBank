using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltSourceBankLibrary;

namespace AltSourceBankMVC.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult AvailableBalance()
        {
            if (System.Web.HttpContext.Current.Session["userName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            string userName = System.Web.HttpContext.Current.Session["userName"].ToString();
            Transactions tran = new Transactions();
            decimal balance = tran.GetBalance(userName);
            return View(balance);
        }
        public ActionResult TransactionHistory()
        {
            if (System.Web.HttpContext.Current.Session["userName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            string userName = System.Web.HttpContext.Current.Session["userName"].ToString();
            Transactions tran = new Transactions();
            List<Transactions> transactions = tran.GetTransactionHistory(userName);
            return View(transactions);
        }
        public ActionResult Deposit()
        {
            if (System.Web.HttpContext.Current.Session["userName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Deposit(Transactions trans)
        {
            trans.User = System.Web.HttpContext.Current.Session["userName"].ToString();
            trans.Type = TransType.DEPOSIT;
            trans.Date = DateTime.Now;
            ViewData["result"] = trans.MakeTransaction(trans);
            return View();
        }
        public ActionResult Withdrawal()
        {
            if (System.Web.HttpContext.Current.Session["userName"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Withdrawal(Transactions trans)
        {
            trans.User = System.Web.HttpContext.Current.Session["userName"].ToString();
            trans.Type = TransType.WITHDRAWAL;
            trans.Date = DateTime.Now;
            ViewData["result"] = trans.MakeTransaction(trans);
            return View();
        }
    }
}