using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace DoAn.Controllers
{
    public class OrderDetail_ProductController : Controller
    {
        // GET: OrderDetail_Product
        SimenEntities db = new SimenEntities();
        public ActionResult LichSuMuaHang()
        {
            var orderDetails = db.OrderDetails.Include(s => s.Product).ToList();
            return View(orderDetails);
        }
        public ActionResult Top10_Pro()
        {
            List<OrderDetail> order = db.OrderDetails.ToList();
            List<Product> pro = db.Products.ToList();
            var check = from od in order
                        join p in pro on od.Id_Product equals p.Id into dbt
                        group od by new
                        {
                            IdPro = od.Id_Product,
                            NamePro = od.Product.NamePro,
                            ImagePro = od.Product.ImagePro,
                            Price = od.Product.Price
                        } into x
                        orderby x.Sum(s => s.Quantity) ascending
                        select new Top_ViewModel
                        {
                            Id = (int)x.Key.IdPro,
                            namePro = x.Key.NamePro,
                            imagePro = x.Key.ImagePro,
                            price = x.Key.Price,
                            Top_10_Pro = x.Sum(s => s.Quantity)
                        };
            return View(check.Take(10).ToList());
        }

    }
}