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
    public class Detail_has_MaterialController : Controller
    {
        private LEGOEntities db = new LEGOEntities();

        // GET: Detail_has_Material
        public ActionResult Index(int pg = 1)
        {
            var details_has_materials = db.Details_has_Materials.ToList();

            const int pageSize = 5;
            if (pg < 1)
                pg = 1;

            int recsCount = details_has_materials.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = details_has_materials.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }

        // GET: Detail_has_Material/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Details_has_Materials details_has_Materials = db.Details_has_Materials.Find(id);
            if (details_has_Materials == null)
            {
                return HttpNotFound();
            }
            return View(details_has_Materials);
        }

        // GET: Detail_has_Material/Create
        public ActionResult Create()
        {
            ViewBag.Details_ID = new SelectList(db.Details, "ID", "Title");
            ViewBag.Materials_ID = new SelectList(db.Materials, "ID", "Title");
            return View();
        }

        // POST: Detail_has_Material/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Details_ID,Materials_ID,Quantity")] Details_has_Materials details_has_Materials)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //db.Details_has_Materials.Add(details_has_Materials);
                    db.addDetailsMaterials(details_has_Materials.Details_ID, details_has_Materials.Materials_ID, details_has_Materials.Quantity);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Details_ID = new SelectList(db.Details, "ID", "Title", details_has_Materials.Details_ID);
                ViewBag.Materials_ID = new SelectList(db.Materials, "ID", "Title", details_has_Materials.Materials_ID);
                return View(details_has_Materials);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.InnerException.Message;
                return View("Error");
            }
        }

        // GET: Detail_has_Material/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Details_has_Materials details_has_Materials = db.Details_has_Materials.Find(id);
            if (details_has_Materials == null)
            {
                return HttpNotFound();
            }
            ViewBag.Details_ID = new SelectList(db.Details, "ID", "Title", details_has_Materials.Details_ID);
            ViewBag.Materials_ID = new SelectList(db.Materials, "ID", "Title", details_has_Materials.Materials_ID);
            return View(details_has_Materials);
        }

        // POST: Detail_has_Material/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Details_ID,Materials_ID,Quantity")] Details_has_Materials details_has_Materials)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //db.Entry(details_has_Materials).State = EntityState.Modified;
                    db.updDetailsMaterials(details_has_Materials.Details_ID, details_has_Materials.Materials_ID, details_has_Materials.Quantity);
                    
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Details_ID = new SelectList(db.Details, "ID", "Title", details_has_Materials.Details_ID);
                ViewBag.Materials_ID = new SelectList(db.Materials, "ID", "Title", details_has_Materials.Materials_ID);
                return View(details_has_Materials);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.InnerException.Message;
                return View("Error");
            }
            
        }

        // GET: Detail_has_Material/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Details_has_Materials details_has_Materials = db.Details_has_Materials.Find(id);
            if (details_has_Materials == null)
            {
                return HttpNotFound();
            }
            return View(details_has_Materials);
        }

        // POST: Detail_has_Material/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Details_has_Materials details_has_Materials = db.Details_has_Materials.Find(id);
                //db.Details_has_Materials.Remove(details_has_Materials);
                db.delDetailsMaterials(details_has_Materials.Details_ID, details_has_Materials.Materials_ID);
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
