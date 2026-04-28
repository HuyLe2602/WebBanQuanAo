using BanHangOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHangOnline.Models;

namespace BanHangOnline.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Thêm sản phẩm vào giỏ
        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity = 1)
        {
            var product = db.Products.Find(productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            var existingItem = cart.FirstOrDefault(x => x.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                // Sử dụng giá khuyến mãi nếu có, không thì dùng giá gốc
                decimal price = product.PriceSale > 0 ? product.PriceSale : product.Price;
                cart.Add(new CartItem(productId, quantity, price, product));
            }

            Session["Cart"] = cart;
            
            // Quay lại trang mà user vừa ở
            if (Request.UrlReferrer != null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            return RedirectToAction("Index");
        }

        // Hiển thị giỏ hàng
        public ActionResult Index()
        {
            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            return View(cart);
        }

        // Xóa sản phẩm khỏi giỏ
        public ActionResult Remove(int productId)
        {
            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            var item = cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
            }
            Session["Cart"] = cart;
            return RedirectToAction("Index");
        }

        // Cập nhật số lượng
        [HttpPost]
        public ActionResult Update(int productId, int quantity)
        {
            if (quantity <= 0)
            {
                return RedirectToAction("Remove", new { productId = productId });
            }

            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            var item = cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
            }
            Session["Cart"] = cart;
            return RedirectToAction("Index");
        }

        // Checkout
        public ActionResult Checkout()
        {
            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            if (!cart.Any())
            {
                return RedirectToAction("Index");
            }
            return View(cart);
        }

        [HttpPost]
        public ActionResult Checkout(string customerName, string phone, string address)
        {
            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            
            if (!cart.Any())
            {
                return RedirectToAction("Index");
            }

            if (string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(address))
            {
                ViewBag.Error = "Vui lòng điền đầy đủ thông tin";
                return View(cart);
            }

            try
            {
                var order = new Order
                {
                    Code = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                    CustomerName = customerName,
                    Phone = phone,
                    Address = address,
                    TotalAmount = Convert.ToDecimal(cart.Sum(x => x.Total)),
                    Quantity = cart.Sum(x => x.Quantity),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                db.Orders.Add(order);
                db.SaveChanges();

                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Price = Convert.ToDecimal(item.Price),
                        Quantity = item.Quantity,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };
                    db.OrderDetails.Add(orderDetail);
                }
                db.SaveChanges();

                Session["Cart"] = null; // Xóa giỏ sau checkout
                return RedirectToAction("OrderSuccess", new { id = order.Id });
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Có lỗi xảy ra: " + ex.Message;
                return View(cart);
            }
        }

        public ActionResult OrderSuccess(int id)
        {
            var order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // Chi tiết đơn hàng
        public ActionResult OrderDetail(int id)
        {
            var order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            var orderDetails = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            ViewBag.OrderDetails = orderDetails;
            return View(order);
        }

        // Lịch sử đơn hàng
        public ActionResult OrderHistory()
        {
            var orders = db.Orders.OrderByDescending(x => x.CreatedDate).ToList();
            return View(orders);
        }
    }
}