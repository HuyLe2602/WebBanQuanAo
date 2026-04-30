using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BanHangOnline.Models;
using BanHangOnline.Models.Common;
using BanHangOnline.Models.EF;

namespace BanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // =========================
        // DANH SÁCH + SEARCH
        // =========================
        public ActionResult Index(string search)
        {
            IQueryable<Product> items = db.Products
                                          .Include(x => x.ProductCategory);

            if (!string.IsNullOrEmpty(search))
            {
                items = items.Where(x => x.Title.Contains(search));
            }

            var result = items
                .OrderByDescending(x => x.Id)
                .AsNoTracking()
                .ToList();

            return View(result);
        }

        // =========================
        // CREATE - GET
        // =========================
        public ActionResult Create()
        {
            LoadProductCategoryDropDown();
            return View();
        }

        // =========================
        // CREATE - POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product model, HttpPostedFileBase uploadImage)
        {
            LoadProductCategoryDropDown(model.ProductCategoryId);

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.IsActive = true;
                model.Alias = BanHangOnline.Models.Common.Filter.FilterChar(model.Title);

                model.Image = SaveImage(uploadImage);

                db.Products.Add(model);
                db.SaveChanges();

                TempData["Success"] = "Thêm sản phẩm thành công!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message);
                return View(model);
            }
        }

        // =========================
        // EDIT - GET
        // =========================
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var item = db.Products.Find(id);
            if (item == null)
                return HttpNotFound();

            LoadProductCategoryDropDown(item.ProductCategoryId);
            return View(item);
        }

        // =========================
        // EDIT - POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model, HttpPostedFileBase uploadImage)
        {
            LoadProductCategoryDropDown(model.ProductCategoryId);

            if (!ModelState.IsValid)
                return View(model);

            var item = db.Products.Find(model.Id);
            if (item == null)
                return HttpNotFound();

            try
            {
                item.Title = model.Title;
                item.Description = model.Description;
                item.Detail = model.Detail;
                item.Price = model.Price;
                item.PriceSale = model.PriceSale;
                item.Quantity = model.Quantity;
                item.ProductCategoryId = model.ProductCategoryId;
                item.ModifiedDate = DateTime.Now;
                model.Alias = BanHangOnline.Models.Common.Filter.FilterChar(model.Title);

                var newImage = SaveImage(uploadImage);
                if (!string.IsNullOrEmpty(newImage))
                {
                    item.Image = newImage;
                }

                db.SaveChanges();

                TempData["Success"] = "Cập nhật thành công!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message);
                return View(model);
            }
        }

        // =========================
        // DELETE
        // =========================
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var item = db.Products
                         .Include(x => x.ProductCategory)
                         .FirstOrDefault(x => x.Id == id);

            if (item == null)
                return HttpNotFound();

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var item = db.Products.Find(id);

            if (item != null)
            {
                db.Products.Remove(item);
                db.SaveChanges();
            }

            TempData["Success"] = "Đã xóa!";
            return RedirectToAction("Index");
        }

        // =========================
        // TOGGLE ACTIVE
        // =========================
        [HttpPost]
        public ActionResult ToggleActive(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // =========================
        // SAVE IMAGE (TÁCH RIÊNG)
        // =========================
        private string SaveImage(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
                return null;

            var fileName = Path.GetFileName(file.FileName);
            var newFileName = Guid.NewGuid() + "_" + fileName;

            var folder = Server.MapPath("~/Content/assets/images/");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var path = Path.Combine(folder, newFileName);
            file.SaveAs(path);

            return "/Content/assets/images/" + newFileName;
        }

        // =========================
        // DROPDOWN CATEGORY
        // =========================
        private void LoadProductCategoryDropDown(object selected = null)
        {
            var items = db.ProductCategories
                          .Where(x => x.IsActive)
                          .OrderBy(x => x.Position)
                          .ToList();

            var result = new List<SelectListItem>();
            var parents = items.Where(x => x.ParentId == null).ToList();

            foreach (var parent in parents)
            {
                result.Add(new SelectListItem
                {
                    Value = parent.Id.ToString(),
                    Text = parent.Title
                });

                AddChildCategories(items, result, parent, 1);
            }

            ViewBag.ProductCategoryId = new SelectList(result, "Value", "Text", selected);
        }

        private void AddChildCategories(
            List<ProductCategory> source,
            List<SelectListItem> result,
            ProductCategory parent,
            int level)
        {
            var children = source
                .Where(x => x.ParentId == parent.Id)
                .OrderBy(x => x.Position)
                .ToList();

            foreach (var child in children)
            {
                result.Add(new SelectListItem
                {
                    Value = child.Id.ToString(),
                    Text = new string('-', level * 2) + " " + child.Title
                });

                AddChildCategories(source, result, child, level + 1);
            }
        }

        // =========================
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}