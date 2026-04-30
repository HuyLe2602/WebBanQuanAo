using System.Web.Mvc;
using BanHangOnline.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BanHangOnline.Controllers
{
    public class HomeController : Controller
    {
        // Trang chủ
        public ActionResult Index()
        {
            return View();
        }

        // Trang giới thiệu
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        // Trang liên hệ
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        // Tạo tài khoản Admin mặc định
        public ActionResult SeedAdmin()
        {
            var context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            // Tạo Role Admin nếu chưa có
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            // Tạo Role Customer nếu chưa có
            if (!roleManager.RoleExists("Customer"))
            {
                roleManager.Create(new IdentityRole("Customer"));
            }

            // Kiểm tra tài khoản admin đã tồn tại chưa
            var user = userManager.FindByEmail("admin@gmail.com");

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FullName = "Administrator",
                    Phone = "0123456789"
                };

                // Tạo tài khoản
                var result = userManager.Create(user, "Admin@123");

                // Nếu tạo thành công -> gán quyền Admin
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                    return Content("Tạo tài khoản Admin thành công");
                }

                // Nếu lỗi -> hiện lỗi
                string errors = "";
                foreach (var error in result.Errors)
                {
                    errors += error + "<br/>";
                }

                return Content(errors);
            }

            return Content("Tài khoản Admin đã tồn tại");
        }
    }
}