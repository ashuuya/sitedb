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
    public class SECONDController : Controller
    {
        private LEGOEntities db = new LEGOEntities();
        public ActionResult Index()
        {

            ViewBag.matTitle = new SelectList(db.Materials, "Title", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string matTitle)
        {
            return Redirect("SECOND/Result?matTitle=" + matTitle);
        }

        public ActionResult Result(string matTitle)
        {
            SqlParameter parameter = new SqlParameter("@matTitle", matTitle);
            var secondTask = db.Database.SqlQuery<SECOND_Result>("SECOND @matTitle", parameter);
            return View(secondTask);
        }
    }
}