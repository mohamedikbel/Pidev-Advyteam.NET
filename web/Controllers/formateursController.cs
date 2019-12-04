using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using data;
using domain.entities;
using data.Infrastructure;
using servicepattern;

namespace web.Controllers
{
    public class formateursController : Controller
    {
        private Context db = new Context();

        // GET: formateurs
        public ActionResult Index()
        {
            return View(db.formateur.ToList());
        }

        // GET: formateurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            formateur formateur = db.formateur.Find(id);
            if (formateur == null)
            {
                return HttpNotFound();
            }
            return View(formateur);
        }

        // GET: formateurs/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,img,nomPrenom,note,specialiste")] formateur formateur)
        {
            IDataBaseFactory Factory = new DataBaseFactory();
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<formateur> fService = new Service<formateur>(Uok);
            if (ModelState.IsValid)
            {
                fService.Add(formateur);
                fService.Commit();
                return RedirectToAction("Index");
            }

            return View(formateur);
        }

        // GET: formateurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            formateur formateur = db.formateur.Find(id);
            if (formateur == null)
            {
                return HttpNotFound();
            }
            return View(formateur);
        }

        // POST: formateurs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,img,nomPrenom,note,specialiste")] formateur formateur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formateur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formateur);
        }

        // GET: formateurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            formateur formateur = db.formateur.Find(id);
            if (formateur == null)
            {
                return HttpNotFound();
            }
            return View(formateur);
        }

        // POST: formateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            formateur formateur = db.formateur.Find(id);
            db.formateur.Remove(formateur);
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
