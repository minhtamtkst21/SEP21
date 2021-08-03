using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using SEP21.Models;

namespace SEP21.Areas.QuanLy.Controllers
{
    public class NhanVienKhoasController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();

        // GET: QuanLy/NhanVienKhoas
        public ActionResult Index()
        {
            if (Session["ID"] != null)
            {
                var nhanVienKhoas = db.NhanVienKhoas;
                return View(nhanVienKhoas.ToList());
            }
            return RedirectToAction("Login", "ManagerAdmin");
        }
        public ActionResult Picture(int id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "images");
        }
        private const string PICTURE_PATH = "~/images/CanBoKhoa/";
        // GET: QuanLy/NhanVienKhoas/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NhanVienKhoa nhanVienKhoa = db.NhanVienKhoas.Find(id);
                if (nhanVienKhoa == null)
                {
                    return HttpNotFound();
                }
                return View(nhanVienKhoa);
            }
            return RedirectToAction("Login", "ManagerAdmin");
        }

        // GET: QuanLy/NhanVienKhoas/Create
        public ActionResult Create()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "ManagerAdmin");
        }

        // POST: QuanLy/NhanVienKhoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NhanVienKhoa nhanVienKhoa, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        db.NhanVienKhoas.Add(nhanVienKhoa);
                        db.SaveChanges();
                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + nhanVienKhoa.ID);

                        scope.Complete();
                        SetAlert("Bạn đã tạo thành công", "success");

                        return RedirectToAction("Index");
                    }
                }
                else SetAlert("Lỗi hình ảnh, vui lòng sửa lại", "danger");
            }
            else SetAlert("Bạn đã tạo không thành công", "danger");
            return View(nhanVienKhoa);
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
        // GET: QuanLy/NhanVienKhoas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NhanVienKhoa nhanVienKhoa = db.NhanVienKhoas.Find(id);
                if (nhanVienKhoa == null)
                {
                    return HttpNotFound();
                }
                return View(nhanVienKhoa);
            }
            return RedirectToAction("Login", "ManagerAdmin");
        }

        // POST: QuanLy/NhanVienKhoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NhanVienKhoa nhanVienKhoa, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.Entry(nhanVienKhoa).State = EntityState.Modified;
                    db.SaveChanges();
                    if (picture != null)
                    {
                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + nhanVienKhoa.ID);
                        SetAlert("Bạn đã chỉnh sửa thành công", "success");
                    }
                    else SetAlert("Lỗi hình ảnh, vui lòng sửa lại", "danger");
                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }
            else SetAlert("Bạn chỉnh sửa không thành công", "danger");
            return View(nhanVienKhoa);
        }

        // GET: QuanLy/NhanVienKhoas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NhanVienKhoa nhanVienKhoa = db.NhanVienKhoas.Find(id);
                if (nhanVienKhoa == null)
                {
                    return HttpNotFound();
                }
                return View(nhanVienKhoa);
            }
            return RedirectToAction("Login", "ManagerAdmin");
        }

        // POST: QuanLy/NhanVienKhoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var scope = new TransactionScope())
            {
                NhanVienKhoa nhanVienKhoa = db.NhanVienKhoas.Find(id);
                db.NhanVienKhoas.Remove(nhanVienKhoa);
                db.SaveChanges();

                var path = Server.MapPath(PICTURE_PATH);
                System.IO.File.Delete(path + nhanVienKhoa.ID);

                scope.Complete();
                SetAlert("Bạn đã xóa thành công", "success");
                return RedirectToAction("Index");
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
