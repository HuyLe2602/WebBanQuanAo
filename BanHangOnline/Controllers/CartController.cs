using BanHangOnline.Models;
using BanHangOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BanHangOnline.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // =============================
        // LẤY GIỎ HÀNG
        // =============================
        private List<CartItem> GetCart()
        {
            if (Session["Cart"] == null)
            {
                Session["Cart"] = new List<CartItem>();
            }
            return (List<CartItem>)Session["Cart"];
        }

        // =============================
        // LƯU GIỎ HÀNG
        // =============================
        private void SaveCart(List<CartItem> cart)
        {
            Session["Cart"] = cart;
        }

        // =============================
        // THÊM VÀO GIỎ (AJAX)
        // =============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddToCart(int productId, int quantity = 1, string size = "M")
        {
            var product = db.Products.Find(productId);
            if (product == null)
            {
                return Json(new { success = false, message = "Sản phẩm không tồn tại" });
            }

            var cart = GetCart();

            var existingItem = cart.FirstOrDefault(x => x.ProductId == productId && x.Size == size);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                decimal price = product.PriceSale > 0 ? product.PriceSale : product.Price;

                cart.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = product.Title,
                    Image = product.Image,
                    Quantity = quantity,
                    Price = price,
                    Size = size
                });
            }

            SaveCart(cart);

            return Json(new
            {
                success = true,
                count = cart.Sum(x => x.Quantity)
            });
        }

        // =============================
        // ĐẾM SỐ LƯỢNG GIỎ
        // =============================
        public JsonResult GetCartCount()
        {
            var cart = GetCart();
            return Json(cart.Sum(x => x.Quantity), JsonRequestBehavior.AllowGet);
        }

        // =============================
        // HIỂN THỊ GIỎ HÀNG
        // =============================
        public ActionResult Index()
        {
            return View(GetCart());
        }

        // =============================
        // XÓA (AJAX)
        // =============================
        public JsonResult Remove(int productId, string size)
        {
            var cart = GetCart();

            var item = cart.FirstOrDefault(x => x.ProductId == productId && x.Size == size);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }

            return Json(new
            {
                success = true,
                count = cart.Sum(x => x.Quantity)
            }, JsonRequestBehavior.AllowGet);
        }

        // =============================
        // UPDATE (AJAX)
        // =============================
        [HttpPost]
        public JsonResult Update(int productId, string size, int quantity)
        {
            var cart = GetCart();

            var item = cart.FirstOrDefault(x => x.ProductId == productId && x.Size == size);

            if (item != null)
            {
                if (quantity <= 0)
                    cart.Remove(item);
                else
                    item.Quantity = quantity;

                SaveCart(cart);
            }

            return Json(new
            {
                success = true,
                total = cart.Sum(x => x.Price * x.Quantity),
                count = cart.Sum(x => x.Quantity)
            });
        }

        // =============================
        // CHECKOUT (GET)
        // =============================
        public ActionResult Checkout()
        {
            var cart = GetCart();
            if (!cart.Any())
                return RedirectToAction("Index");

            return View(cart);
        }

        // =============================
        // CHECKOUT (AJAX)
        // =============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Checkout(string customerName, string phone, string address, string paymentMethod)
        {
            var cart = GetCart();

            if (!cart.Any())
                return Json(new { success = false, message = "Giỏ hàng trống" });

            if (string.IsNullOrWhiteSpace(customerName) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(address))
            {
                return Json(new { success = false, message = "Vui lòng nhập đầy đủ thông tin" });
            }

            try
            {
                var order = new Order
                {
                    Code = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                    CustomerName = customerName,
                    Phone = phone,
                    Address = address,
                    TotalAmount = cart.Sum(x => x.Price * x.Quantity),
                    Quantity = cart.Sum(x => x.Quantity),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                db.Orders.Add(order);
                db.SaveChanges();

                foreach (var item in cart)
                {
                    db.OrderDetails.Add(new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    });
                }

                db.SaveChanges();

                // clear cart
                Session["Cart"] = null;

                return Json(new
                {
                    success = true,
                    orderId = order.Id
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        // =============================
        // THÀNH CÔNG
        // =============================
        public ActionResult OrderSuccess(int id)
        {
            var order = db.Orders.Find(id);
            if (order == null)
                return HttpNotFound();

            return View(order);
        }

        // =============================
        // CHI TIẾT ĐƠN
        // =============================
        public ActionResult OrderDetail(int id)
        {
            var order = db.Orders.Find(id);
            if (order == null)
                return HttpNotFound();

            ViewBag.OrderDetails = db.OrderDetails
                                     .Where(x => x.OrderId == id)
                                     .ToList();

            return View(order);
        }

        // =============================
        // LỊCH SỬ
        // =============================
        public ActionResult OrderHistory()
        {
            return View(db.Orders
                          .OrderByDescending(x => x.CreatedDate)
                          .ToList());
        }
    }
}