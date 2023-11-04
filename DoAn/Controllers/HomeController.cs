using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Controllers
{
    public class HomeController : Controller
    {
        new_simenEntities1 db = new new_simenEntities1();
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public PartialViewResult NoLogin()
        {
            return new PartialViewResult();
        }

    }
}