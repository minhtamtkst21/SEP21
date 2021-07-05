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
    public class TuyenDungController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();
        // GET: QuanLy/TuyenDung
        public ActionResult Index()
        {
            return View(db.TuyenDungs.ToList());
        }
    }
}