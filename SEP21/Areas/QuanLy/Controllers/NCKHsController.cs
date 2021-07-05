using System;
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
        public ActionResult Create([Bind(Include = "ID,Ten,NoiDung")] NCKH nCKH)
        {
            if (ModelState.IsValid)
            {
                db.NCKHs.Add(nCKH);
                db.SaveChanges();
                return RedirectToAction("Index");
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Ten,NoiDung")] NCKH nCKH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nCKH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
