using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using SEP21.Models;

namespace SEP21.Areas.QuanLy.Controllers
{
    public class BaiVietsController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();
        // GET: QuanLy/BaiViets
        public ActionResult Index()
        {
            var baiViets = db.BaiViets.Include(b => b.LoaiBaiViet1).Include(b => b.NhanVienKhoa);
            return View(baiViets.ToList());
        }
        public ActionResult Index2()
        {
            var baiViets = db.BaiViets.Include(b => b.LoaiBaiViet1).Include(b => b.NhanVienKhoa);
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

        // GET: QuanLy/BaiViets/Details/5
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
            return View(baiViet);
        }
        public ActionResult Picture(int id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "images");
        }
        private const string PICTURE_PATH = "~/images/BaiViet/";
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
        // GET: QuanLy/BaiViets/Create
        public ActionResult Create()
        {
            ViewBag.LoaiBaiViet = new SelectList(db.LoaiBaiViets, "ID", "TenLoaiBaiViet");
            ViewBag.NguoiDang = new SelectList(db.NhanVienKhoas, "ID", "HoTen");
            return View();
        }

        // POST: QuanLy/BaiViets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BaiViet BaiViet, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        BaiViet.Title = ToUnsignString(BaiViet.TieuDe);
                        BaiViet.NgayDangBai = DateTime.Now;
                        db.BaiViets.Add(BaiViet);
                        db.SaveChanges();

                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + BaiViet.ID);

                        scope.Complete();
                        SetAlert("Bạn đã tạo thành công", "success");
                        return RedirectToAction("Index2");
                    }
                }
                else SetAlert("Lỗi hình ảnh, vui lòng sửa lại", "danger");
            }
            else SetAlert("Bạn đã tạo không thành công", "danger");
            ViewBag.LoaiBaiViet = new SelectList(db.LoaiBaiViets, "ID", "TenLoaiBaiViet", BaiViet.LoaiBaiViet);
            ViewBag.NguoiDang = new SelectList(db.NhanVienKhoas, "ID", "MaNhanVien", BaiViet.NguoiDang);
            return View(BaiViet);
        }

        // GET: QuanLy/BaiViets/Edit/5
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
            ViewBag.NguoiDang = new SelectList(db.NhanVienKhoas, "ID", "HoTen", baiViet.NguoiDang);
            return View(baiViet);
        }

        // POST: QuanLy/BaiViets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BaiViet baiViet, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    baiViet.Title = ToUnsignString(baiViet.TieuDe);
                    baiViet.NgayDangBai = DateTime.Now;
                    db.Entry(baiViet).State = EntityState.Modified;
                    db.SaveChanges();

                    if (picture != null)
                    {
                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + baiViet.ID);
                        SetAlert("Bạn đã chỉnh sửa thành công", "success");
                    }
                    else SetAlert("Lỗi hình ảnh, vui lòng sửa lại", "danger");
                    scope.Complete();
                    return RedirectToAction("Index2");              
                }
            }
            else SetAlert("Bạn chỉnh sửa không thành công", "danger");
            ViewBag.LoaiBaiViet = new SelectList(db.LoaiBaiViets, "ID", "TenLoaiBaiViet", baiViet.LoaiBaiViet);
            ViewBag.NguoiDang = new SelectList(db.NhanVienKhoas, "ID", "HoTen", baiViet.NguoiDang);
            return View(baiViet);
        }

        // GET: QuanLy/BaiViets/Delete/5
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

        // POST: QuanLy/BaiViets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var scope = new TransactionScope())
            {
                var model = db.BaiViets.Find(id);
                db.BaiViets.Remove(model);
                db.SaveChanges();
               
                var path = Server.MapPath(PICTURE_PATH);
                System.IO.File.Delete(path + model.ID);
               
                scope.Complete();
                SetAlert("Bạn đã xóa thành công", "success");
                return RedirectToAction("Index2");
            }
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
