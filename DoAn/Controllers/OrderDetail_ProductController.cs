using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Controllers
{
    public class OrderDetail_ProductController : Controller
    {
        // GET: OrderDetail_Product
        new_simenEntities1 db = new new_simenEntities1();
        public ActionResult Index()
        {
            return View();
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