using domain.entities;
using MyFinance.Data.Infrastructure;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace web.Controllers
{

    public class RecupController : Controller
    {
        IDataBaseFactory dbf;
        IUnitOfWork uow;
        IService<employe> service;
        public RecupController()
        {
            dbf = new DataBaseFactory();
            uow = new UnitOfWork(dbf);
            service = new Service<employe>(uow);

        }
        // GET: Recup
        public ActionResult Index()
        {
            return View();
        }

        // GET: Recup/Details/5
        public ActionResult Details(int id)
        {
            return View();



        }

        // GET: Recup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recup/Create
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

        // GET: Recup/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Recup/Edit/5
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

        // GET: Recup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Recup/Delete/5
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
        public ActionResult Recup(string adress)
        {

           var x =  service.GetAll().Where(a => a.email == adress).FirstOrDefault();

            string z = x.codeqr;
             if(x!=null)

            {
                var senderEmail = new MailAddress("pidevadvyteam@gmail.com", "Advyteam");
                var receiverEmail = new MailAddress(adress, "Mohamed ikbel");

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
                    Subject = "Votre lien pour resinstaller votre adresse ",
                    Body = "localhost:49267/Recup/verif?mail="+adress+"&&qr="+z
            })
                {
                    smtp.Send(mess);
                }
            }


            return RedirectToAction("Index");
        }

        public ActionResult verif(string mail , string qr)
          {

            var x = service.GetAll().Where(a => a.email == mail && a.codeqr == qr).FirstOrDefault();

           


            if (x!=null)
            {
                Session["mailrec"] = mail;
                Session["qr"] = qr;
                return RedirectToAction("ChangerMotdepasse");

            }

            else { return RedirectToAction("Erreur"); }
            

        }


         public ActionResult ChangerMotdepasse()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Changer(string nvmp)
        {

            string exist = string.Empty;

            FormUrlEncodedContent dataForm = new FormUrlEncodedContent(new[] {
            new KeyValuePair<string,string>("mailr",Session["mailrec"].ToString()),
            new KeyValuePair<string,string>("qrr",Session["qr"].ToString()),
            new KeyValuePair<string, string>("pass",nvmp)
        });



            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            HttpResponseMessage response = Client.PutAsync("pidev-web/api/recuperer",dataForm).Result;
            var tokne = response.Content.ReadAsStringAsync().Result;
            IEnumerable<employe> r = response.Content.ReadAsAsync<IEnumerable<employe>>().Result;
            List<employe> list = new List<employe>();

            foreach (var item in r)
            {
                list.Add(item);
            }
            // Console.WriteLine(em.nom);
            if (list[0] == null)
            {
                TempData["Message"] = "Verifier Vos parametre";
                return RedirectToAction("Create", "Login");

            }



            TempData["Message"] = "Félicitation !  Votre compte est récuperé";
            return RedirectToAction("Create", "Login");
    }


        public ActionResult Erreur()
        {
            return View();
        }


    }


}
