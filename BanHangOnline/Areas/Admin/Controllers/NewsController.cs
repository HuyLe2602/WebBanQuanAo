using BanHangOnline.Models;
using BanHangOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {   
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/News
        public ActionResult Index()
        {
            var items = db.News.OrderByDescending(x => x.Id).ToList();
            return View(items);
        }

        public ActionResult Add() {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add( News model)
        {
            if (ModelState.IsValid)
            {
                // Ensure required fields exist (Title is used to build Alias)
                if (string.IsNullOrWhiteSpace(model.Title))
                {
                    ModelState.AddModelError("Title", "Tiêu đề không được để trống");
                    return View(model);
                }

                if (model != null)
                {
                    var now = System.DateTime.Now;
                    model.CreatedDate = now;
                    model.ModifiedDate = now;
                }
                model.Alias = BanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                db.News.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}