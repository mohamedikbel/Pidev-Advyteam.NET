using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using data;
using service;
using Service.Pattern;
using PiDevNet.data.infrastructure;
using PiDevNet.data;
using System.Net.Mail;
using Newtonsoft.Json;

namespace web.Controllers
{
    public class congesController : Controller
    {
        private Context db = new Context();
        static  IDatabaseFactory Factory = new DatabaseFactory();
         
        public ActionResult reinitialiserConges()
        {
            List<employe> appo = new List<employe>();
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<employe> jbService = new Service<employe>(Uok);
            List<employe> t = new List<employe>();
            appo = jbService.GetAll().ToList();

            for (int i = appo.Count - 1; i >= 0; i--)
            {
                appo[i].nb_conge_restants = appo[i].nb_jours_conge_total;
                jbService.Commit();

            }
            return RedirectToAction("list_demandes_manager");

        }
        // GET: conges
        public ActionResult Index()
        {
            ICongeService s = new CongeService();
            List<conge> l = new List<conge>();
            l = s.consulterMesProjets(1);
            return View(l);

            /*var conge = db.conge.Include(c => c.employe);
            return View(conge.ToList());*/
        }
        public ActionResult consulterMesConges()
        {
            ICongeService s = new CongeService();
            List<conge> l = new List<conge>();
            l = s.consulterMesProjets(1);
             return View(l);
        }
        public ActionResult list_demandes_manager()
        {
            ICongeService s = new CongeService();
            List<conge> l = new List<conge>();
            l = s.list_demandes_enAttente();
            return View(l);
        }
        
        // GET: conges/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<conge> jbService = new Service<conge>(Uok);
            conge app = jbService.GetById(id);
            return View(app);
            if (app == null)
            {
                return HttpNotFound();
            }
            return View(app);
        }
        public ActionResult valider(int id)
        {
             if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             IUnitOfWork Uok = new UnitOfWork(Factory);
             IService<conge> jbService = new Service<conge>(Uok);
             IService<employe> jbServiceEmploye = new Service<employe>(Uok);

             conge app = jbService.GetById(id);
              employe e = jbServiceEmploye.GetById(app.employe_id.Value);
            DateTime fin = DateTime.Parse(app.date_fin.ToString());
            DateTime deb = DateTime.Parse(app.date_deb.ToString());
            TimeSpan Diff = fin - deb;
            int nb = int.Parse(Diff.Days.ToString()) + 1;
            // TODO: Add update logic here

            if (app == null)
             {
                 return HttpNotFound();
             }
             else
             {  if (e.nb_conge_restants - nb >= 0)
                { app.etat = "valide";
                   
                    e.nb_conge_restants = e.nb_conge_restants - nb;

                    jbServiceEmploye.Commit();
                    //envoyer mail

                    var verifyurl = "/Signup/VerifiyAccount/";
                    var link = Request.Url.AbsolutePath.Replace(Request.Url.PathAndQuery, verifyurl);

                    var fromEmail = new MailAddress("manel.lamloum@esprit.tn", "lamloum manel");
                    var toEmail = new MailAddress(e.email);

                   // var toEmail = new MailAddress("marammtir12@gmail.com");
                    var FromEmailPassword = "kesstheahmerkwi";

                    string subject = "Validation du conge";

                    string body = "Nous vous informons  de l'acceptation de votre demande de congé du  " +app.date_deb+
                        " jusqu'à " + app.date_fin ;

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


                    //envoyer mail
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Cet employe n'a que " + e.nb_conge_restants + " jours de congé restants cette année ! DEMANDE REFUSEE !");
                    this.refuser(app.id);
                }
            }
             return RedirectToAction("list_demandes_manager"); 
         }
        public ActionResult refuser(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<conge> jbService = new Service<conge>(Uok);
            conge app = jbService.GetById(id);
            if (app == null)
            {
                return HttpNotFound();
            }
            else
            {
                app.etat = "refuse";
                jbService.Commit();
                //mail
                var verifyurl = "/Signup/VerifiyAccount/";
                var link = Request.Url.AbsolutePath.Replace(Request.Url.PathAndQuery, verifyurl);

                var fromEmail = new MailAddress("manel.lamloum@esprit.tn", "lamloum manel");
                var toEmail = new MailAddress(app.employe.email);

                // var toEmail = new MailAddress("marammtir12@gmail.com");
                var FromEmailPassword = "kesstheahmerkwi";

                string subject = "Validation du conge";

                string body = "Nous vous informons  que  votre demande de congé du  " + app.date_deb +
                    " jusqu'à " + app.date_fin+"  a été refusée.";

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


                //mail
            }
            return RedirectToAction("list_demandes_manager");
        }
       


    
        public ActionResult consulterEmployesNbConges()
        {
            IUnitOfWork Uok = new UnitOfWork(Factory);

            IService<employe> s = new Service<employe>(Uok);
            List<employe> l = new List<employe>();
            l = s.GetAll().ToList();
            return View(l);
        }


