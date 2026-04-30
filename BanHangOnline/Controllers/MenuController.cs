using System.Linq;
using System.Web.Mvc;
using BanHangOnline.Models;

namespace BanHangOnline.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Menu danh mục ở banner trang chủ
        public ActionResult MenuProductCategory()
        {
            var items = db.ProductCategories
                .Where(x => x.IsActive)
                .OrderBy(x => x.Position)
                .Take(3)
                .ToList();

            return PartialView("_MenuProductCategory", items);
        }

        // Menu filter New Arrivals
        public ActionResult MenuArrivals()
        {
            var items = db.ProductCategories
                .Where(x => x.IsActive)
                .OrderBy(x => x.Position)
                .ToList();

            return PartialView("_MenuArrivals", items);
        }

        // Menu top
        public ActionResult MenuTop()
        {
            var items = db.Categories
                .Where(x => x.IsActive)
                .OrderBy(x => x.Position)
                .ToList();

            return PartialView("_MenuTop", items);
        }

        // Menu bottom
        public ActionResult MenuBottom()
        {
            var items = db.Categories
                .Where(x => x.IsActive)
                .OrderBy(x => x.Position)
                .ToList();

            return PartialView("_MenuBottom", items);
        }
    }
}