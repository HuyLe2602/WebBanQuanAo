using BanHangOnline.Models;
using BanHangOnline.Models.EF;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdvertisementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var items = db.Advs.OrderByDescending(x => x.CreatedDate).ToList();
            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Adv model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                db.Advs.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var item = db.Advs.Find(id);
            if (item == null) return HttpNotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Adv model)
        {
            if (ModelState.IsValid)
            {
                var item = db.Advs.Find(model.Id);
                if (item == null) return HttpNotFound();

                item.Title = model.Title;
                item.Description = model.Description;
                item.Image = model.Image;
                item.Link = model.Link;
                item.Type = model.Type;
                item.ModifiedDate = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var item = db.Advs.Find(id);
            if (item == null) return HttpNotFound();

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var item = db.Advs.Find(id);
            if (item == null) return HttpNotFound();

            db.Advs.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}