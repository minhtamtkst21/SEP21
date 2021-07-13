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
    public class NCKHsController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();

        // GET: QuanLy/NCKHs
        public ActionResult Index()
        {
            return View(db.NCKHs.ToList());
        }

        // GET: QuanLy/NCKHs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NCKH nCKH = db.NCKHs.Find(id);
            if (nCKH == null)
            {
                return HttpNotFound();
            }
            return View(nCKH);
        }
        public ActionResult Picture(int id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "images");
        }
        private const string PICTURE_PATH = "~/images/NCKH/";
        // GET: QuanLy/NCKHs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuanLy/NCKHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NCKH nCKH, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        db.NCKHs.Add(nCKH);
                        db.SaveChanges();

                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + nCKH.ID);

                        scope.Complete();
                        return RedirectToAction("Index");
                    }
                }
                else ModelState.AddModelError("", " Picture not found!");
            }
            return View(nCKH);
        }

        // GET: QuanLy/NCKHs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NCKH nCKH = db.NCKHs.Find(id);
            if (nCKH == null)
            {
                return HttpNotFound();
            }
            return View(nCKH);
        }

        // POST: QuanLy/NCKHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NCKH nCKH, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.Entry(nCKH).State = EntityState.Modified;
                    db.SaveChanges();

                    if (picture != null)
                    {
                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + nCKH.ID);
                    }

                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }
            return View(nCKH);
        }

        // GET: QuanLy/NCKHs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NCKH nCKH = db.NCKHs.Find(id);
            if (nCKH == null)
            {
                return HttpNotFound();
            }
            return View(nCKH);
        }

        // POST: QuanLy/NCKHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NCKH nCKH = db.NCKHs.Find(id);
            db.NCKHs.Remove(nCKH);
            db.SaveChanges();

            var path = Server.MapPath(PICTURE_PATH);

            System.IO.File.Delete(path + nCKH.ID);
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
