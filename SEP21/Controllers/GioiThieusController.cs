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
    public class GioiThieusController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();

        // GET: GioiThieus
        public ActionResult Index()
        {
            return View(db.GioiThieux.ToList());
        }
        public ActionResult Index2()
        {
            return View(db.GioiThieux.ToList());
        }

        // GET: GioiThieus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioiThieu gioiThieu = db.GioiThieux.Find(id);
            if (gioiThieu == null)
            {
                return HttpNotFound();
            }
            return View(gioiThieu);
        }

        // GET: GioiThieus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GioiThieus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SuMang,TamNhin,TrietLyGiaoDuc,ID")] GioiThieu gioiThieu)
        {
            if (ModelState.IsValid)
            {
                db.GioiThieux.Add(gioiThieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gioiThieu);
        }

        // GET: GioiThieus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioiThieu gioiThieu = db.GioiThieux.Find(id);
            if (gioiThieu == null)
            {
                return HttpNotFound();
            }
            return View(gioiThieu);
        }

        // POST: GioiThieus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SuMang,TamNhin,TrietLyGiaoDuc,ID")] GioiThieu gioiThieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gioiThieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gioiThieu);
        }

        // GET: GioiThieus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioiThieu gioiThieu = db.GioiThieux.Find(id);
            if (gioiThieu == null)
            {
                return HttpNotFound();
            }
            return View(gioiThieu);
        }

        // POST: GioiThieus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GioiThieu gioiThieu = db.GioiThieux.Find(id);
            db.GioiThieux.Remove(gioiThieu);
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
