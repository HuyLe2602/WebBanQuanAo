using BanHangOnline.Models;
using BanHangOnline.Models.Common;
using BanHangOnline.Models.EF;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/News
        public ActionResult Index()
        {
            var items = db.News.OrderByDescending(x => x.CreatedDate).ToList();
            return View(items);
        }

        // GET: Admin/News/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: Admin/News/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(News model)
        {
            if (ModelState.IsValid)
            {
                model.Alias = BanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;

                db.News.Add(model);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var item = db.News.Find(id);
            if (item == null)
                return HttpNotFound();

            return View(item);
        }

        // POST: Admin/News/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(News model)
        {
            if (ModelState.IsValid)
            {
                var item = db.News.Find(model.Id);
                if (item == null)
                    return HttpNotFound();

                item.Title = model.Title;
                model.Alias = BanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                item.Description = model.Description;
                item.Detail = model.Detail;
                item.Image = model.Image;
                item.IsActive = model.IsActive;
                item.IsHome = model.IsHome;
                item.IsHot = model.IsHot;
                item.Position = model.Position;
                item.ModifiedDate = DateTime.Now;

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var item = db.News.Find(id);
            if (item == null)
                return HttpNotFound();

            return View(item);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var item = db.News.Find(id);
            if (item == null)
                return HttpNotFound();

            db.News.Remove(item);
            db.SaveChanges();

            return RedirectToAction("Index");
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