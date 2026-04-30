using System;
using System.Linq;
using System.Web.Mvc;
using BanHangOnline.Models;
using BanHangOnline.Models.EF;

namespace BanHangOnline.Areas.Admin.Controllers
{
    public class SeedController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            // ======================
            // 1. DANH MỤC
            // ======================
            if (!db.ProductCategories.Any())
            {
                var cat1 = new ProductCategory
                {
                    Title = "Áo",
                    Alias = "ao",
                    IsActive = true,
                    Position = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                var cat2 = new ProductCategory
                {
                    Title = "Quần",
                    Alias = "quan",
                    IsActive = true,
                    Position = 2,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                var cat3 = new ProductCategory
                {
                    Title = "Giày",
                    Alias = "giay",
                    IsActive = true,
                    Position = 3,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                db.ProductCategories.Add(cat1);
                db.ProductCategories.Add(cat2);
                db.ProductCategories.Add(cat3);
                db.SaveChanges();
            }

            // ======================
            // 2. SẢN PHẨM
            // ======================
            if (!db.Products.Any())
            {
                var catId = db.ProductCategories.First().Id;

                db.Products.Add(new Product
                {
                    Title = "Áo sơ mi basic",
                    Alias = "ao-so-mi-basic",
                    ProductCategoryId = catId,
                    Price = 250000,
                    PriceSale = 199000,
                    Quantity = 50,
                    Description = "Áo sơ mi form rộng",
                    Detail = "Chất vải cotton, thoáng mát",
                    Image = "",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                });

                db.Products.Add(new Product
                {
                    Title = "Áo thun local brand",
                    Alias = "ao-thun-local",
                    ProductCategoryId = catId,
                    Price = 180000,
                    PriceSale = 150000,
                    Quantity = 100,
                    Description = "Áo thun trẻ trung",
                    Detail = "Form oversize",
                    Image = "",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                });

                db.SaveChanges();
            }

            return Content("✅ Seed dữ liệu thành công!");
        }
    }
}