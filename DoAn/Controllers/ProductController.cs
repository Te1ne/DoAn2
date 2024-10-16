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
        SimenEntities db = new SimenEntities();
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

                if (pro.UploadImage != null)
                {
                    pro.Id = r.Next(1, 1000000000);
                    string filename = Path.GetFileNameWithoutExtension(pro.UploadImage.FileName);
                    string extent = Path.GetExtension(pro.UploadImage.FileName);
                    filename = filename + extent;
                    pro.ImagePro = "~/Content/assets/images/" + filename;
                    pro.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/assets/images/"), filename));
                }
                db.Products.Add(pro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("sai roi bro");
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NamePro,Price,Size,Description_Pro,ImagePro,Color")] Product product, HttpPostedFileBase ImagePro)
        {
            var pro = db.Products.FirstOrDefault(s => s.Id == product.Id);
            if (pro != null)
            {
                pro.Id = product.Id;
                pro.NamePro = product.NamePro;
                pro.Price = product.Price;
                pro.Size = product.Size;
                pro.Color = product.Color;
                var filename = Path.GetFileName(ImagePro.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/assets/images"), filename);
                pro.ImagePro = "~/Content/assets/images/" + filename;
                ImagePro.SaveAs(path);

                pro.Categories = product.Categories;
            }
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
        public ActionResult SearchOption(double min = double.MinValue, double max = double.MaxValue)
        {
            if (min < 0 || max < 0)
            {
                ViewBag.ErrorMessage = "Giá phải lớn hơn hoặc bằng 0.";
                return View(new List<Product>());
            }

            if (min >= max)
            {
                ViewBag.ErrorMessage = "Giá tối thiểu phải nhỏ hơn giá tối đa.";
                return View(new List<Product>());
            }
            var list = db.Products.Where(p => (double)p.Price >= min && (double)p.Price <= max).ToList();
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
        public ActionResult AtLeast()
        {
            var products = from p in db.Products
                           orderby p.Price ascending
                           select p;
            return View(products.Take(1).ToList());

        }
        public ActionResult AtMax()
        {
            var products = from p in db.Products
                           orderby p.Price descending
                           select p;
            return View(products.Take(1).ToList());
        }

        public ActionResult SelectCate()
        {
            Category se_cate = new Category();
            se_cate.ListCate = db.Categories.ToList<Category>();
            return PartialView(se_cate);
        }

    }
}