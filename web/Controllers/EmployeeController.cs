using domain;
using PidevFinal.data.Infrastructure;
using service;
using servicePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using web.Models;

namespace web.Controllers
{
    public class EmployeeController : Controller
    {

        IDatabaseFactory Factory = new DatabaseFactory();
        public EmployeeController()
        {
            var Cs = new Evaluation360Service();
        }
        // GET: Employee
        public ActionResult Index()
        {
            String idemp = (string)System.Web.HttpContext.Current.Session["id"].ToString();
            int x = int.Parse(idemp);
            if (x == 0)
                return RedirectToAction("Exist");

            return View();
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
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

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
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
        [HttpGet]
        public ActionResult Dashboard()
        {
            IDatabaseFactory Factory = new DatabaseFactory();
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<employe> chService = new Service<employe>(Uok);
            List<employe> list = chService.GetAll().ToList();
            List<String> repartions = new List<String>();
            
            var nbrvue = list.Select(x => x.rating);
            var Names = list.Select(x => x.nom);


                var rep = repartions;
                ViewBag.NBRVUE = nbrvue;
                ViewBag.REP = Names;

                return View(); 
            //List<employe> appo = new List<employe>();
            //IUnitOfWork Uok = new UnitOfWork(Factory);
            //IService<employe> Cs = new Service<employe>(Uok);

            //var list = Cs.GetAll();
            //List<int> repartitions = new List<int>();
            //var typeC = list.Select(x => x.rating).Distinct();

            //foreach (var item in typeC)
            //{
            //    repartitions.Add(list.Count(x => x.rating == item));

            //}
            //var rep = repartitions;
            //ViewBag.TYPEC = typeC;
            //ViewBag.REP = repartitions.ToList();

            //return View();
        }
        public ActionResult stat()
        {
            //IDatabaseFactory Factory = new DatabaseFactory();
            //IUnitOfWork Uok = new UnitOfWork(Factory);
            //IService<employe> chService = new Service<employe>(Uok);
            //List<employe> list = chService.GetAll().ToList();
            //List<String> repartions = new List<String>();
            //var role = (list.Select(x => x.role));
            //// if(role.Equals("Employe"))
            ////{
            //var nbrvue = list.Select(x => x.rating).Distinct();
            //var Names = list.Select(x => x.nom).Distinct();


            //var rep = repartions;
            //ViewBag.NBRVUE = nbrvue;
            //ViewBag.REP = Names;

            //var t = Cs.GetClaimsbytype(TypeClaims.Branche_technique).Count();
            //var f = Cs.GetClaimsbytype(TypeClaims.Branche_financière).Count();
            //var rr = Cs.GetClaimsbytype(TypeClaims.Branche_relationnelle).Count();


            //ViewBag.t = t;
            //ViewBag.f = f;
            //ViewBag.rr = rr;

            //return View(Cs.GetAll().ToList());
 return View();

        }
        public ActionResult loginout()
        {
            Session.Abandon();
            return RedirectToAction("login");
        }

        [HttpGet]
        public ActionResult login()
        {
        //    employeModels persoonModel = new employeModels();
           return View();
        }

        
        [HttpPost]
        public ActionResult login(employe model)
        {

            HttpClient Client = new HttpClient();
       ////     Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:9080/projetintegre-web/rest/employee/getEmplById/" + model.email + "/" + model.password).Result;
            if (response.IsSuccessStatusCode)
            {
                employe a = response.Content.ReadAsAsync<employe>().Result;

                if (a == null)
                    return RedirectToAction("Exist", "Employee");

                else
                {
                    Session["id"] = a.id;
                    Session["nom"] = a.nom;

                    Session["email"] = a.email;
                    Session["password"] = a.password;
                    if (a.role.Equals("Manager"))
                        return RedirectToAction("Index", "Critere");
                    else
                        return RedirectToAction("Index", "Avis");

                }
            }
           



            return View();

        }
        //[HttpGet]
        //public  employe employeId(employe model)
        //{

        //    HttpClient Client = new HttpClient();
        //    ////     Client.BaseAddress = new Uri("http://localhost:9080");
        //    Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //    HttpResponseMessage response = Client.GetAsync("http://localhost:9080/projetintegre-web/rest/employee/getEmplById/" + model.email + "/" + model.password).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        employe a = response.Content.ReadAsAsync<employe>().Result;
        //        Session["email"] = a.email;
        //        Session["password"] = a.password;
        //        if (a.role.Equals("Manager"))
        //            return RedirectToAction("Index", "Critere");
        //        else
        //            return RedirectToAction("Index", "Commentaire");

        //    }
        //    else
        //    {
        //        return RedirectToAction("Exist");
        //    }

        //}


    }
}
