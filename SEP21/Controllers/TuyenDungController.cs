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
    public class TuyenDungController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();
        // GET: TuyenDung
        public ActionResult Index()
        {
            var Tuyendung1 = db.TuyenDungs.Include(a => a.LoaiTuyenDung);
            return View(Tuyendung1.ToList());
        }
    }
}