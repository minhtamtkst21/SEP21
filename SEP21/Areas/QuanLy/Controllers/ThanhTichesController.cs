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
    public class ThanhTichesController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();

        // GET: QuanLy/ThanhTiches
        public ActionResult Index()
        {
            return View(db.ThanhTiches.ToList());
        }

        // GET: QuanLy/ThanhTiches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhTich thanhTich = db.ThanhTiches.Find(id);
            if (thanhTich == null)
            {
                return HttpNotFound();
            }
            return View(thanhTich);
        }
        public ActionResult Picture(int id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "images");
        }
        private const string PICTURE_PATH = "~/images/ThanhTich/";

        // GET: QuanLy/ThanhTiches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuanLy/ThanhTiches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ThanhTich thanhTich, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        db.ThanhTiches.Add(thanhTich);
                        db.SaveChanges();

                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + thanhTich.ID);

                        scope.Complete();
                        return RedirectToAction("Index");
                    }
                }
                else ModelState.AddModelError("", " Picture not found!");
            }

            return View(thanhTich);
        }

        // GET: QuanLy/ThanhTiches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhTich thanhTich = db.ThanhTiches.Find(id);
            if (thanhTich == null)
            {
                return HttpNotFound();
            }
            return View(thanhTich);
        }

        // POST: QuanLy/ThanhTiches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ThanhTich thanhTich, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.Entry(thanhTich).State = EntityState.Modified;
                    db.SaveChanges();

                    if (picture != null)
                    {
                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + thanhTich.ID);
                    }

                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }
            return View(thanhTich);
        }

        // GET: QuanLy/ThanhTiches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhTich thanhTich = db.ThanhTiches.Find(id);
            if (thanhTich == null)
            {
                return HttpNotFound();
            }
            return View(thanhTich);
        }

        // POST: QuanLy/ThanhTiches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThanhTich thanhTich = db.ThanhTiches.Find(id);
            db.ThanhTiches.Remove(thanhTich);
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
