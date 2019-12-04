using data;
using domain;
using PidevFinal.data.Infrastructure;
using service;
using servicePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class Evaluation360Controller : Controller
    {
        Model1 db = new Model1();

        IDatabaseFactory Factory = new DatabaseFactory();

        Evaluation360Service Cs;

        public Evaluation360Controller()
        {
            Cs = new Evaluation360Service();
        }
        // GET: Evaluation360
        public ActionResult Index()
        {
            List<evaluation360> appo = new List<evaluation360>();
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<evaluation360> jbService = new Service<evaluation360>(Uok);

            appo = jbService.GetAll().ToList();


            return View(appo);
        }

        // GET: Employe/Details/5
        public ActionResult Details(int id)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:9080/projetintegre-web/rest/employee/EmployeById/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<domain.employe>>().Result;
            }
            else
            {
                ViewBag.result = "erroe";
            }

            return View();
        }


        // GET: Evaluation360/Create
        public ActionResult Create()
        {
            ViewBag.employe = db.employe;
            return View();



        }

        // POST: Evaluation360/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(evaluation360 e)
        {
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<evaluation360> jbService = new Service<evaluation360>(Uok);
            IService<employe> x = new Service<employe>(Uok);

            employe emp = x.GetById((long)e.evaluationEmploye360_id);
            // TODO: Add insert logic here
            jbService.Add(e);
            jbService.Commit();

            var verifyurl = "/Signup/VerifiyAccount/";
            var link = Request.Url.AbsolutePath.Replace(Request.Url.PathAndQuery, verifyurl);

            var fromEmail = new MailAddress("nesria.guinoubi@esprit.tn", "nesria guinouvi");
            var toEmail = new MailAddress(emp.email);

            // var toEmail = new MailAddress("marammtir12@gmail.com");
            var FromEmailPassword = "183JFT0354";

            string subject = "Information sur le lancement d'une évaluation";

            string body = "votre manager a lancer une évaluation le "+e.datedebut+"pour vous évaluer .Monsieur/madame ."+emp.nom+" "+emp.prenom;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, FromEmailPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            }) smtp.Send(message);

            return RedirectToAction("Index");


        }

        // GET: Evaluation360/Edit/5
        public ActionResult Edit(int id)
        {
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<evaluation360> jbService = new Service<evaluation360>(Uok);
            evaluation360 app = jbService.GetById(id);

            return View(app);
        }

        // POST: Evaluation360/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                IUnitOfWork Uok = new UnitOfWork(Factory);
                IService<evaluation360> jbService = new Service<evaluation360>(Uok);
                evaluation360 app = jbService.GetById(id);
                // TODO: Add update logic here
                app.datedebut = DateTime.Parse(Request.Form["datedebut"]);

                jbService.Commit();
                return RedirectToAction("Index", new { id = app.id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Evaluation360/Delete/5
        public ActionResult Delete(int id)
        {
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<evaluation360> jbService = new Service<evaluation360>(Uok);
            evaluation360 app = jbService.GetById(id);
            if (app == null)
                return View("NotFound");
            else return View(app);
        }

        // POST: Evaluation360/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<evaluation360> jbService = new Service<evaluation360>(Uok);
            if (ModelState.IsValid)
            {
                jbService.Delete(jbService.GetById(id));
                jbService.Commit();
                jbService.Dispose();

                return View("deleted");

            }
            return RedirectToAction("Index");
        }
        public ActionResult eval360()
        {
            return RedirectToAction("Index", "Critere");


        }
        public ActionResult commentaire()
        {
            return RedirectToAction("IndexByEmp", "Commentaire");


        }

    }
}