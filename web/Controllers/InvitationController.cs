using domain.entities;
using service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class InvitationController : Controller
    {   
        public InvitationServiice Is;
        public EmployeeService Es;
        public TacheService Ts;

        public InvitationController()
        {
            Is = new InvitationServiice();
            Es = new EmployeeService();
            Ts = new TacheService();
        }
        // GET: Invitation
        public ActionResult Index()
        {
            return View();
        }

        // GET: Invitation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Invitation/Create
        public ActionResult Create(int id)
        {
            invetation i = new invetation()
            {
                idFormation = id,
                vu = false,
                etat_Invitation = "En_Attente"

            };
            ViewBag.idEmploye = new SelectList(Es.GetAll().ToList(), "EM_Id", "prenom","Selctioner Employer");

            return View(i);
        }

        // POST: Invitation/Create
        [HttpPost]
        public ActionResult Create(invetation i )
        {
            if (ModelState.IsValid)
            {
                Is.Add(i);
                Is.Commit();
                TempData["SM"] = "Invitation envoyer Avec Success";
                return RedirectToRoute(new
                {
                    controller = "formations",
                    action = "Index"
                });
            }
            ViewBag.idEmploye = new SelectList(Es.GetAll().ToList(), "EM_Id", "prenom");
            return View(i);

        }

        // GET: Invitation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Invitation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invitation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Invitation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       
        public JsonResult chartA(int id)
        {

            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dt = new DataTable();
            dt.Columns.Add("Tache", System.Type.GetType("System.String"));
            dt.Columns.Add("Duree", System.Type.GetType("System.Int32"));
            IEnumerable<tache> lt = Ts.getByEmp(id);
            if(lt.Count() > 0)
            {
                foreach (tache t in lt )
                {
                    DataRow dr = dt.NewRow();
                    dr["Tache"] = t.nom;
                    dr["Duree"] = t.dureeReelle;
                    dt.Rows.Add(dr);
                }

                foreach (DataColumn dc in dt.Columns)
                {
                    List<object> x = new List<object>();
                    x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                    iData.Add(x);
                }
            }
           
           

           
           
            return Json(iData, JsonRequestBehavior.AllowGet);

        }

        public JsonResult chartB(int id)
        {

            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dt = new DataTable();
            dt.Columns.Add("Tache", System.Type.GetType("System.String"));
            dt.Columns.Add("Duree", System.Type.GetType("System.Int32"));
            IEnumerable<tache> lt = Ts.getByEmp(id);
            if (lt.Count() > 0)
            {
                foreach (tache t in lt)
                {
                    DataRow dr = dt.NewRow();
                    dr["Tache"] = t.nom;
                    dr["Duree"] = t.dureeEtimee;
                    dt.Rows.Add(dr);
                }

                foreach (DataColumn dc in dt.Columns)
                {
                    List<object> x = new List<object>();
                    x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                    iData.Add(x);
                }
            }





            return Json(iData, JsonRequestBehavior.AllowGet);

        }
    }
}
