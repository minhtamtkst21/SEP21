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
    public class HomeController : Controller
    {
        private SEP24Team5Entities db = new SEP24Team5Entities();
        public ActionResult Index()
        {
            return View(db.BaiViets.ToList());
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}