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
    public class DaoTaosController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();

        // GET: QuanLy/DaoTaos
        public ActionResult Index()
        {
            return View(db.DaoTaos.ToList());
        }

        // GET: QuanLy/DaoTaos/Details/5
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

        // GET: QuanLy/DaoTaos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuanLy/DaoTaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NoiDung,TieuDe")] DaoTao daoTao)
        {
            if (ModelState.IsValid)
            {
                db.DaoTaos.Add(daoTao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(daoTao);
        }

        // GET: QuanLy/DaoTaos/Edit/5
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

        // POST: QuanLy/DaoTaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NoiDung,TieuDe")] DaoTao daoTao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(daoTao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(daoTao);
        }

        // GET: QuanLy/DaoTaos/Delete/5
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

        // POST: QuanLy/DaoTaos/Delete/5
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
