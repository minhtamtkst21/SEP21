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
        public void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
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
                    SetAlert("Bạn đã đăng nhập thành công", "success");
                    return RedirectToAction("Index2", "BaiViets");
                }
                else
                {
                   SetAlert("Bạn đã nhập sai tài khoản hoặc mật khẩu", "danger");
                }
            }
            else
            {
                SetAlert("Bạn đã nhập sai tài khoản hoặc mật khẩu", "danger");
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