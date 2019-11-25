using domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using web.Models;

namespace web.Controllers
{
    public class TestController : Controller
    {
        
        public ActionResult Index()
        {

            IEnumerable<test> testls;
            HttpResponseMessage response = GlobalVariable.webApiClient.GetAsync("test").Result;
            testls =  response.Content.ReadAsAsync<IEnumerable<test>>().Result;
            return View(testls);
        }

       
        public ActionResult Details(int id)
        {

            HttpResponseMessage response = GlobalVariable.webApiClient.GetAsync("test/"+id.ToString()).Result;
            test t = response.Content.ReadAsAsync<test>().Result;
            ViewBag.t = t;
            HttpResponseMessage response1 = GlobalVariable.webApiClient.GetAsync("test/Quest/" + id.ToString()).Result;
            IEnumerable<question> lsq = response1.Content.ReadAsAsync<IEnumerable<question>>().Result;
            ViewBag.lsq = lsq;

            return View();
        }

        
        public ActionResult Create()
        {
            return View(new testview());
        }

        
        [HttpPost]
        public ActionResult Create(testview t)
        {
            try
            {
               
                HttpResponseMessage response = GlobalVariable.webApiClient.PostAsJsonAsync<testview>("test", t).Result;
                TempData["SM"] = "Ajouter Avec Success";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Edit(int id)
        {
            HttpResponseMessage response1 = GlobalVariable.webApiClient.GetAsync("test/" + id.ToString()).Result;
            testview t = response1.Content.ReadAsAsync<testview>().Result;
            return View(t);
        }

        // POST: Test/Edit/5
        [HttpPost]
        public ActionResult Edit(testview t)
        {
            try
            {
                HttpResponseMessage response = GlobalVariable.webApiClient.PutAsJsonAsync<testview>("test/" + t.formation.id.ToString(), t).Result;
                TempData["SM"] = "Modifier Avec Success";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariable.webApiClient.DeleteAsync("test/"+id.ToString()).Result;
            TempData["SM"] = "Supprimer Avec Success";
            return RedirectToAction("Index");
        }

        public ActionResult AddForm(int id)
        {
            HttpResponseMessage response = GlobalVariable.webApiClient.GetAsync("formation").Result;
           List<formation> lsf = response.Content.ReadAsAsync<List<formation>>().Result;
            ViewBag.formation = lsf;
            TForm t = new TForm() { idT = id };
            return View(t);
        }

        [HttpPost]
        public ActionResult AddForm(TForm tf)
        {
            HttpResponseMessage response = GlobalVariable.webApiClient.PutAsync("test/AddF/"+tf.idT.ToString()+"/"+tf.idf.ToString(),null).Result;
            TempData["SM"] = "Test affecter Avec Success";
            return RedirectToAction("Index");
        }

    }
}
