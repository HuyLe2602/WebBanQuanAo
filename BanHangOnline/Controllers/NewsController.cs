using System.Web.Mvc;

namespace BanHangOnline.Controllers
{
    // Lightweight redirecting controller so requests to /news are sent to the Admin area
    public class NewsController : Controller
    {
        // GET: /News
        public ActionResult Index()
        {
            return RedirectToAction("Index", "News", new { area = "Admin" });
        }

        // GET: /News/Add
        public ActionResult Add()
        {
            return RedirectToAction("Add", "News", new { area = "Admin" });
        }

        // GET: /News/Edit/5
        public ActionResult Edit(int id)
        {
            return RedirectToAction("Edit", "News", new { area = "Admin", id = id });
        }
    }
}
