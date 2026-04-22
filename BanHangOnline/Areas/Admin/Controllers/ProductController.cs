using BanHangOnline.Models;
using BanHangOnline.Models.EF;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BanHangOnline.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Product/Add
        public ActionResult Add()
        {
            // populate categories for dropdown in add view
            ViewBag.Categories = new SelectList(db.ProductCategories, "Id", "Title");
            // return explicit area view so MVC finds it reliably
            return View("~/Areas/Admin/Views/Product/Add.cshtml");
        }

        // POST: Admin/Product/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.Title))
                {
                    ModelState.AddModelError("Title", "Tên sản phẩm không được để trống");
                    // return explicit area view so validation messages appear
                    return View("~/Areas/Admin/Views/Product/Add.cshtml", model);
                }

                var now = DateTime.Now;
                model.CreatedDate = now;
                model.ModifiedDate = now;

                // build alias from title (use existing filter helper if available)
                try
                {
                    model.Alias = BanHangOnline.Models.Common.Filter.ChuyenCoDauThanhKhongDau(model.Title);
                    model.Alias = BanHangOnline.Models.Common.Filter.FilterChar(model.Alias);
                }
                catch
                {
                    model.Alias = model.Title;
                }

                db.Products.Add(model);
                db.SaveChanges();
                // Redirect to admin product index
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }

            // If validation fails return explicit view
            return View("~/Areas/Admin/Views/Product/Add.cshtml", model);
        }

        // GET: Admin/Product (simple list redirect or view placeholder)
        public ActionResult Index()
        {
            var items = db.Products.OrderByDescending(x => x.Id).ToList();
            // return explicit area view to avoid view resolution issues
            return View("~/Areas/Admin/Views/Product/Index.cshtml", items);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(400);
            var item = db.Products.Find(id.Value);
            if (item == null) return HttpNotFound();
            // populate categories for dropdown if needed
            ViewBag.Categories = new SelectList(db.ProductCategories, "Id", "Title", item.ProductCategoryId);
            return View("~/Areas/Admin/Views/Product/Edit.cshtml", item);
        }

        // POST: Admin/Product/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                var existing = db.Products.Find(model.Id);
                if (existing == null) return HttpNotFound();
                existing.Title = model.Title;
                existing.ProductCode = model.ProductCode;
                existing.Description = model.Description;
                existing.Detail = model.Detail;
                existing.Price = model.Price;
                existing.PriceSale = model.PriceSale;
                existing.Quantity = model.Quantity;
                existing.Image = model.Image;
                existing.ProductCategoryId = model.ProductCategoryId;
                existing.ModifiedDate = DateTime.Now;
                db.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
            ViewBag.Categories = new SelectList(db.ProductCategories, "Id", "Title", model.ProductCategoryId);
            return View("~/Areas/Admin/Views/Product/Edit.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var item = db.Products.Find(id);
            if (item == null) return HttpNotFound();
            db.Products.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }
    }
}
