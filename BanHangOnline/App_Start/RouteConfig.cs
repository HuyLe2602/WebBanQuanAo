using System.Web.Mvc;
using System.Web.Routing;

namespace BanHangOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // =========================
            // TRANG CHỦ
            // =========================
            routes.MapRoute(
                name: "TrangChu",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BanHangOnline.Controllers" }
            );

            // =========================
            // GIỎ HÀNG
            // =========================
            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BanHangOnline.Controllers" }
            );

            // =========================
            // TIN TỨC
            // =========================
            routes.MapRoute(
                name: "NewsDetail",
                url: "tin-tuc/{alias}-n{id}",
                defaults: new { controller = "News", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "BanHangOnline.Controllers" }
            );

            routes.MapRoute(
                name: "News",
                url: "tin-tuc",
                defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BanHangOnline.Controllers" }
            );

            // =========================
            // SẢN PHẨM
            // =========================
            routes.MapRoute(
                name: "ProductDetail",
                url: "chi-tiet/{alias}-p{id}",
                defaults: new { controller = "Products", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "BanHangOnline.Controllers" }
            );

            routes.MapRoute(
                name: "ProductCategory",
                url: "danh-muc/{alias}-{id}",
                defaults: new { controller = "Products", action = "ProductCategory", id = UrlParameter.Optional },
                namespaces: new[] { "BanHangOnline.Controllers" }
            );

            // =========================
            // DANH MỤC BÀI VIẾT / LIÊN HỆ (nếu dùng sau này)
            // =========================
            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BanHangOnline.Controllers" }
            );


            // =========================
            // ROUTE MẶC ĐỊNH (LUÔN ĐẶT CUỐI)
            // =========================
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BanHangOnline.Controllers" }
            );
        }
    }
}