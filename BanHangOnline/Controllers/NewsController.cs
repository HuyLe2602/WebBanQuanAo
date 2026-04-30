using BanHangOnline.Models;
using System.Linq;
using System.Web.Mvc;

namespace BanHangOnline.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Danh sách bài viết
        public ActionResult Index()
        {
            var items = db.News
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();

            return View(items);
        }

        // Chi tiết bài viết
        public ActionResult Detail(string alias, int id)
        {
            var item = db.News.Find(id);

            if (item == null)
                return HttpNotFound();

            return View(item);
        }

        // Bài viết nổi bật trang chủ
        public PartialViewResult Partial_News_Home()
        {
            var items = db.News
                .Where(x => x.IsActive && x.IsHome)
                .OrderByDescending(x => x.CreatedDate)
                .Take(3)
                .ToList();

            return PartialView(items);
        }
    }
}