using System.Web.Mvc;

namespace SEP21.Areas.QuanLy
{
    public class QuanLyAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "QuanLy";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "QuanLy_default",
                "QuanLy/{controller}/{action}/{id}",
                new { controller = "ManagerAdmin", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}