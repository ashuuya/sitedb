using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace sitedb.Models
{
    public class Product_has_DetailController : Controller
    {
        private LEGOEntities db = new LEGOEntities();

        // GET: Product_has_Detail
        public ActionResult Index(int pg = 1)
        {
            var products_has_details = db.Products_has_Details.ToList();

            const int pageSize = 5;
            if (pg < 1)
                pg = 1;

            int recsCount = products_has_details.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = products_has_details.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }

        // GET: Product_has_Detail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products_has_Details products_has_Details = db.Products_has_Details.Find(id);
            if (products_has_Details == null)
            {
                return HttpNotFound();
            }
            return View(products_has_Details);
        }

        // GET: Product_has_Detail/Create
        public ActionResult Create()
        {
            ViewBag.Details_ID = new SelectList(db.Details, "ID", "Title");
            ViewBag.Products_ID = new SelectList(db.Products, "ID", "Title");
            return View();
        }

        // POST: Product_has_Detail/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Products_ID,Details_ID,Quantity")] Products_has_Details products_has_Details)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    //db.Products_has_Details.Add(products_has_Details);
                    db.addProductsDetails(products_has_Details.Products_ID, products_has_Details.Details_ID, products_has_Details.Quantity);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Details_ID = new SelectList(db.Details, "ID", "Title", products_has_Details.Details_ID);
                ViewBag.Products_ID = new SelectList(db.Products, "ID", "Title", products_has_Details.Products_ID);
                return View(products_has_Details);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.InnerException.Message;
                return View("Error");
            }
        }

        // GET: Product_has_Detail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products_has_Details products_has_Details = db.Products_has_Details.Find(id);
            if (products_has_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.Details_ID = new SelectList(db.Details, "ID", "Title", products_has_Details.Details_ID);
            ViewBag.Products_ID = new SelectList(db.Products, "ID", "Title", products_has_Details.Products_ID);
            return View(products_has_Details);
        }

        // POST: Product_has_Detail/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Products_ID,Details_ID,Quantity")] Products_has_Details products_has_Details)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //.Entry(products_has_Details).State = EntityState.Modified;
                    db.updProductsDetails(products_has_Details.Products_ID, products_has_Details.Details_ID, products_has_Details.Quantity);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Details_ID = new SelectList(db.Details, "ID", "Title", products_has_Details.Details_ID);
                ViewBag.Products_ID = new SelectList(db.Products, "ID", "Title", products_has_Details.Products_ID);
                return View(products_has_Details);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.InnerException.Message;
                return View("Error");
            }
            
        }

        // GET: Product_has_Detail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products_has_Details products_has_Details = db.Products_has_Details.Find(id);
            if (products_has_Details == null)
            {
                return HttpNotFound();
            }
            return View(products_has_Details);
        }

        // POST: Product_has_Detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Products_has_Details products_has_Details = db.Products_has_Details.Find(id);
                //db.Products_has_Details.Remove(products_has_Details);
                db.delProductsDetails(products_has_Details.Products_ID, products_has_Details.Details_ID);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.InnerException.Message;
                return View("Error");
            }
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
