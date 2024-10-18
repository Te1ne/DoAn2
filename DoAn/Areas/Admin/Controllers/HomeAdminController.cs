using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;
using System.Data.Entity;

namespace DoAn.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        SimenEntities db = new SimenEntities();
        [Route("")]
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAdmin(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                return RedirectToAction("DanhMucSanPham", "HomeAdmin", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorMessage = "Tài khoản hoặc mật khẩu không đúng!";
                return View();
            }
        }

        public ActionResult DanhMucSanPham()
        {
            return View(db.Products.ToList());
        }

        [Route("Create")]
        [HttpGet]
        public ActionResult Create()
        {
            Product pro = new Product();
            return View(pro);
        }

        [Route("Create")]
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
                if (!ModelState.IsValid)
                {
                    return View(pro);
                }

                db.Products.Add(pro);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
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

        public ActionResult LichSuMuaHang()
        {
            var orderDetails = db.OrderDetails.Include(s => s.Product).ToList();
            return View(orderDetails);
        }
        public ActionResult CapNhatTrangThaiDonHang(int id)
        {
            return View(db.OrderDetails.Where(s => s.IdOrder == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult CapNhatTrangThaiDonHang(OrderDetail orderDetail)
        {
            var orderDetailInDb = db.OrderDetails.FirstOrDefault(s => s.IdOrder == orderDetail.IdOrder);
            orderDetailInDb.StateDelivery = orderDetail.StateDelivery;

            db.SaveChanges();

            return RedirectToAction("LichSuMuaHang");
        }

        public ActionResult SearchProduct(string searchName)
        {
            if (string.IsNullOrEmpty(searchName))
            {
                ViewBag.ErrorMessage = "Vui lòng nhập tên sản phẩm để tìm kiếm.";
                return View("DanhMucSanPham", new List<Product>());
            }

            var list = db.Products.Where(a => a.NamePro.Contains(searchName)).ToList();

            if (!list.Any())
            {
                ViewBag.ErrorMessage = "Không tìm thấy sản phẩm nào.";
            }

            return View("DanhMucSanPham", list);
        }

        public ActionResult SearchOrder(int? id)
        {
            if (!id.HasValue || id <= 0)
            {
                ViewBag.ErrorMessage = "Vui lòng nhập ID";
                return View("LichSuMuaHang", new List<OrderDetail>());
            }

            if (id <= 0)
            {
                ViewBag.ErrorMessage = "Vui lòng nhập ID sản phẩm hợp lệ.";
                return View("LichSuMuaHang", new List<OrderDetail>());
            }
            var list = db.OrderDetails.Where(a => a.Id_Orderdt == id).ToList();

            if (!list.Any())
            {
                ViewBag.ErrorMessage = "Không tìm thấy Order nào.";
            }

            return View("LichSuMuaHang", list);
        }


    }
}