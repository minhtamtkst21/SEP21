using SEP21.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEP21.Areas.QuanLy.Controllers
{
    public class ManagerAdminController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();
        // GET: QuanLy/ManagerAdmin
        public ActionResult Login()
        {
            ViewBag.Message = "Login";
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {

            var admin = db.LoginAdmins.FirstOrDefault(x => x.username == username);
            if (admin != null)
            {
                if (admin.password.Equals(password))
                {
                    Session["FullName"] = admin.username;
                    Session["UserID"] = admin.ID;
                    return RedirectToAction("Index", "AdminHome");
                }
            }
            else
            {
                ViewBag.Message = "tài khoản không tồn tại";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}