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
using service;

namespace web.Controllers
{
    public class formationsController : Controller
    {   
        public FormationService db;

        public FormateurService fs;
        public SkillsService ss;

        public formationsController()
        {
            db = new FormationService();
            fs = new FormateurService();
            ss = new SkillsService();
        }
        // GET: formations
        public ActionResult Index()
        {
           
            return View(db.GetAll().ToList());
        }

       

        // GET: formations/Create
        public ActionResult Create()
        {
            ViewBag.v = "";
            ViewBag.formateur_id = new SelectList(fs.GetAll().ToList(), "id", "nomPrenom");
            List<SelectListItem> lst = new List<SelectListItem>() {
                new SelectListItem { Text = "Training", Value="Training" },
                new SelectListItem { Text = "Conference", Value="Conference" },
            };
            ViewBag.lst = lst;
           
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,date_debut,date_fin,description,img,titre,type,formateur_id")] formation formation)
        {
           

            if (ModelState.IsValid   )
            {
                int i = DateTime.Compare(formation.date_debut.Value, formation.date_fin.Value);
                if(i <= 0)
                {
                    formation.skills.Add(ss.GetById(1));
                    
                    
                    db.Add(formation);
                    db.Commit();
                    TempData["SM"] = "Ajouter Avec Success";
                    return RedirectToAction("Index");
                }
                else
                    TempData["er"] = "Date Fin Doit etre Superieur ou egale a Date Debut";

            }
            List<SelectListItem> lst = new List<SelectListItem>() {
                new SelectListItem { Text = "Training", Value="Training" },
                new SelectListItem { Text = "Conference", Value="Conference" },
            };
            ViewBag.lst = lst;
            ViewBag.formateur_id = new SelectList(fs.GetAll().ToList(), "id", "nomPrenom", formation.formateur_id);
         
                
            
            return View(formation);
        }

        // GET: formations/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            formation formation = db.GetById(id);
            if (formation == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> lst = new List<SelectListItem>() {
                new SelectListItem { Text = "Training", Value="Training" },
                new SelectListItem { Text = "Conference", Value="Conference" },
            };
            ViewBag.lst = lst;
            ViewBag.formateur_id = new SelectList(fs.GetAll().ToList(), "id", "nomPrenom", formation.formateur_id);
            ViewBag.db = formation.date_debut.ToString();
            ViewBag.df = formation.date_fin.ToString();
            return View(formation);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,date_debut,date_fin,description,img,titre,type,formateur_id")] formation formation)
        {
            
            if (ModelState.IsValid )
            {
                int i = DateTime.Compare(formation.date_debut.Value, formation.date_fin.Value);
                if(i <= 0)
                {
                    db.Update(formation);
                    db.Commit();
                    TempData["SM"] = "Modifier Avec Success";
                    return RedirectToAction("Index");
                }
                else
                    TempData["er"] = "Date Fin Doit etre Superieur ou egale a Date Debut";

            }
            List<SelectListItem> lst = new List<SelectListItem>() {
                new SelectListItem { Text = "Training", Value="Training" },
                new SelectListItem { Text = "Conference", Value="Conference" },
            };
            ViewBag.lst = lst;
            ViewBag.formateur_id = new SelectList(fs.GetAll().ToList(), "id", "nomPrenom", formation.formateur_id);
            ViewBag.db = formation.date_debut.ToString();
            ViewBag.df = formation.date_fin.ToString();
          
               
            return View(formation);
        }

        // GET: formations/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            formation formation = db.GetById(id);
            if (formation == null)
            {
                return HttpNotFound();
            }
            db.Delete(formation);
            db.Commit();
            TempData["SM"] = "Supprimer Avec Success";
            return RedirectToAction("Index");
        }

        

       
    }
}
