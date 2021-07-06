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
    }
}