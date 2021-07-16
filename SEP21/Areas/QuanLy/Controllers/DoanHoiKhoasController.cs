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
    public class DoanHoiKhoasController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();

        // GET: QuanLy/DoanHoiKhoas
        public ActionResult Index()
        {
            var doanHoiKhoas = db.DoanHoiKhoas.Include(d => d.SinhVien);
            return View(doanHoiKhoas.ToList());
        }

        // GET: QuanLy/DoanHoiKhoas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoanHoiKhoa doanHoiKhoa = db.DoanHoiKhoas.Find(id);
            if (doanHoiKhoa == null)
            {
                return HttpNotFound();
            }
            return View(doanHoiKhoa);
        }
        public ActionResult Picture(int id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "images");
        }
        private const string PICTURE_PATH = "~/images/DoanHoiKhoa/";

        // GET: QuanLy/DoanHoiKhoas/Create
        public ActionResult Create()
        {
            ViewBag.MSSV = new SelectList(db.SinhViens, "ID", "MSSV");
            return View();
        }

        // POST: QuanLy/DoanHoiKhoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoanHoiKhoa doanHoiKhoa, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        db.DoanHoiKhoas.Add(doanHoiKhoa);
                        db.SaveChanges();

                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + doanHoiKhoa.ID);

                        scope.Complete();
                        return RedirectToAction("Index");
                    }
                }
                else ModelState.AddModelError("", " Picture not found!");
            }

            ViewBag.MSSV = new SelectList(db.SinhViens, "ID", "MSSV", doanHoiKhoa.MSSV);
            return View(doanHoiKhoa);
        }

        // GET: QuanLy/DoanHoiKhoas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoanHoiKhoa doanHoiKhoa = db.DoanHoiKhoas.Find(id);
            if (doanHoiKhoa == null)
            {
                return HttpNotFound();
            }
            ViewBag.MSSV = new SelectList(db.SinhViens, "ID", "MSSV", doanHoiKhoa.MSSV);
            return View(doanHoiKhoa);
        }

        // POST: QuanLy/DoanHoiKhoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DoanHoiKhoa doanHoiKhoa, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.Entry(doanHoiKhoa).State = EntityState.Modified;
                    db.SaveChanges();

                    if (picture != null)
                    {
                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + doanHoiKhoa.ID);
                    }

                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.MSSV = new SelectList(db.SinhViens, "ID", "MSSV", doanHoiKhoa.MSSV);
            return View(doanHoiKhoa);
        }

        // GET: QuanLy/DoanHoiKhoas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoanHoiKhoa doanHoiKhoa = db.DoanHoiKhoas.Find(id);
            if (doanHoiKhoa == null)
            {
                return HttpNotFound();
            }
            return View(doanHoiKhoa);
        }

        // POST: QuanLy/DoanHoiKhoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoanHoiKhoa doanHoiKhoa = db.DoanHoiKhoas.Find(id);
            db.DoanHoiKhoas.Remove(doanHoiKhoa);
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
