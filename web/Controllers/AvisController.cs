using domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class AvisController : Controller
    {
        // GET: Avis
        public ActionResult Index()
        {
            String idemp = (string)System.Web.HttpContext.Current.Session["id"].ToString();
            int x = int.Parse(idemp);
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("projetintegre-web/rest/employee/aff/"+x).Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<avis>>().Result;
            }
            else
            {
                ViewBag.result = "erroe";
            }


            return View();
        }

        // GET: Avis/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Avis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Avis/Create
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

        // GET: Avis/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Avis/Edit/5
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

        // GET: Avis/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        public ActionResult logout()
        {
            return RedirectToAction("loginout", "Employee");

        }

        public ActionResult eval360()
        {
            return RedirectToAction("Index", "Commentaire");


        }
        
        public ActionResult stat()
        {
            return RedirectToAction("Dashboard", "Employee");

            //return RedirectToAction("Dashboard");
        }

        // POST: Avis/Delete/5
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
    }
}
