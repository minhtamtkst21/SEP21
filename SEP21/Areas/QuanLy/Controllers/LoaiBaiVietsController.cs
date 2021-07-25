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
    public class LoaiBaiVietsController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();

        // GET: QuanLy/LoaiBaiViets
        public ActionResult Index()
        {
            return View(db.LoaiBaiViets.ToList());
        }

        // GET: QuanLy/LoaiBaiViets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiBaiViet loaiBaiViet = db.LoaiBaiViets.Find(id);
            if (loaiBaiViet == null)
            {
                return HttpNotFound();
            }
            return View(loaiBaiViet);
        }

        // GET: QuanLy/LoaiBaiViets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuanLy/LoaiBaiViets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoaiBaiViet loaiBaiViet)
        {
            if (ModelState.IsValid)
            {
                db.LoaiBaiViets.Add(loaiBaiViet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiBaiViet);
        }

        // GET: QuanLy/LoaiBaiViets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiBaiViet loaiBaiViet = db.LoaiBaiViets.Find(id);
            if (loaiBaiViet == null)
            {
                return HttpNotFound();
            }
            return View(loaiBaiViet);
        }

        // POST: QuanLy/LoaiBaiViets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenLoaiBaiViet")] LoaiBaiViet loaiBaiViet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiBaiViet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiBaiViet);
        }

        // GET: QuanLy/LoaiBaiViets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiBaiViet loaiBaiViet = db.LoaiBaiViets.Find(id);
            if (loaiBaiViet == null)
            {
                return HttpNotFound();
            }
            return View(loaiBaiViet);
        }

        // POST: QuanLy/LoaiBaiViets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiBaiViet loaiBaiViet = db.LoaiBaiViets.Find(id);
            db.LoaiBaiViets.Remove(loaiBaiViet);
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
