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
        public IInvetationService Is;
        public Context ctx = new Context();
        public formationsController()
        {
            db = new FormationService();
            fs = new FormateurService();
            ss = new SkillsService();
            Is = new InvitationServiice();
        }
        
        public ActionResult Index()
        {
            
            return View(ctx.formation.ToList());
        }

       

        
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
                    /*  db.Update(formation);
                      db.Commit();*/
                    ctx.Entry(formation).State = EntityState.Modified;
                    ctx.SaveChanges();

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
           
          
               
            return View(formation);
        }

       
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
            foreach(invetation i in Is.GetAll().ToList<invetation>())
            {
                if (i.idFormation == formation.id)
                    Is.Delete(i);


            }
            Is.Commit();
            db.Delete(formation);
            db.Commit();
            TempData["SM"] = "Supprimer Avec Success";
            return RedirectToAction("Index");
        }

        

       
    }
}
