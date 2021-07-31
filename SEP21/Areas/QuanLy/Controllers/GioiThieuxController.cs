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
    public class GioiThieuxController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();

        // GET: QuanLy/GioiThieux
        public ActionResult Index()
        {
            if (Session["ID"] != null)
            {
                return View(db.GioiThieux.ToList());
            }
            return RedirectToAction("Login", "ManagerAdmin");
        }

        // GET: QuanLy/GioiThieux/Details/5
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
        // GET: QuanLy/GioiThieux/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ID"] != null)
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
            return RedirectToAction("Login", "ManagerAdmin");
        }

        // POST: QuanLy/GioiThieux/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GioiThieu gioiThieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gioiThieu).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Bạn đã chỉnh sửa thành công", "success");
                return RedirectToAction("Index");
            }
            else SetAlert("Bạn chỉnh sửa không thành công", "danger");
            return View(gioiThieu);
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
