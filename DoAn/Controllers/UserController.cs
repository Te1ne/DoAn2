using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Controllers
{
    public class UserController : Controller
    {
        SimenEntities db = new SimenEntities();
        // GET: User
        public ActionResult Index(int id)
        {
            return View(db.Accounts.Where(s => s.IdAccount == id).FirstOrDefault());
        }

        public ActionResult Edit(int id)
        {
            return View(db.Accounts.Where(s => s.IdAccount == id).FirstOrDefault());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAccount,Email,PhoneNumber,NameAccount,City,Password_User")] Account account)
        {
            var pro = db.Accounts.FirstOrDefault(s => s.IdAccount == account.IdAccount);
            if (pro != null)
            {
                pro.IdAccount = account.IdAccount;
                pro.NameAccount = account.NameAccount;
                pro.Email = account.Email;
                pro.PhoneNumber = account.PhoneNumber;
                pro.City = account.City;
                pro.Password_User = account.Password_User;
                pro.ConfirmPass = account.Password_User;

            }
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                Console.WriteLine(ex);
            }
            return RedirectToAction("Index/" + pro.IdAccount);
        }

    }
}