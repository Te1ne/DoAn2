using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Controllers
{
    public class HomeController : Controller
    {
        SimenEntities db = new SimenEntities();
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