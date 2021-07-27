﻿using System;
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
    public class LoginAdminsController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();

        // GET: QuanLy/LoginAdmins
        public ActionResult Index()
        {
            return View(db.LoginAdmins.ToList());
        }

        // GET: QuanLy/LoginAdmins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginAdmin loginAdmin = db.LoginAdmins.Find(id);
            if (loginAdmin == null)
            {
                return HttpNotFound();
            }
            return View(loginAdmin);
        }

        // GET: QuanLy/LoginAdmins/Create
        public ActionResult Create()
        {
            return View();
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

        // POST: QuanLy/LoginAdmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoginAdmin loginAdmin)
        {
            if (loginAdmin.username !=null && loginAdmin.password !=null)
            {
                
                db.LoginAdmins.Add(loginAdmin);
                db.SaveChanges();
                SetAlert("Bạn đã tạo thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Bạn đã tạo không thành công", "success");
            }

            return View(loginAdmin);
        }

        // GET: QuanLy/LoginAdmins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginAdmin loginAdmin = db.LoginAdmins.Find(id);
            if (loginAdmin == null)
            {
                return HttpNotFound();
            }
            return View(loginAdmin);
        }

        // POST: QuanLy/LoginAdmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LoginAdmin loginAdmin)
        {
            if (loginAdmin.username != null && loginAdmin.password != null)
            {
                db.Entry(loginAdmin).State = EntityState.Modified;
                SetAlert("Bạn đã chỉnh sửa thành công", "success");
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Bạn đã chỉnh sửa không thành công", "success");
            }
            return View(loginAdmin);
        }

        // GET: QuanLy/LoginAdmins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginAdmin loginAdmin = db.LoginAdmins.Find(id);
            if (loginAdmin == null)
            {
                return HttpNotFound();
            }
            return View(loginAdmin);
        }

        // POST: QuanLy/LoginAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoginAdmin loginAdmin = db.LoginAdmins.Find(id);
            db.LoginAdmins.Remove(loginAdmin);
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
