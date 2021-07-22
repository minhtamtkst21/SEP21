using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using SEP21.Models;

namespace SEP21.Areas.QuanLy.Controllers
{
    public class DangKyHoatDongsController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();
        // GET: QuanLy/DangKyHoatDongs
        public ActionResult Index()
        {
            var list = db.DangKyHoatDongs.Include(d => d.BaiViet).Include(d => d.SinhVien);
            return View(list.ToList());
        }
        public ActionResult Index2()
        {
            var list = db.DangKyHoatDongs.Include(d => d.BaiViet).Include(d => d.SinhVien);
            return View(list.ToList());
        }
        public ActionResult ExportExcel()
        {
            var list = db.DangKyHoatDongs.Include(d => d.BaiViet).Include(d => d.SinhVien);
            int stt = 1;
            ExcelPackage ep = new ExcelPackage();
            ExcelWorksheet Sheet = ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "STT";
            Sheet.Cells["B1"].Value = "Thời gian đăng ký";
            Sheet.Cells["C1"].Value = "Hoạt động";
            Sheet.Cells["D1"].Value = "Tên sinh viên";

            int row = 2;// dòng bắt đầu ghi dữ liệu
            foreach (var item in list)
            {
                Sheet.Cells[string.Format("A{0}",row)].Value = stt;
                stt++;
                Sheet.Cells[string.Format("B{0}",row)].Value = item.ThoiGianDangKy;
                Sheet.Cells[string.Format("C{0}",row)].Value = item.BaiViet.TieuDe;
                Sheet.Cells[string.Format("D{0}",row)].Value = item.SinhVien.HoTen;
                row++;
            }
            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=" + "Report.xlsx");
            Response.BinaryWrite(ep.GetAsByteArray());
            Response.End();
            return View("Index2");
        }

        // GET: QuanLy/DangKyHoatDongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DangKyHoatDong dangKyHoatDong = db.DangKyHoatDongs.Find(id);
            if (dangKyHoatDong == null)
            {
                return HttpNotFound();
            }
            return View(dangKyHoatDong);
        }

        // GET: QuanLy/DangKyHoatDongs/Create
        public ActionResult Create()
        {
            ViewBag.HoatDong = new SelectList(db.BaiViets, "ID", "TenHoatDong");
            ViewBag.MSSV = new SelectList(db.SinhViens, "ID", "MSSV");
            return View();
        }

        // POST: QuanLy/DangKyHoatDongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HoatDong,MSSV,ThoiGianDangKy")] DangKyHoatDong dangKyHoatDong)
        {
            if (ModelState.IsValid)
            {
                db.DangKyHoatDongs.Add(dangKyHoatDong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HoatDong = new SelectList(db.BaiViets, "ID", "TenHoatDong", dangKyHoatDong.HoatDong);
            ViewBag.MSSV = new SelectList(db.SinhViens, "ID", "MSSV", dangKyHoatDong.MSSV);
            return View(dangKyHoatDong);
        }

        // GET: QuanLy/DangKyHoatDongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DangKyHoatDong dangKyHoatDong = db.DangKyHoatDongs.Find(id);
            if (dangKyHoatDong == null)
            {
                return HttpNotFound();
            }
            ViewBag.HoatDong = new SelectList(db.BaiViets, "ID", "TenHoatDong", dangKyHoatDong.HoatDong);
            ViewBag.MSSV = new SelectList(db.SinhViens, "ID", "MSSV", dangKyHoatDong.MSSV);
            return View(dangKyHoatDong);
        }

        // POST: QuanLy/DangKyHoatDongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DangKyHoatDong dangKyHoatDong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dangKyHoatDong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HoatDong = new SelectList(db.BaiViets, "ID", "TenHoatDong", dangKyHoatDong.HoatDong);
            ViewBag.MSSV = new SelectList(db.SinhViens, "ID", "MSSV", dangKyHoatDong.MSSV);
            return View(dangKyHoatDong);
        }

        // GET: QuanLy/DangKyHoatDongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DangKyHoatDong dangKyHoatDong = db.DangKyHoatDongs.Find(id);
            if (dangKyHoatDong == null)
            {
                return HttpNotFound();
            }
            return View(dangKyHoatDong);
        }

        // POST: QuanLy/DangKyHoatDongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DangKyHoatDong dangKyHoatDong = db.DangKyHoatDongs.Find(id);
            db.DangKyHoatDongs.Remove(dangKyHoatDong);
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
