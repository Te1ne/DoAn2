using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        SimenEntities db = new SimenEntities();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult Create()
        {
            Category cate = new Category();
            return View(cate);
        }
        [HttpPost]
        public ActionResult Create(Category cate)
        {
            try
            {
                db.Categories.Add(cate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("sai roi bro");
            }
        }
    }
}