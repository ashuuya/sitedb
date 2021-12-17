using sitedb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sitedb.Controllers
{
    public class FIRSTController : Controller
    {
        private LEGOEntities db = new LEGOEntities();
        public ActionResult Index()
        {

            ViewBag.prodID = new SelectList(db.Products, "ID", "ID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string prodID)
        {
            return Redirect("FIRST/Result?prodID=" + prodID);
        }

        public ActionResult Result(int prodID)
        {
            SqlParameter parameter = new SqlParameter("@prodID", prodID);
            var firstTask = db.Database.SqlQuery<FIRST_Result>("FIRST @prodID", parameter);
            return View(firstTask);
        }
    }
}