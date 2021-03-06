using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using SEP21.Models;

namespace SEP21.Controllers
{
    public class BaiVietController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();
        private DangKyHoatDong sv = new DangKyHoatDong();
        // GET: BaiViets
        public ActionResult Index()
        {
            var baiViets = db.BaiViets.Include(b => b.LoaiBaiViet1);
            return View(baiViets.ToList());
        }

        public string ToUnsignString(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            input = input.Replace(".", "-");
            input = input.Replace(" ", "-");
            input = input.Replace(",", "-");
            input = input.Replace(";", "-");
            input = input.Replace(":", "-");
            input = input.Replace("  ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            while (str2.Contains("--"))
            {
                str2 = str2.Replace("--", "-").ToLower();
            }
            return str2;
        }

        [HttpPost]
        public ActionResult Login(string username, string password, int id)
        {
            if (username != null && password != null)
            {
                var sinhvien = db.Logins.FirstOrDefault(x => x.username == username);
                if (sinhvien != null)
                {
                    if (sinhvien.password.Equals(password))
                    {
                        Session["FullName"] = sinhvien.username;
                        Session["UserID"] = sinhvien.ID;
                        Session["Password"] = sinhvien.password;
                        Session["MSSV"] = sinhvien.username.Substring(sinhvien.username.Length - 10, 10);
                        SetAlert("Bạn đã đăng nhập thành công", "success");
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                    else
                    {
                        SetAlert("Vui lòng nhập lại mật khẩu", "warning");
                    }
                }
                else
                {
                    SetAlert("Bạn đã nhập sai tài khoản hoặc mật khẩu, vui lòng nhập lại!", "warning");
                }
            }
            else
            {
                SetAlert("Bạn chưa nhập tài khoản hoặc mật khẩu, vui lòng nhập lại", "danger");
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult Login2(string username, string password)
        {
            var sinhvien = db.Logins.FirstOrDefault(x => x.username == username);
            if (sinhvien != null)
            {
                if (sinhvien.password.Equals(password))
                {
                    Session["FullName"] = sinhvien.username;
                    Session["UserID"] = sinhvien.ID;
                    Session["Password"] = sinhvien.password;
                    Session["MSSV"] = sinhvien.username.Substring(sinhvien.username.Length - 10, 10);
                    SetAlert("Bạn đã đăng nhập thành công", "success");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    SetAlert("Vui lòng nhập lại mật khẩu", "warning");
                }
            }
            else
            {
                SetAlert("Bạn đã nhập sai tài khoản hoặc mật khẩu, vui lòng nhập lại!", "warning");
            }
            return RedirectToAction("Login");
        }
        public ActionResult ChangePass()
        {
            return View();
        }
        public ActionResult ForgetPass()
        {
            return View();
        }
        public ActionResult ChangePass2(string username, string oldpassword, string newpassword, string confirmpassword)
        {
            var sinhvien = db.Logins.FirstOrDefault(x => x.username == username);
            if (sinhvien != null)
            {
                if (sinhvien.password.Equals(oldpassword))
                {
                    if (newpassword == confirmpassword)
                    {
                        sinhvien.password = newpassword;
                        SetAlert("Đổi mật khẩu thành công", "success");
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        SetAlert("Mật khẩu xác nhận không trùng khớp", "warning");
                        return RedirectToAction("ChangePass");
                    }
                }
                SetAlert("Vui lòng nhập lại mật khẩu", "warning");
                return RedirectToAction("ChangePass");
            }
            return RedirectToAction("ChangePass");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Search(string keyword)
        {
            var model = db.BaiViets.ToList();
            model = model.Where(p => p.TieuDe.ToLower().Contains(keyword.ToLower())).ToList();
            ViewBag.Keyword = keyword;
            return View("Index", "home", model);
        }
        public ActionResult DangKy(string username, int id)
        {
            sv.HoatDong = id;
            var Sinhvien = db.SinhViens.FirstOrDefault(x => x.MSSV == username.Substring(username.Length - 10, 10));
            sv.MSSV = Sinhvien.HoTen;
            sv.ThoiGianDangKy = DateTime.Today;
            sv.hd_mssv = username.ToString().Substring(username.Length - 10, 10) + "." + id.ToString();
            db.DangKyHoatDongs.Add(sv);
            db.SaveChanges();
            SetAlert("Bạn đã đăng kí thành công", "success");
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult XoaDK(string username, int id)
        {
            sv.HoatDong = id;
            var hd_mssv = username.ToString().Substring(username.Length - 10, 10) + "." + id.ToString();
            sv = db.DangKyHoatDongs.FirstOrDefault(x => x.hd_mssv == hd_mssv);
            sv = db.DangKyHoatDongs.Find(sv.ID);
            db.DangKyHoatDongs.Remove(sv);
            db.SaveChanges();
            SetAlert("Bạn đã hủy đăng kí thành công", "success");
            return Redirect(Request.UrlReferrer.ToString());
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
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Picture(int id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "images");
        }
        private const string PICTURE_PATH = "~/images/BaiViet/";
        // GET: BaiViets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            ViewData["list"] = db.DangKyHoatDongs.ToList();
            return View(baiViet);
        }

        // GET: BaiViets/Create
        public ActionResult Create()
        {
            ViewBag.LoaiBaiViet = new SelectList(db.LoaiBaiViets, "ID", "TenLoaiBaiViet");
            ViewBag.NguoiDang = new SelectList(db.NhanVienKhoas, "ID", "MaNhanVien");
            return View();
        }
        public bool CheckHD()
        {
            return false;
        }
        // POST: BaiViets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NgayDangBai,TieuDe,NguoiDang,NoiDung,LoaiBaiViet")] BaiViet baiViet)
        {
            if (ModelState.IsValid)
            {
                db.BaiViets.Add(baiViet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoaiBaiViet = new SelectList(db.LoaiBaiViets, "ID", "TenLoaiBaiViet", baiViet.LoaiBaiViet);
            return View(baiViet);
        }

        // GET: BaiViets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiBaiViet = new SelectList(db.LoaiBaiViets, "ID", "TenLoaiBaiViet", baiViet.LoaiBaiViet);
            return View(baiViet);
        }

        // POST: BaiViets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NgayDangBai,TieuDe,NguoiDang,NoiDung,LoaiBaiViet")] BaiViet baiViet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baiViet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiBaiViet = new SelectList(db.LoaiBaiViets, "ID", "TenLoaiBaiViet", baiViet.LoaiBaiViet);
            return View(baiViet);
        }

        // GET: BaiViets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            return View(baiViet);
        }

        // POST: BaiViets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaiViet baiViet = db.BaiViets.Find(id);
            db.BaiViets.Remove(baiViet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
