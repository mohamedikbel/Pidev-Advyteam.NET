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
using web.Models;
using data.Infrastructure;
using System.IO;
using servicepattern;

namespace web.Controllers
{
    public class formateursController : Controller
    {
        public FormateurService formateurS;
        IEmployeService emp = null;
        IEmployeeService emps = null;
        public Context ctx = new Context();
        public formateursController()
        {
            formateurS = new FormateurService();
            emp = new EmployeService();
            emps = new EmployeeService();
        }
      
        // GET: formateurs
        public ActionResult Index(String searchString)
        {
          List<formateur> Form = new List<formateur>();

           
           Form= formateurS.MaxNote();
            List<formateur> Forms = new List<formateur>();
           
            if (!String.IsNullOrEmpty(searchString))
                {
                    for (int i = Form.Count - 1; i >= 0; i--)
                    {

                        if (Form[i].nomPrenom.Contains(searchString))
                        {
                            Forms.Add(Form[i]);
                        }
                  
                    }
                }else
            {

                Forms = Form;

            }
           


            return View(Forms);
        }

       

        
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( formateur f, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                formateur q = new formateur();
                q.id = f.id;
                q.img = file.FileName;
                q.nomPrenom = f.nomPrenom;
                q.note = f.note;
                q.specialiste = f.specialiste;
                formateurS.Add(q);
                formateurS.Commit();
                var fileName = "";
                if (file.ContentLength > 0)
                {
                    fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Uploads/"), fileName);
                    file.SaveAs(path);
                }
                TempData["SM"] = "Ajouter Avec Success";
                return RedirectToAction("Index");
            }

            return View();
        }

       
        public ActionResult Edit( int id)
        {
            
            formateur formateusr = formateurS.GetById(id);
           
            if (formateusr == null)
            {
                return HttpNotFound();
            }
            return View(formateusr);
        }

        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( formateur formateur)
        {
            
            if (!ModelState.IsValid)
            {
                RedirectToAction("Edit");
            }
           

            ctx.Entry(formateur).State = EntityState.Modified;
            ctx.SaveChanges();
            TempData["SM"] = "Modifer Avec Success";
            return RedirectToAction("Index");
        }

        
        public ActionResult Delete(int id)
        {
           
            formateur formateur = formateurS.GetById(id);
            if (formateur == null)
            {
                return HttpNotFound();
            }
           
            formateurS.Delete(formateur);
            formateurS.Commit();
            TempData["SM"] = "Supprimer Avec Success";
            return RedirectToAction("Index");
        }

        


        public ActionResult Dashboard()
        {   
            IDataBaseFactory Factory = new DataBaseFactory();
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<tache> chService = new Service<tache>(Uok);
            IService<employe> hService = new Service<employe>(Uok);
            List<employe> em= hService.GetAll().ToList();
            List<tache> list = chService.GetAll().ToList();
            
            List<String> repartions = new List<String>();
            var nbrvue = emp.MaxEmp().Select(x => x.dureeReelle - x.dureeEtimee).Distinct().Take(10);
            var nbrvu = emp.MaxEmp().Select(x => x.employe_EM_Id).Take(10);
            List<String> noms = new List<String>();
            foreach(int id in nbrvu)

            {
                employe e = emps.GetById(id);
                noms.Add(e.prenom);

            }




            





            var rep = repartions;
           
           


            
            ViewBag.NBRVUE = nbrvue;
            ViewBag.REP = noms;

            return View();
        }






    }
}
