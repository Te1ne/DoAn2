using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Controllers
{
    public class ProductController : Controller
    {
        SimenEntities db = new SimenEntities();
        // GET: Product
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
        public ActionResult Create()
        {
            Product pro = new Product();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Create(Product pro)
        {
            try
            {
                db.Products.Add(pro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
              return Content ("sai roi bro");
            }
        }
    }
}