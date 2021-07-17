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
    public class BaiVietsController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();

        // GET: QuanLy/BaiViets
        public ActionResult Index()
        {
            var baiViets = db.BaiViets.Include(b => b.LoaiBaiViet1).Include(b => b.NhanVienKhoa);
            return View(baiViets.ToList());
        }
        public ActionResult Index2()
        {
            var baiViets = db.BaiViets.Include(b => b.LoaiBaiViet1).Include(b => b.NhanVienKhoa);
            return View(baiViets.ToList());
        }

        // GET: QuanLy/BaiViets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            return View(baiViet);
        }
        public ActionResult Picture(int id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "images");
        }
        private const string PICTURE_PATH = "~/images/BaiViet/";
        // GET: QuanLy/BaiViets/Create
        public ActionResult Create()
        {
            ViewBag.LoaiBaiViet = new SelectList(db.LoaiBaiViets, "ID", "TenLoaiBaiViet");
            ViewBag.NguoiDang = new SelectList(db.NhanVienKhoas, "ID", "MaNhanVien");
            return View();
        }

        // POST: QuanLy/BaiViets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BaiViet BaiViet)
        {
            if (ModelState.IsValid)
                    {
                        BaiViet.NoiDung.Substring(3,BaiViet.NoiDung.Length - 6);
                        db.BaiViets.Add(BaiViet);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                
            
            ViewBag.LoaiBaiViet = new SelectList(db.LoaiBaiViets, "ID", "TenLoaiBaiViet", BaiViet.LoaiBaiViet);
            ViewBag.NguoiDang = new SelectList(db.NhanVienKhoas, "ID", "MaNhanVien", BaiViet.NguoiDang);
            return View(BaiViet);
        }

        // GET: QuanLy/BaiViets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiBaiViet = new SelectList(db.LoaiBaiViets, "ID", "TenLoaiBaiViet", baiViet.LoaiBaiViet);
            ViewBag.NguoiDang = new SelectList(db.NhanVienKhoas, "ID", "MaNhanVien", baiViet.NguoiDang);
            return View(baiViet);
        }

        // POST: QuanLy/BaiViets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BaiViet baiViet)
        {
            if (ModelState.IsValid)
            {
                    db.Entry(baiViet).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
            }
            
            ViewBag.LoaiBaiViet = new SelectList(db.LoaiBaiViets, "ID", "TenLoaiBaiViet", baiViet.LoaiBaiViet);
            ViewBag.NguoiDang = new SelectList(db.NhanVienKhoas, "ID", "MaNhanVien", baiViet.NguoiDang);
            return View(baiViet);
        }

        // GET: QuanLy/BaiViets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            return View(baiViet);
        }

        // POST: QuanLy/BaiViets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
                BaiViet baiViet = db.BaiViets.Find(id);
                db.BaiViets.Remove(baiViet);
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
