using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebRadioApp.Models;

namespace WebRadioApp.Controllers
{
    public class radiostationsController : Controller
    {
        private WebRadiosEntities db = new WebRadiosEntities();

        // GET: radiostations
        public ActionResult Index()
        {
            var radiostations = db.radiostations.Include(r => r.category).Include(r => r.country);
            return View(radiostations.ToList());
        }

        // GET: radiostations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            radiostations radiostations = db.radiostations.Find(id);
            if (radiostations == null)
            {
                return HttpNotFound();
            }
            return View(radiostations);
        }

        // GET: radiostations/Create
        public ActionResult Create()
        {
            ViewBag.categoryId = new SelectList(db.category, "id", "name");
            ViewBag.countryId = new SelectList(db.country, "id", "name");
            return View();
        }

        // POST: radiostations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,url,categoryId,countryId")] radiostations radiostations)
        {
            if (ModelState.IsValid)
            {
                radiostations.modified_date = DateTime.Now;
                radiostations.modifiedby = Environment.UserName;
                db.radiostations.Add(radiostations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoryId = new SelectList(db.category, "id", "name", radiostations.categoryId);
            ViewBag.countryId = new SelectList(db.country, "id", "name", radiostations.countryId);
            return View(radiostations);
        }

        // GET: radiostations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            radiostations radiostations = db.radiostations.Find(id);
            if (radiostations == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryId = new SelectList(db.category, "id", "name", radiostations.categoryId);
            ViewBag.countryId = new SelectList(db.country, "id", "name", radiostations.countryId);
            return View(radiostations);
        }

        // POST: radiostations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,url,categoryId,countryId")] radiostations radiostations)
        {
            if (ModelState.IsValid)
            {
                radiostations.modified_date = DateTime.Now;
                radiostations.modifiedby = Environment.UserName;
                db.Entry(radiostations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryId = new SelectList(db.category, "id", "name", radiostations.categoryId);
            ViewBag.countryId = new SelectList(db.country, "id", "name", radiostations.countryId);
            return View(radiostations);
        }

        // GET: radiostations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            radiostations radiostations = db.radiostations.Find(id);
            if (radiostations == null)
            {
                return HttpNotFound();
            }
            return View(radiostations);
        }

        // POST: radiostations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            radiostations radiostations = db.radiostations.Find(id);
            db.radiostations.Remove(radiostations);
            db.SaveChanges();
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
