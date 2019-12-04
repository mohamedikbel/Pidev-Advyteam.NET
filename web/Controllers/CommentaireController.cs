using domain;
using PidevFinal.data.Infrastructure;
using servicePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using web.Controllers;

namespace web.Controllers
{
    public class CommentaireController : Controller
    {
        IDatabaseFactory Factory = new DatabaseFactory();

       public System.Web.SessionState.HttpSessionState Session { get; }

   
    // GET: Commentaire
    public ActionResult Index()
        {
            String idemp = (string)System.Web.HttpContext.Current.Session["id"].ToString();
            int x = int.Parse(idemp);
            ViewBag.idemploye = x;
            ViewBag.idemp = x;

            List<evaluation360> appo = new List<evaluation360>();
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<evaluation360> jbService = new Service<evaluation360>(Uok);

            appo = jbService.GetAll().ToList();


            return View(appo);
        }

        // GET: Commentaire/Details/5
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

        // GET: Commentaire/Create/id/idemp
        public ActionResult Create(int id, int idemp)
        {
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<commentaire> jbService = new Service<commentaire>(Uok);
            var Model = new commentaire();

            Model.commentaireEvzl360_id = id;
            Model.employecommentaire_id = idemp;

            return View(Model);

        }


        // POST: Commentaire/Create/id/idemp
        [HttpPost]
        public ActionResult Create(commentaire c, int id, int idemp)
        {

            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<commentaire> jbService = new Service<commentaire>(Uok);
            List<commentaire> appo = new List<commentaire>();
            appo = jbService.GetAll().ToList();
            for (int i = appo.Count - 1; i >= 0; i--)
            { if (appo[i].commentaireEvzl360_id == id && appo[i].employecommentaire_id == idemp)
                    return View("Exist");

            }
            c.commentaireEvzl360_id = id;
            c.employecommentaire_id = idemp;
            jbService.Add(c);
            jbService.Commit();
            String y = (string)System.Web.HttpContext.Current.Session["id"].ToString();
            int x = int.Parse(y);
            ViewBag.idemploye = x;
            return RedirectToAction("IndexByEmp", new { idemploye = x });




        }




        // GET: Commentaire/Edit/5
        public ActionResult Edit(int id)
        {
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<commentaire> jbService = new Service<commentaire>(Uok);
            commentaire app = jbService.GetById(id);

            return View(app);
        }

        // POST: Commentaire/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                IUnitOfWork Uok = new UnitOfWork(Factory);
                IService<commentaire> jbService = new Service<commentaire>(Uok);
                commentaire app = jbService.GetById(id);
                // TODO: Add update logic here
                app.comment = Request.Form["comment"];
                jbService.Commit();
                return RedirectToAction("Index", new { id = app.id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Commentaire/Delete/5
        public ActionResult Delete(int id)
        {
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<commentaire> jbService = new Service<commentaire>(Uok);
            commentaire app = jbService.GetById(id);
            if (app == null)
                return View("NotFound");
            else return View(app);
        }

        // POST: Commentaire/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<commentaire> jbService = new Service<commentaire>(Uok);
            if (ModelState.IsValid)
            {
                jbService.Delete(jbService.GetById(id));
                jbService.Commit();
                jbService.Dispose();

                return View("deleted");

            }
            return RedirectToAction("Index");
        }


        // GET: Commentaire/afficher/id
        public ActionResult afficher(commentaire id, FormCollection collection)
        {
            List<commentaire> appo = new List<commentaire>();
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<commentaire> jbService = new Service<commentaire>(Uok);
            appo = jbService.GetAll().ToList();
            return View(appo);


        }

        // GET: Commentaire/IndexByEmp/idemploye
        public ActionResult IndexByEmp(int idemploye)
        {

        IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<commentaire> jbService = new Service<commentaire>(Uok);
            IService<employe> serviceemploye = new Service<employe>(Uok);

            employe emp = serviceemploye.GetById(idemploye);
            String idemp = (string)System.Web.HttpContext.Current.Session["id"].ToString();

            int x = int.Parse(idemp);
          //  ViewBag.idemploye = 2;
            if (x != 0)
            {
              
                List<commentaire> appo = new List<commentaire>();
                List<commentaire> j = new List<commentaire>();
                appo = jbService.GetAll().ToList();
                for (int i = appo.Count - 1; i >= 0; i--)
                {
                    if (appo[i].employecommentaire_id == x)
                    {
                        j.Add(appo[i]);
                    }
                }

                return View(j);

            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }
        public static int compternbeval(int idemploye)
        {
            int x = 0;
            IDatabaseFactory Factory = new DatabaseFactory();
            IUnitOfWork Uok = new UnitOfWork(Factory);
            IService<evaluation360> jbService = new Service<evaluation360>(Uok);
            IService<commentaire> jbService1 = new Service<commentaire>(Uok);

            List<commentaire> appo = new List<commentaire>();
            List<evaluation360> evalall = new List<evaluation360>();
            List<evaluation360> evallist = new List<evaluation360>();

            appo = jbService1.GetAll().ToList();
            evalall = jbService.GetAll().ToList();
            for (int i = evalall.Count - 1; i >= 0; i--)
            {
                if (evalall[i].evaluationEmploye360_id == idemploye)
                    evallist.Add(evalall[i]); }
            for (int j = appo.Count - 1; j >= 0; j--)
            {
                for (int k = evallist.Count - 1; k >= 0; k--)
                {
                    if (appo[j].commentaireEvzl360_id == evallist[k].id)
                        x++;
                  //  ViewBag.t = x;
                }


            }



            return x;

        }

        public ActionResult eval360()
        {
            return RedirectToAction("Index", "Avis");


        }
    }

    }

