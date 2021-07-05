using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEP21.Models;
using System.Data.SqlClient;

namespace SEP21.Areas.QuanLy.Controllers
{
    public class AdminAccountsController : Controller
    {
        SqlConnection connect = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlDataReader dr;
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        void connectionString()
        {
            connect.ConnectionString = "data sourch=tuleap.vanlanguni.edu.vn,18082; database=SEP24Team5; integrated security = SSPI;";
        }
        public ActionResult Verify(AdminAccount account)
        {
            connectionString();
            connect.Open();
            command.Connection = connect;
            command.CommandText = "select * from LoginAdmin where username='"+account.Username+"' and password='"+account.Password+"'";
            dr = command.ExecuteReader();
            if (dr.Read())
            {
                connect.Close();
                return View("index");
            }
            else
            {
                connect.Close();
                return View("index 1");
            }
        }
    }
}