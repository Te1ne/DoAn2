using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Controllers
{
    public class UserController : Controller
    {
        new_simenEntities1 db = new new_simenEntities1();
        // GET: User
        public ActionResult Index(int id)
        {
            return View(db.Accounts.Where(s => s.IdAccount == id).FirstOrDefault());
        }
    }
}