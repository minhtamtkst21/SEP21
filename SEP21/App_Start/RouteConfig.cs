using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SEP21
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "tintuc",
                url: "Tin-tuc/{Title}-{id}",
                defaults: new { controller = "BaiViet", action = "Details", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "gioithieu",
                url: "Gioi-thieu",
                defaults: new { controller = "GioiThieus", action = "Index"}
            );

            routes.MapRoute(
                name: "cctc",
                url: "Co-cau-to-chuc",
                defaults: new { controller = "CanBoKhoa", action = "Index" }
            );

            routes.MapRoute(
                name: "daotao",
                url: "Dao-tao",
                defaults: new { controller = "DaoTao", action = "Index" }
            );

            routes.MapRoute(
                name: "login",
                url: "Dang-nhap",
                defaults: new { controller = "BaiViet", action = "Login" }
            );

            routes.MapRoute(
                name: "tuyendung",
                url: "Tuyen-dung",
                defaults: new { controller = "TuyenDung", action = "Index" }
            );

            routes.MapRoute(
                name: "dmk",
                url: "Doi-mat-khau",
                defaults: new { controller = "BaiViet", action = "ChangePass" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
