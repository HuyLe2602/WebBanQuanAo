using BanHangOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Products
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Partial_ItemsByCateId()
        {
            var items = db.Products.Where(x => x.IsHome).Take(12).ToList();
            return PartialView("_ItemsByCateId", items);
        }
    }
}