        // GET: conges/listCongeParEmploye/id  
        public ActionResult listCongeParEmploye(int id)
        {
            IUnitOfWork Uok = new UnitOfWork(Factory);

            ICongeService  s = new CongeService();
            List<conge> l = new List<conge>();
            l = s.getCongesParEmploye(id);
            return View(l);
        }





        // GET: conges/DemandeConge
        public ActionResult DemandeConge()
        {
            ViewBag.employe_id = new SelectList(db.employe, "id", "email");
            return View();
        }

        // POST: conges/DemandeConge 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DemandeConge( conge conge)
        {
            IUnitOfWork Uok = new UnitOfWork(Factory); 
            IService<conge> iser = new Service<conge>(Uok);
            IService<employe> jbServiceEmploye = new Service<employe>(Uok);
            employe e = jbServiceEmploye.GetById(conge.employe_id.Value);
            DateTime fin = DateTime.Parse(conge.date_fin.ToString());
            DateTime deb = DateTime.Parse(conge.date_deb.ToString());
            TimeSpan Diff = fin - deb;
            int nb = int.Parse(Diff.Days.ToString())+1;
 
            if (ModelState.IsValid)
            {
                if (conge.date_deb <= conge.date_fin)
                {  if (conge.paye == true)
                    {
                        if (e.nb_conge_restants - nb >= 0)
                        {
                            DateTime localDate = DateTime.Now;
                            conge.date_demande = localDate;
                            conge.etat = "non Traitee";
                            iser.Add(conge);
                            iser.Commit();
                            System.Windows.Forms.MessageBox.Show("demande de congé payé envoyée!");

                            return RedirectToAction("consulterMesConges");
                        }
                        else
                            System.Windows.Forms.MessageBox.Show("Vous n'avez que "+ e.nb_conge_restants + " jours de cingé restants cette année ! DEMANDE NON ENVOYEE !");


                    }
                    else
                    {
                        DateTime localDate = DateTime.Now;
                        conge.date_demande = localDate;
                        conge.etat = "non Traitee";
                        iser.Add(conge);
                        iser.Commit();
                        System.Windows.Forms.MessageBox.Show("demande congé non payé envoyée!");

                        return RedirectToAction("consulterMesConges");

                    }
                }
                else
                    System.Windows.Forms.MessageBox.Show("Selectionnez date de début et date fin valide !");


            }

            ViewBag.employe_id = new SelectList(db.employe, "id", "email", conge.employe_id);
            return View(conge);
        }











        // GET: conges/Create
        public ActionResult Create()
        {
            ViewBag.employe_id = new SelectList(db.employe, "id", "email");
            return View();
        }

        // POST: conges/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,date_deb,date_fin,paye,employe_id,date_demande,etat")] conge conge)
        {
            if (ModelState.IsValid)
            {
                db.conge.Add(conge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.employe_id = new SelectList(db.employe, "id", "email", conge.employe_id);
            return View(conge);
        }

        // GET: conges/Edit/5
        public ActionResult Edit(int id)
        {
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<conge> jbService = new Service<conge>(Uok);
            conge app = jbService.GetById(id);

             ViewBag.employe_id = new SelectList(db.employe, "id", "email", app.employe_id);
            return View(app);
        }

        // POST: conges/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection formValues , int id)
        {
            
                IUnitOfWork Uok = new UnitOfWork(Factory);
                IService<conge> jbService = new Service<conge>(Uok);
                conge app = jbService.GetById(id);
            // TODO: Add update logic here
           app.date_deb = DateTime.Parse(Request.Form["date_deb"]);
             app.date_fin= DateTime.Parse(Request.Form["date_fin"]);
 
           app.etat = Request.Form["etat"];
                jbService.Commit();

            return RedirectToAction("Index");

        }

        // GET: conges/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<conge> jbService = new Service<conge>(Uok);
            conge conge = jbService.GetById(id);
            if (conge == null)
            {
                return HttpNotFound();
            }
            return View(conge);
        }

        // POST: conges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int  id)
        {
            IUnitOfWork Uok = new UnitOfWork(Factory);

            IService<conge> iser = new Service<conge>(Uok);
            conge conge = iser.GetById(id);
            iser.Delete(conge);
            iser.Commit();

            /*db.conge.Remove(conge);
            db.SaveChanges();*/
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult getStat(int? idE ,int? annee)
        {
            IUnitOfWork Uok = new UnitOfWork(Factory);

            ITacheService s = new TacheService();
             List<string> typeC = new List<string>();
            List<int> rep = new List<int>();
            DateTime now = DateTime.Now;
            for (int i = 0; i < 12; i++)
            {
                int somme = s.getNbHeuresTravailParEmploye(1, "January", 2019);
                 now = now.AddMonths(1);

                typeC.Add(now.Month.ToString());
                rep.Add(somme);
                

            }

            string mois = JsonConvert.SerializeObject(typeC);
            string nbheures = JsonConvert.SerializeObject(rep);


            ViewBag.mois = mois;
            ViewBag.nbheures = nbheures; 
            return View();
        }

    }
}
