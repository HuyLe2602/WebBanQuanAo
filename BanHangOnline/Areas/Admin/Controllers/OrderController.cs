using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHangOnline.Models;
using BanHangOnline.Models.EF;
using BanHangOnline.Models.Common;

namespace BanHangOnline.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Hiển thị danh sách tất cả đơn hàng
        /// </summary>
        public ActionResult Index()
        {
            try
            {
                // Lấy tất cả đơn hàng, bao gồm chi tiết đơn hàng
                var orders = db.Orders
                    .Include("OrderDetails")
                    .OrderByDescending(o => o.CreatedDate)
                    .ToList();

                return View(orders);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in OrderController.Index: {ex.Message}");
                return View(new List<Order>());
            }
        }

        /// <summary>
        /// Hiển thị chi tiết một đơn hàng cụ thể
        /// </summary>
        public ActionResult Details(int id)
        {
            try
            {
                // Tìm đơn hàng theo ID, bao gồm chi tiết đơn hàng và thông tin sản phẩm
                var order = db.Orders
                    .Include("OrderDetails.Product")
                    .FirstOrDefault(o => o.Id == id);

                if (order == null)
                {
                    return HttpNotFound("Không tìm thấy đơn hàng");
                }

                return View(order);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in OrderController.Details: {ex.Message}");
                return HttpNotFound("Có lỗi khi lấy thông tin đơn hàng");
            }
        }

        /// <summary>
        /// Hiển thị form chỉnh sửa trạng thái đơn hàng
        /// </summary>
        public ActionResult Edit(int id)
        {
            try
            {
                var order = db.Orders.Find(id);

                if (order == null)
                {
                    return HttpNotFound("Không tìm thấy đơn hàng");
                }

                // Chuẩn bị dữ liệu cho dropdown status
                var statuses = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Chờ xử lý", Value = "0" },
                    new SelectListItem { Text = "Đang xử lý", Value = "1" },
                    new SelectListItem { Text = "Đã gửi hàng", Value = "2" },
                    new SelectListItem { Text = "Đã nhận hàng", Value = "3" },
                    new SelectListItem { Text = "Bị hủy", Value = "4" }
                };

                ViewBag.StatusList = statuses;
                return View(order);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in OrderController.Edit: {ex.Message}");
                return HttpNotFound("Có lỗi khi lấy thông tin đơn hàng");
            }
        }

        /// <summary>
        /// Xử lý cập nhật trạng thái đơn hàng
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var order = db.Orders.Find(id);

                if (order == null)
                {
                    return HttpNotFound("Không tìm thấy đơn hàng");
                }

                // Lấy giá trị status mới từ form
                string newStatus = collection["Status"];

                if (!string.IsNullOrWhiteSpace(newStatus))
                {
                    if (int.TryParse(newStatus, out int statusValue))
                    {
                        order.ModifiedDate = DateTime.Now;
                        db.SaveChanges();

                        TempData["SuccessMessage"] = "Cập nhật trạng thái đơn hàng thành công!";
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in OrderController.Edit POST: {ex.Message}");
                TempData["ErrorMessage"] = "Có lỗi khi cập nhật đơn hàng";
                return RedirectToAction("Edit", new { id });
            }
        }

        /// <summary>
        /// Hiển thị form xác nhận xóa đơn hàng
        /// </summary>
        public ActionResult Delete(int id)
        {
            try
            {
                var order = db.Orders.Include("OrderDetails").FirstOrDefault(o => o.Id == id);

                if (order == null)
                {
                    return HttpNotFound("Không tìm thấy đơn hàng");
                }

                return View(order);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in OrderController.Delete GET: {ex.Message}");
                return HttpNotFound("Có lỗi khi lấy thông tin đơn hàng");
            }
        }

        /// <summary>
        /// Xử lý xóa đơn hàng
        /// </summary>
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var order = db.Orders.Find(id);

                if (order == null)
                {
                    return HttpNotFound("Không tìm thấy đơn hàng");
                }

                // Xóa tất cả OrderDetails trước
                var orderDetails = db.OrderDetails.Where(od => od.OrderId == id).ToList();
                foreach (var detail in orderDetails)
                {
                    db.OrderDetails.Remove(detail);
                }

                // Xóa Order
                db.Orders.Remove(order);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Xóa đơn hàng thành công!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in OrderController.DeleteConfirmed: {ex.Message}");
                TempData["ErrorMessage"] = "Có lỗi khi xóa đơn hàng";
                return RedirectToAction("Delete", new { id });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}