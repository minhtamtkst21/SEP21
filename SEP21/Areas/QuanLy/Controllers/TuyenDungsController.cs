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
    public class TuyenDungsController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();

        // GET: QuanLy/TuyenDungs
        public ActionResult Index()
        {
            if (Session["ID"] != null)
            {
                var tuyenDungs = db.TuyenDungs;
                return View(tuyenDungs.ToList());
            }
            return RedirectToAction("Login", "ManagerAdmin");
        }

        // GET: QuanLy/TuyenDungs/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TuyenDung tuyenDung = db.TuyenDungs.Find(id);
                if (tuyenDung == null)
                {
                    return HttpNotFound();
                }
                return View(tuyenDung);
            }
            return RedirectToAction("Login", "ManagerAdmin");
        }
        public void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }

        // GET: QuanLy/TuyenDungs/Create
        public ActionResult Create()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "ManagerAdmin");
        }
        // POST: QuanLy/TuyenDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TuyenDung tuyenDung)
        {
            if (ModelState.IsValid)
            {
                db.TuyenDungs.Add(tuyenDung);
                db.SaveChanges();
                SetAlert("Bạn đã tạo thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Bạn đã tạo không thành công", "success");
            }
            return View(tuyenDung);
        }
        // GET: QuanLy/TuyenDungs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TuyenDung tuyenDung = db.TuyenDungs.Find(id);
                if (tuyenDung == null)
                {
                    return HttpNotFound();
                }
                return View(tuyenDung);
            }
            return RedirectToAction("Login", "ManagerAdmin");
        }

        // POST: QuanLy/TuyenDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TuyenDung tuyenDung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tuyenDung).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Bạn đã chỉnh sửa thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Bạn đã chỉnh sửa không thành công", "success");
            }
            return View(tuyenDung);
        }

        // GET: QuanLy/TuyenDungs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TuyenDung tuyenDung = db.TuyenDungs.Find(id);
                if (tuyenDung == null)
                {
                    return HttpNotFound();
                }
                return View(tuyenDung);
            }
            return RedirectToAction("Login", "ManagerAdmin");
        }

        // POST: QuanLy/TuyenDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TuyenDung tuyenDung = db.TuyenDungs.Find(id);
            db.TuyenDungs.Remove(tuyenDung);
            db.SaveChanges();
            SetAlert("Bạn đã xóa thành công", "success");
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
