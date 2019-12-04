using MySql.Data.MySqlClient.Memcached;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web.Models;

namespace web.Controllers
{
    public class TacheSOAController : Controller
    {
        // GET: Tache
        public ActionResult Index()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("AdvyteamProject2-web/rest/tache").Result;
            if (response.IsSuccessStatusCode)
            {
 
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Tache>>().Result;
            }
            else
            {
                ViewBag.result = "erroe";
            }


            return View();
        }

        // GET: Tache/Details/5
        public ActionResult Details(int id)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("AdvyteamProject2-web/rest/tache/getById/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<Tache>().Result;
            }
            else
            {
                ViewBag.result = "erroe";
            }

            return View();
        }

        // GET: Tache/Create
        public ActionResult Create()
        {
             return View();
        }

        // POST: Tache/Create
        [HttpPost]
        public ActionResult Create(Tache t)
        {
            HttpClient Client = new HttpClient();

            var response = Client.PostAsJsonAsync<Tache>("http://localhost:9080/AdvyteamProject2-web/rest/tache", t).Result;
 
            return RedirectToAction("Index");
        }

        // GET: Tache/Edit/5
        public ActionResult Edit(int id)
        {
            Tache t = new Tache();
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("AdvyteamProject2-web/rest/tache/getById/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                t= response.Content.ReadAsAsync<Tache>().Result;
            }
            return View(t);
        }

        // POST: Tache/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Tache t)
        {

            HttpClient Client = new HttpClient(); 
            var response = Client.PutAsJsonAsync<Tache>("http://localhost:9080/AdvyteamProject2-web/rest/tache", t).Result;
            return RedirectToAction("Index");
        }

        // GET: Tache/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tache/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            HttpClient Client = new HttpClient();
            HttpResponseMessage response = Client.DeleteAsync("http://localhost:9080/AdvyteamProject2-web/rest/tache/" + id).Result;
            return RedirectToAction("Index");
        }

        // GET: Tache/affecter_tache_projet 
        public ActionResult affecter_tache_projet()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("AdvyteamProject2-web/rest/tache").Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<Tache> l = response.Content.ReadAsAsync<IEnumerable<Tache>>().Result;


                ViewBag.result = new SelectList(l.ToList(), "id", "nom");
            }
            HttpClient Client1 = new HttpClient();
            Client1.BaseAddress = new Uri("http://localhost:9080");
            Client1.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response1 = Client1.GetAsync("AdvyteamProject2-web/rest/projet").Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<Projet> l1 = response.Content.ReadAsAsync<IEnumerable<Projet>>().Result;


                ViewBag.resultP = new SelectList(l1.ToList(), "id", "nom");
            }



            return View();
        }
        // POST: Tache/affecter_tache_projet/id_projet/id_tache
        [HttpPost]
        public ActionResult affecter_tache_projet(int id_projet, int id_tache)
        {
            Tache t = new Tache();
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("AdvyteamProject2-web/rest/tache/getById/" + id_tache).Result;
            if (response.IsSuccessStatusCode)
            {
                t = response.Content.ReadAsAsync<Tache>().Result;
            }

            t.projet_id = id_projet;
            this.Edit(id_tache, t);
             return View();
        }
    }
}
