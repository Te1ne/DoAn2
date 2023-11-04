using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Controllers
{
    public class ShoppingCartController : Controller
    {
        new_simenEntities1 db = new new_simenEntities1();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
            {
                return View("EmptyCart");

            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(int id)
        {
            var _pro = db.Products.SingleOrDefault(s => s.Id == id);
            if (_pro != null && Session["Email"] != null) 
            {
                GetCart().Add_Product_Cart(_pro);
                return RedirectToAction("ShowCart", "ShoppingCart");
            }
            return RedirectToAction("Login", "Login");
        }
        public ActionResult Update_Cart_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["idPro"]);
            int _quantity = int.Parse(form["cartQuantity"]);
            cart.Update_quantity(id_pro, _quantity);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int total_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                total_quantity_item = cart.Total_quantity();
                ViewBag.QuantityCart = total_quantity_item;
            }
            return PartialView("BagCart");
        }

        public PartialViewResult MoneyCart()
        {
            decimal total_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                total_quantity_item = cart.Total_money();
                ViewBag.QuantityCart = total_quantity_item;
            }
            return PartialView("BagCart");
        }


        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                OrderProduct _order = new OrderProduct();
                _order.DateTime = DateTime.Now;
                _order.Address = form["AddressDelivery"];
                _order.ID_Account = int.Parse(form["CodeCustomer"]);
                db.OrderProducts.Add(_order);
                foreach (var item in cart.Items)
                {
                    OrderDetail _order_detail = new OrderDetail();
                    _order_detail.IdOrder = _order.Id_OderPro;
                    _order_detail.Id_Product = item._product.Id;
                    _order_detail.UnitPrice = item._product.Price;
                    _order_detail.Quantity = item._quantity;
                    db.OrderDetails.Add(_order_detail);
                }
                cart.ClearCart(); 
                return RedirectToAction("CheckOut_Success", "ShoppingCart");
            }
            catch
            {
                return Content("Error checkout, Please check information of Customer....Thank you so much<3.");
            }

        }
        public ActionResult CheckOut_Success()
        {
            return View();
        }


    }
}