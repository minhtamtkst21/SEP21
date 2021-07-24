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
    public class DaoTaoController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();

        // GET: DaoTaos
        public ActionResult Index()
        {
            return View(db.DaoTaos.ToList());
        }

        // GET: DaoTaos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaoTao daoTao = db.DaoTaos.Find(id);
            if (daoTao == null)
            {
                return HttpNotFound();
            }
            return View(daoTao);
        }

        // GET: DaoTaos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DaoTaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TieuDe,NoiDung")] DaoTao daoTao)
        {
            if (ModelState.IsValid)
            {
                db.DaoTaos.Add(daoTao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(daoTao);
        }

        // GET: DaoTaos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaoTao daoTao = db.DaoTaos.Find(id);
            if (daoTao == null)
            {
                return HttpNotFound();
            }
            return View(daoTao);
        }

        // POST: DaoTaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TieuDe,NoiDung")] DaoTao daoTao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(daoTao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(daoTao);
        }

        // GET: DaoTaos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaoTao daoTao = db.DaoTaos.Find(id);
            if (daoTao == null)
            {
                return HttpNotFound();
            }
            return View(daoTao);
        }

        // POST: DaoTaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DaoTao daoTao = db.DaoTaos.Find(id);
            db.DaoTaos.Remove(daoTao);
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
