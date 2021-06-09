using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEP21.Areas.QuanLy.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: QuanLy/AdminHome
        public ActionResult Index()
        {
            return View();
        }

        // GET: QuanLy/AdminHome/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuanLy/AdminHome/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuanLy/AdminHome/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QuanLy/AdminHome/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuanLy/AdminHome/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QuanLy/AdminHome/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuanLy/AdminHome/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
