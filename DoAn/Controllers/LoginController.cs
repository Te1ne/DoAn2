using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        SimenEntities db = new SimenEntities();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult LoginAccount(Account _user)
        {
            var check_email = db.Accounts.Where(s => s.Email == _user.Email).FirstOrDefault();

            if (check_email == null)
            {
                ViewBag.ErrorInfo = "Email không tồn tại trong hệ thống.";
                return View("Login");
            }
            else
            {
                if (check_email.Password_User != _user.Password_User)
                {
                    ViewBag.ErrorInfo = "Mật khẩu không đúng.";
                    return View("Login");
                }
                else
                {
                    Session["Account"] = check_email;
                    Session["Email"] = _user.Email;
                    Session["Password_User"] = _user.Password_User;
                    Session["NameAccount"] = check_email.NameAccount;
                    return RedirectToAction("Index", "Home");
                }
            }
        }


        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Account _user)
        {
            if (ModelState.IsValid)
            {
                Random r = new Random();
                _user.IdAccount = r.Next(1, 100000);
                var check_ID = db.Accounts.Where(s => s.IdAccount == _user.IdAccount).FirstOrDefault();
                var check_Name = db.Accounts.Where(s => s.NameAccount == _user.NameAccount).FirstOrDefault();
                var check_email = db.Accounts.Where(s => s.Email == _user.Email).FirstOrDefault();

                if (check_Name != null)
                {
                    ModelState.AddModelError("NameAccount", "Tên tài khoản đã tồn tại trong hệ thống.");
                }

                if (check_email != null)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại trong hệ thống.");
                }

                if (check_ID == null && check_email == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Accounts.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.ErrorRegister = "This ID is exixst";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}