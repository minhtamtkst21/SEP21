using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SEP21.Models;

namespace SEP21.Controllers
{
    public class CanBoKhoaController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();

        // GET: NhanVienKhoas
        public ActionResult Index()
        {
            var nhanVienKhoas = db.NhanVienKhoas.Include(n => n.Khoa1);
            return View(nhanVienKhoas.ToList());
        }

        // GET: NhanVienKhoas/Details/5
        public ActionResult Details(int? id)
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
        public ActionResult Picture(int id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "images");
        }
        private const string PICTURE_PATH = "~/images/CanBoKhoa/";
        // GET: NhanVienKhoas/Create
        public ActionResult Create()
        {
            ViewBag.Khoa = new SelectList(db.Khoas, "ID", "MaKhoa");
            return View();
        }

        // POST: NhanVienKhoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MaNhanVien,HoTen,ChucVu,Khoa")] NhanVienKhoa nhanVienKhoa)
        {
            if (ModelState.IsValid)
            {
                db.NhanVienKhoas.Add(nhanVienKhoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Khoa = new SelectList(db.Khoas, "ID", "MaKhoa", nhanVienKhoa.Khoa);
            return View(nhanVienKhoa);
        }

        // GET: NhanVienKhoas/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.Khoa = new SelectList(db.Khoas, "ID", "MaKhoa", nhanVienKhoa.Khoa);
            return View(nhanVienKhoa);
        }

        // POST: NhanVienKhoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaNhanVien,HoTen,ChucVu,Khoa")] NhanVienKhoa nhanVienKhoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVienKhoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Khoa = new SelectList(db.Khoas, "ID", "MaKhoa", nhanVienKhoa.Khoa);
            return View(nhanVienKhoa);
        }

        // GET: NhanVienKhoas/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: NhanVienKhoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhanVienKhoa nhanVienKhoa = db.NhanVienKhoas.Find(id);
            db.NhanVienKhoas.Remove(nhanVienKhoa);
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
