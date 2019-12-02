using domain.entities;
using MyFinance.Data.Infrastructure;
using Newtonsoft.Json;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using web.Models;

namespace web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        IDataBaseFactory dbf;
        IUnitOfWork uow;
        IService<employe> service;
        public AdminController()
        {
            dbf = new DataBaseFactory();
            uow = new UnitOfWork(dbf);
            service = new Service<employe>(uow);

        }
        public ActionResult Index()
        {


        HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("pidev-web/api/employe").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<employe>>().Result;
            }
            else
            {
                ViewBag.result = "error";
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employe emp = service.GetById(id);
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }
        // GET: Admin/Details/5
      

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Createe(employe c)
        {

            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            var sqlFormattedDate = c.datedn.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string json = JsonConvert.SerializeObject(c);
            Console.WriteLine(json);
            Client.PostAsync("pidev-web/api/employe", new StringContent(json,Encoding.UTF8,"application/json")).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).GetAwaiter().GetResult();
           // Client.DefaultRequestHeaders.ExpectContinue = false;

            //HttpResponseMessage res = await Client.PostAsync(new Uri("http://localhost:9080/pidev-web/api/employe", UriKind.RelativeOrAbsolute), new StringContent(json, Encoding.UTF8, "application/json"));

            return RedirectToAction("Index");











            return RedirectToAction("Index");
        }

        // POST: Admin/Create
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

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
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

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employe emp = service.GetById(id);
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        [HttpPost, ActionName("Deletee")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DeleteAsync("pidev-web/api/employe/" + id).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).GetAwaiter().GetResult();
            return RedirectToAction("Index");
        }
        // POST: Admin/Delete/5
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
