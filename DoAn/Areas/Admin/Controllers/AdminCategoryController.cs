using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using DoAn.Models;

namespace DoAn.Areas.Admin.Controllers
{
    public class AdminCategoryController : Controller
    {
        // GET: Admin/AdminCategory
        SimenEntities db = new SimenEntities();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        [Route("Create")]
        [HttpGet]
        public ActionResult Create()
        {
            Category pro = new Category();
            return View(pro);
        }

        [Route("Create")]
        [HttpPost]
        public ActionResult Create(Category pro)
        {
            var existingCategory = db.Categories.FirstOrDefault(c => c.NameCate == pro.NameCate);

            if (existingCategory != null)
            {
                ModelState.AddModelError("NameCate", "Tên danh mục đã tồn tại, vui lòng chọn tên khác.");
            }

            if (!ModelState.IsValid)
            {
                return View(pro);
            }

            try
            {
                Random r = new Random();
                pro.IdCate = r.Next(1, 1000000000);
                db.Categories.Add(pro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Đã có lỗi xảy ra.");
            }
        }


        public ActionResult Details(int id)
        {
            return View(db.Categories.Where(s => s.IdCate == id).FirstOrDefault());
        }

        public ActionResult Edit(int id)
        {
            return View(db.Categories.Where(s => s.IdCate == id).FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            var existingCategory = db.Categories.FirstOrDefault(c => c.NameCate == category.NameCate && c.IdCate != category.IdCate);

            if (existingCategory != null)
            {
                ModelState.AddModelError("NameCate", "Tên danh mục đã tồn tại, vui lòng chọn tên khác.");
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }
            db.Entry(category).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            return View(db.Categories.Where(s => s.IdCate == id).FirstOrDefault());
        }


        [HttpPost]

        public ActionResult Delete(int id, Category category)
        {
            try
            {
                category = db.Categories.Where(s => s.IdCate == id).FirstOrDefault();
                db.Categories.Remove(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data using in other table, Error Delete");
            }
        }
    }
}