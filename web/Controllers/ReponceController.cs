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
    public class ReponceController : Controller
    {
      


        // GET: Reponce/Create
        public ActionResult Create(int id)
        {
            List<SelectListItem> lst = new List<SelectListItem>() {
                new SelectListItem { Text = "Vrai", Value=true.ToString() },
                new SelectListItem { Text = "faux", Value=false.ToString() },
            };
            ViewBag.lst = lst;
            ViewBag.id = id;
            question q = new question() { id = id };
            reponceV r = new reponceV();
            r.question = q;
            return View(r);
        }

        // POST: Reponce/Create
        [HttpPost]
        public ActionResult Create(reponceV r)
        {
            int i = r.question.id;
            r.question = null;
            HttpResponseMessage response = GlobalVariable.webApiClient.PostAsJsonAsync<reponceV>("test/AddR/" + i.ToString(), r).Result;
            TempData["SM"] = "Ajouter Avec Success";

            return RedirectToRoute(new
            {
                controller = "Question",
                action = "Details",
                id = i
            });
        }

        // GET: Reponce/Edit/5
        public ActionResult Edit(int id)
        {
           
            List<SelectListItem> lst = new List<SelectListItem>() {
                new SelectListItem { Text = "Vrai", Value=true.ToString() },
                new SelectListItem { Text = "faux", Value=false.ToString() },
            };
            ViewBag.lst = lst;

            HttpResponseMessage response1 = GlobalVariable.webApiClient.GetAsync("test/Quest/Rep/get/" + id.ToString()).Result;
            reponceV r = response1.Content.ReadAsAsync<reponceV>().Result;
            ViewBag.id = r.question.id;
            return View(r);
        }

        // POST: Reponce/Edit/5
        [HttpPost]
        public ActionResult Edit(reponceV r)
        {
            int i = r.question.id;
            r.question = null;
            HttpResponseMessage response = GlobalVariable.webApiClient.PutAsJsonAsync<reponceV>("test/Rep/" +i.ToString(), r).Result;
            TempData["SM"] = "Modifier Avec Success";

            return RedirectToRoute(new
            {
                controller = "Question",
                action = "Details",
                id = i
            });
        }

        // GET: Reponce/Delete/5
        public ActionResult Delete(int idq, int idt)
        {
            HttpResponseMessage response = GlobalVariable.webApiClient.DeleteAsync("test/Rep/" + idq.ToString()).Result;
            TempData["SM"] = "Supprimer Avec Success";
            return RedirectToRoute(new
            {
                controller = "Question",
                action = "Details",
                id = idt
            });
        }

       
    }
}
