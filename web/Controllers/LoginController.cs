using domain.entities;
using Microsoft.ApplicationInsights.Extensibility;
using Newtonsoft.Json;
using System;
using service;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Data.Entity;
using MyFinance.Data.Infrastructure;
using Service.Pattern;

namespace web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        IDataBaseFactory dbf;
        IUnitOfWork uow;
        IService<employe> service;
        public LoginController()
        {
            dbf = new DataBaseFactory();
            uow = new UnitOfWork(dbf);
            service = new Service<employe>(uow);

        }
        public ActionResult Index()
        {
           

            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
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

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
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

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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

        [HttpPost]
        public ActionResult login(string mail, string pass)
        {



            string exist = string.Empty;

            FormUrlEncodedContent dataForm = new FormUrlEncodedContent(new[] {
            new KeyValuePair<string,string>("mail",mail),
            new KeyValuePair<string, string>("pass",pass)
        });

          

            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            HttpResponseMessage response =  Client.PostAsync("pidev-web/api/login", dataForm).Result;
            var tokne = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(response);
            JavaScriptSerializer oJS = new JavaScriptSerializer();
            employe em = new employe();
            em = oJS.Deserialize<employe>(tokne);

           // Console.WriteLine(em.nom);
            if(em==null)
            {
                TempData["Message"] = "Verifier Vos parametre";
                return RedirectToAction("Create");

            }
            Session["emnom"] = em.nom;
            return RedirectToAction("Index","Admin");
        }



        [HttpPost]
        public ActionResult loginqr(string codeqr)
        {

            var list = service.GetAll();
            var x = service.GetAll().Where(a => a.qrlogin == codeqr).FirstOrDefault();

            Console.WriteLine(x);
            


            Console.WriteLine(list);





            return View();
            
        }
    }

   



}
