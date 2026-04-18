using BanHangOnline.Models;
using BanHangOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Category
        public ActionResult Index()
        {
            // reuse root Categories list view (or you can create admin-specific views under Areas/Admin/Views/Category)
            var items = db.Categories;
            return View("~/Views/Category/Index.cshtml", items);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var item = db.Categories.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            db.Categories.Remove(item);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            return View("~/Views/Category/Add.cshtml");
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int id)
        {
            var item = db.Categories.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View("~/Views/Category/Edit.cshtml", item);
        }

        // POST: Admin/Category/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                var item = db.Categories.Find(model.Id);
                if (item == null)
                {
                    return HttpNotFound();
                }

                // update fields
                item.Title = model.Title;
                item.Description = model.Description;
                item.Position = model.Position;
                item.seoTitle = model.seoTitle;
                item.seoDescription = model.seoDescription;
                item.seoKeywords = model.seoKeywords;
                item.ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                item.Alias = BanHangOnline.Models.Common.Filter.ChuyenCoDauThanhKhongDau(model.Title);
                item.Alias = BanHangOnline.Models.Common.Filter.FilterChar(item.Alias);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            if (ModelState.IsValid)
            {
                // CreatedDate is declared as string in CommonAbstract, convert DateTime to string
                model.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                model.ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                model.Alias = BanHangOnline.Models.Common.Filter.ChuyenCoDauThanhKhongDau(model.Title);
                model.Alias = BanHangOnline.Models.Common.Filter.FilterChar(model.Alias);
                db.Categories.Add(model);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
