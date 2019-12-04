using data;
using domain;
using Newtonsoft.Json;
using PidevFinal.data.Infrastructure;
using servicePattern;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using web.Models;

namespace web.Controllers
{
    public class CritereController : Controller
    {
        Model1 db = new Model1();

        IDatabaseFactory Factory = new DatabaseFactory();

        public CritereController()
        {

        }
        // GET: Critere
        public ActionResult Index()
        {
         String   nom = (string)System.Web.HttpContext.Current.Session["nom"].ToString();
            ViewBag.nom = nom;
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:9080/projetintegre-web/rest/critere").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<critere>>().Result;
            }
            else
            {
                ViewBag.result = "erroe";
            }


            return View();
        }

        // GET: Critere/Details/5
        public ActionResult Details(int id)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:9080/projetintegre-web/rest/evaluation/"+id).Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<domain.evaluation>>().Result;
            }
            else
            {
                ViewBag.result = "erroe";
            }

            return View();
        }

        // GET: Critere/Create
        public ActionResult Create()
        {
            ViewBag.evaluation = db.evaluation;
             return View();
        }

        // POST: Critere/Create
        [HttpPost]
        public ActionResult Create(critereModels e,evaluationModels v)
        {
            HttpClient Client = new HttpClient();

            var response = Client.PostAsJsonAsync<critereModels>("http://localhost:9080/projetintegre-web/rest/critere/", e).Result;
            //var response = Client.PutAsJsonAsync<evaluationModels>("http://localhost:9080/projetintegre-web/rest/critere/",v,"/",e).Result;

            // var response1 = Client.PostAsJsonAsync<critereModels>("http://localhost:9080/projetintegre-web/rest/evaluation/"+3+"/"+1,e).Result;

            return RedirectToAction("Index");
      
        }


        // GET: Critere/Edit/5
        public ActionResult Edit(int id)
        {
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<critere> jbService = new Service<critere>(Uok);
            critere app = jbService.GetById(id);

                  return View(app);

        }

        // PUT: Critere/Edit
        [HttpPut]
        public  ActionResult Edit(critereModels e, FormCollection collection)
        {
            HttpClient Client = new HttpClient();


            var response = Client.PutAsJsonAsync<critereModels>("http://localhost:9080/projetintegre-web/rest/critere", e).Result;
            return RedirectToAction("Index");
        }

        // GET: Critere/Delete/5
        public ActionResult Delete(int id)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:9080/projetintegre-web/rest/critere/RecupererCritereById/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<domain.critere>>().Result;
            }
            else
            {
                ViewBag.result = "erroe";
            }

            return View();
        }

        // POST: Critere/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {

            HttpClient Client = new HttpClient();
            HttpResponseMessage response = Client.DeleteAsync("http://localhost:9080/projetintegre-web/rest/critere/" + id.ToString()).Result;

            // TODO: Add delete logic here

            return View("deleted");



        }

        // GET: Critere/Affectercritere/id
        public ActionResult Affectercritere(int id)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:9080/projetintegre-web/rest/evaluation").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<domain.evaluation>>().Result;
            }
            else
            {
                ViewBag.result = "erroe";
            }


            return View();
        }
        public ActionResult logout()
        {
            return RedirectToAction("loginout", "Employee");


        }
        public ActionResult traiter()
        {
            return RedirectToAction("Index", "Evaluation360");


        }
    }
 
}
    

