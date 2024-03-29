﻿
using MyFinance.Data.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using System.Data;
using web.Models;
using System.Web;
using System.IO;
using System.Net.Mail;

namespace web.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("pidev-web/api/employe/" + id).Result;
            IEnumerable<employe> r = response.Content.ReadAsAsync<IEnumerable<employe>>().Result;
            List<employe> list = new List<employe>();

            foreach (var item in r)
            {
                list.Add(item);
            }


            if (r == null)
            {
                return HttpNotFound();
            }
            return View(list[0]);
        }
        // GET: Admin/Details/5
      

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Createe(employe c )
        {
           
           
            HttpClient Client = new HttpClient();
           Client.BaseAddress = new Uri("http://localhost:9080");

            string json = JsonConvert.SerializeObject(c);
             var myCleanJsonObject = JObject.Parse(json);
             string test = myCleanJsonObject.ToString().Substring(1, myCleanJsonObject.ToString().Length - 1);
            Console.WriteLine("Substring: {0}", test);
            System.Diagnostics.Debug.WriteLine(json);

            Client.PostAsync("pidev-web/api/employe", new StringContent(json,Encoding.UTF8, "application/json")).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).GetAwaiter().GetResult();


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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("pidev-web/api/employe/" + id).Result;
            IEnumerable<employe> r = response.Content.ReadAsAsync<IEnumerable<employe>>().Result;
            List<employe> list = new List<employe>();

            foreach (var item in r)
            {
                list.Add(item);
            }


            if (r == null)
            {
                return HttpNotFound();
            }
            return View(list[0]);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Edite(int id, employe e)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
             /* string json = JsonConvert.SerializeObject(e);
              Client.PutAsync("pidev-web/api/employe/"+id, new StringContent(json, Encoding.UTF8, "application/json")).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).GetAwaiter().GetResult();

      */

            string json = JsonConvert.SerializeObject(e);
            var myCleanJsonObject = JObject.Parse(json);
            string test = myCleanJsonObject.ToString().Substring(1, myCleanJsonObject.ToString().Length - 1);
            Console.WriteLine("Substring: {0}", test);
            System.Diagnostics.Debug.WriteLine(json);

            Client.PutAsync("pidev-web/api/employe/"+id, new StringContent(json, Encoding.UTF8, "application/json")).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).GetAwaiter().GetResult();
          
            return RedirectToAction("Index");
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("pidev-web/api/employe/" + id).Result;
            IEnumerable<employe> r = response.Content.ReadAsAsync<IEnumerable<employe>>().Result;
            List<employe> list = new List<employe>();

            foreach (var item in r)
            {
                list.Add(item);
            }


            if (r == null)
            {
                return HttpNotFound();
            }
            return View(list[0]);
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

        [HttpPost]
        public ActionResult SendSMS(string Num, string Body)
        {
            var senderEmail = new MailAddress("pidevadvyteam@gmail.com", "Advyteam");
            var receiverEmail = new MailAddress(Num,"Mohamed ikbel");

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, "ESPRIT123")
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = "Inscription MyApp",
                Body = Body
            })
            {
                smtp.Send(mess);
            }

            return RedirectToAction("Index");
        }



    }
}
