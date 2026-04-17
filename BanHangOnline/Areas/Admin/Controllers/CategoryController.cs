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

        public ActionResult Add()
        {
            return View("~/Views/Category/Add.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            if (ModelState.IsValid)
            {
                // CreatedDate is declared as string in CommonAbstract, convert DateTime to string
                model.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                db.Categories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
