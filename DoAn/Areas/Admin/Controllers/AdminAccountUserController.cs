using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Areas.Admin.Controllers
{
    public class AdminAccountUserController : Controller
    {
        // GET: Admin/AdminAccountUser
        SimenEntities db = new SimenEntities();
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

        public ActionResult Details(int id)
        {
            return View(db.Accounts.Where(s => s.IdAccount == id).FirstOrDefault());
        }

        public ActionResult Delete(int id)
        {
            return View(db.Accounts.Where(s => s.IdAccount == id).FirstOrDefault());
        }


        [HttpPost]

        public ActionResult Delete(int id, Account account)
        {
            try
            {
                account = db.Accounts.Where(s => s.IdAccount == id).FirstOrDefault();
                db.Accounts.Remove(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data using in other table, Error Delete");
            }
        }

        public ActionResult SearchAccount(string searchName)
        {
            if (string.IsNullOrEmpty(searchName))
            {
                ViewBag.ErrorMessage = "Vui lòng nhập tên người dùng để tìm kiếm.";
                return View("Index", new List<Account>());
            }

            var list = db.Accounts.Where(a => a.NameAccount.Contains(searchName)).ToList();

            if (!list.Any())
            {
                ViewBag.ErrorMessage = "Không tìm thấy người dùng nào.";
            }

            return View("Index", list);
        }

    }
}