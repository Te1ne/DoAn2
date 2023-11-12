using DoAn.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Controllers
{
    public class ProductController : Controller
    {
        new_simenEntities1 db = new new_simenEntities1();
        // GET: Product
        public ActionResult Index(Category cate)
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
                Random r = new Random();

                if(pro.UploadImage != null)
                {
                    pro.Id = r.Next(1, 1000000000);
                    string filename = Path.GetFileNameWithoutExtension(pro.UploadImage.FileName);
                    string extent = Path.GetExtension(pro.UploadImage.FileName);
                    filename = filename + extent;
                    pro.ImagePro = "~/Content/assets/images/" + filename;
                    pro.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/assets/images/"), filename));
                    pro.Price *= 24000;
                }
                db.Products.Add(pro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
              return Content ("sai roi bro");
            }
        }

        public ActionResult Details(int id)
        {
            return View(db.Products.Where(s => s.Id == id).FirstOrDefault());
        }

        public ActionResult Edit(int id)
        {
            return View(db.Products.Where(s => s.Id == id).FirstOrDefault());
        }


        [HttpPost]

        public ActionResult Edit(int id, Product pro)
        {
            db.Entry(pro).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Delete(int id)
        {
            return View(db.Products.Where(s => s.Id == id).FirstOrDefault());
        }


        [HttpPost]

        public ActionResult Delete(int id, Product pro)
        {
            try
            {
                pro = db.Products.Where(s => s.Id == id).FirstOrDefault();
                db.Products.Remove(pro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data using in other table, Error Delete");
            }
        }
        public ActionResult SearchOption(double min=double.MinValue,double max= double.MaxValue)
        {
            var list = db.Products.Where(p=>(double)p.Price>=min &&  (double)p.Price<=max).ToList();
            return View(list);
        }
        public ActionResult MintoMax()
        {
            var products = from p in db.Products
                           orderby p.Price ascending
                           select p;
            return View(products);
        }

        public ActionResult MaxtoMin()
        {
            var products = from p in db.Products
                           orderby p.Price descending
                           select p;
            return View(products);
        }

    }
}