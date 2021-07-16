﻿using System;
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
    public class BaiVietsController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();

        // GET: BaiViets
        public ActionResult Index()
        {
            var baiViets = db.BaiViets.Include(b => b.LoaiBaiViet1).Include(b => b.NhanVienKhoa);
            return View(baiViets.ToList());
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
            return View(baiViet);
        }

        // GET: BaiViets/Create
        public ActionResult Create()
        {
            ViewBag.LoaiBaiViet = new SelectList(db.LoaiBaiViets, "ID", "TenLoaiBaiViet");
            ViewBag.NguoiDang = new SelectList(db.NhanVienKhoas, "ID", "MaNhanVien");
            return View();
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
            ViewBag.NguoiDang = new SelectList(db.NhanVienKhoas, "ID", "MaNhanVien", baiViet.NguoiDang);
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
            ViewBag.NguoiDang = new SelectList(db.NhanVienKhoas, "ID", "MaNhanVien", baiViet.NguoiDang);
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
            ViewBag.NguoiDang = new SelectList(db.NhanVienKhoas, "ID", "MaNhanVien", baiViet.NguoiDang);
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
