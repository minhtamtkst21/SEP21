using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SEP21.Models;

namespace SEP21.Areas.QuanLy.Controllers
{
    public class DangKyHoatDongsController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();
        // GET: QuanLy/DangKyHoatDongs
        public ActionResult Index()
        {
            var list = db.DangKyHoatDongs.Include(d => d.BaiViet).Include(d => d.SinhVien);
            return View(list.ToList());
        }

        // GET: QuanLy/DangKyHoatDongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DangKyHoatDong dangKyHoatDong = db.DangKyHoatDongs.Find(id);
            if (dangKyHoatDong == null)
            {
                return HttpNotFound();
            }
            return View(dangKyHoatDong);
        }

        // GET: QuanLy/DangKyHoatDongs/Create
        public ActionResult Create()
        {
            ViewBag.HoatDong = new SelectList(db.BaiViets, "ID", "TenHoatDong");
            ViewBag.MSSV = new SelectList(db.SinhViens, "ID", "MSSV");
            return View();
        }

        // POST: QuanLy/DangKyHoatDongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HoatDong,MSSV,ThoiGianDangKy")] DangKyHoatDong dangKyHoatDong)
        {
            if (ModelState.IsValid)
            {
                db.DangKyHoatDongs.Add(dangKyHoatDong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HoatDong = new SelectList(db.BaiViets, "ID", "TenHoatDong", dangKyHoatDong.HoatDong);
            ViewBag.MSSV = new SelectList(db.SinhViens, "ID", "MSSV", dangKyHoatDong.MSSV);
            return View(dangKyHoatDong);
        }

        // GET: QuanLy/DangKyHoatDongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DangKyHoatDong dangKyHoatDong = db.DangKyHoatDongs.Find(id);
            if (dangKyHoatDong == null)
            {
                return HttpNotFound();
            }
            ViewBag.HoatDong = new SelectList(db.BaiViets, "ID", "TenHoatDong", dangKyHoatDong.HoatDong);
            ViewBag.MSSV = new SelectList(db.SinhViens, "ID", "MSSV", dangKyHoatDong.MSSV);
            return View(dangKyHoatDong);
        }

        // POST: QuanLy/DangKyHoatDongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DangKyHoatDong dangKyHoatDong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dangKyHoatDong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HoatDong = new SelectList(db.BaiViets, "ID", "TenHoatDong", dangKyHoatDong.HoatDong);
            ViewBag.MSSV = new SelectList(db.SinhViens, "ID", "MSSV", dangKyHoatDong.MSSV);
            return View(dangKyHoatDong);
        }

        // GET: QuanLy/DangKyHoatDongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DangKyHoatDong dangKyHoatDong = db.DangKyHoatDongs.Find(id);
            if (dangKyHoatDong == null)
            {
                return HttpNotFound();
            }
            return View(dangKyHoatDong);
        }

        // POST: QuanLy/DangKyHoatDongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DangKyHoatDong dangKyHoatDong = db.DangKyHoatDongs.Find(id);
            db.DangKyHoatDongs.Remove(dangKyHoatDong);
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
