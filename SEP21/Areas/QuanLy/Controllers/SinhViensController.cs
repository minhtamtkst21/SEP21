using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LinqToExcel;
using OfficeOpenXml;
using SEP21.Models;

namespace SEP21.Areas.QuanLy.Controllers
{
    public class SinhViensController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();

        // GET: QuanLy/SinhViens1
        public ActionResult Index()
        {
            var sinhViens = db.SinhViens.Include(s => s.Khoa);
            return View(sinhViens.ToList());
        }
        public ActionResult ExportExcel()
        {
            var list = db.Khoas;
            ExcelPackage ep = new ExcelPackage();
            ExcelWorksheet Sheet = ep.Workbook.Worksheets.Add("Import");
            Sheet.Cells["A1"].Value = "MSSV";
            Sheet.Cells["B1"].Value = "họ tên";
            Sheet.Cells["C1"].Value = "Mã khoa";
            Sheet.Cells["D1"].Value = "Niên khóa";
            Sheet.Cells["E1"].Value = "Số điện thoại";
            Sheet.Cells["F1"].Value = "Mail";
            Sheet.Cells["C2"].Value = "1";
            Sheet.Cells["D2"].Value = "K24";
            Sheet.Cells["G2"].Value = "Xem mã khoa ở Sheet_Khoa";
            ExcelWorksheet Sheet2 = ep.Workbook.Worksheets.Add("Khoa");
            Sheet2.Cells["A1"].Value = "Tên Khoa";
            Sheet2.Cells["B1"].Value = "Mã Khoa";

            int row = 2;// dòng bắt đầu ghi dữ liệu
            foreach (var item in list)
            {
                Sheet2.Cells[string.Format("A{0}", row)].Value = item.TenKhoa;
                Sheet2.Cells[string.Format("B{0}", row)].Value = item.ID;
                row++;
            }

            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=" + "Import.xlsx");
            Response.BinaryWrite(ep.GetAsByteArray());
            Response.End();
            return View("Index2");
        }
        [HttpPost]
        public ActionResult Upload(FormCollection formCollection)
        {
            var usersList = new List<SinhVien>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var user = new SinhVien();
                            user.MSSV = (workSheet.Cells[rowIterator, 1].Value).ToString();
                            user.HoTen = (workSheet.Cells[rowIterator, 2].Value).ToString();
                            user.TenKhoa = int.Parse(workSheet.Cells[rowIterator, 3].Value.ToString());
                            user.NienKhoa = workSheet.Cells[rowIterator, 4].Value.ToString();
                            user.SoDienThoai = int.Parse(workSheet.Cells[rowIterator, 5].Value.ToString());
                            user.mail = (workSheet.Cells[rowIterator, 6].Value).ToString();
                            usersList.Add(user);
                        }
                    }
                }
            }
            foreach (var item in usersList)
            {
                db.SinhViens.Add(item);
                var sv = new Login();
                sv.username = item.mail.Substring(0, item.mail.Length - 14);
                sv.password = "VLU" + sv.username.Substring(sv.username.Length - 10, 10);
                db.Logins.Add(sv);
            }
            db.SaveChanges();
            SetAlert("Bạn đã tạo thành công", "success");
            return Redirect("Index");
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
        // GET: QuanLy/SinhViens1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhViens.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // GET: QuanLy/SinhViens1/Create
        public ActionResult Create()
        {
            ViewBag.TenKhoa = new SelectList(db.Khoas, "ID", "MaKhoa");
            return View();
        }

        // POST: QuanLy/SinhViens1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MSSV,HoTen,TenKhoa,NienKhoa,SoDienThoai,mail")] SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                db.SinhViens.Add(sinhVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TenKhoa = new SelectList(db.Khoas, "ID", "MaKhoa", sinhVien.TenKhoa);
            return View(sinhVien);
        }

        // GET: QuanLy/SinhViens1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhViens.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.TenKhoa = new SelectList(db.Khoas, "ID", "MaKhoa", sinhVien.TenKhoa);
            return View(sinhVien);
        }

        // POST: QuanLy/SinhViens1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinhVien).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Bạn đã chỉnh sửa thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                 SetAlert("Chỉnh sửa không thành công", "danger");
            }
            ViewBag.TenKhoa = new SelectList(db.Khoas, "ID", "MaKhoa", sinhVien.TenKhoa);
            return View(sinhVien);
        }

        // GET: QuanLy/SinhViens1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhViens.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // POST: QuanLy/SinhViens1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SinhVien sinhVien = db.SinhViens.Find(id);
            db.SinhViens.Remove(sinhVien);
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
