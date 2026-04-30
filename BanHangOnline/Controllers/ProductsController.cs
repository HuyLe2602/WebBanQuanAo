using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using BanHangOnline.Models;
using BanHangOnline.Models.EF;

namespace BanHangOnline.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // =========================
        // DANH SÁCH SẢN PHẨM THEO DANH MỤC
        // =========================
        public ActionResult ProductCategory(string alias, int id)
        {
            var category = db.ProductCategories
                             .FirstOrDefault(x => x.Id == id && x.IsActive);

            if (category == null)
            {
                return HttpNotFound();
            }

            var products = db.Products
                             .Include(x => x.ProductCategory)
                             .Where(x => x.ProductCategoryId == id)
                             .OrderByDescending(x => x.Id)
                             .ToList();

            ViewBag.Category = category;
            return View(products);
        }

        // =========================
        // CHI TIẾT SẢN PHẨM
        // =========================
        public ActionResult Detail(int id)
        {
            var item = db.Products.Include("ProductCategory")
                .FirstOrDefault(x => x.Id == id);

            if (item == null)
                return HttpNotFound();

            return View(item);
        }

        // =========================
        // LOAD SẢN PHẨM THEO DANH MỤC (PARTIAL)
        // =========================
        public ActionResult Partial_ItemsByCateId(int? id)
        {
            var items = db.Products.Include("ProductCategory").AsQueryable();

            if (id.HasValue)
            {
                items = items.Where(x => x.ProductCategoryId == id.Value);
            }

            return PartialView("_Partial_ItemsByCateId", items.ToList());
        }

        // =========================
        // SẢN PHẨM MỚI
        // =========================
        public ActionResult Partial_NewProducts()
        {
            var items = db.Products
                          .OrderByDescending(x => x.Id)
                          .Take(8)
                          .ToList();

            return PartialView("_Partial_NewProducts", items);
        }

        // =========================
        // SẢN PHẨM NỔI BẬT
        // =========================
        public ActionResult Partial_FeatureProducts()
        {
            var items = db.Products
                          .Where(x => x.IsFeature)
                          .OrderByDescending(x => x.Id)
                          .Take(8)
                          .ToList();

            return PartialView("_Partial_FeatureProducts", items);
        }

        // =========================
        // 🔥 BEST SELLER (ĐÃ FIX)
        // =========================
        public ActionResult Partial_BestSeller()
        {
            var items = db.Products
                          .Where(x => x.IsActive)
                          .OrderByDescending(x => x.Id) // nếu có Sold thì đổi sang Sold
                          .Take(8)
                          .ToList();

            return PartialView("_Partial_BestSeller", items);
        }

        // =========================
        // 🔒 GIẢI PHÓNG KẾT NỐI DB (TRÁNH LỖI NGẦM)
        // =========================
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