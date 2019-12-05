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
    public class QuestionController : Controller
    {
       

        
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = GlobalVariable.webApiClient.GetAsync("test/Quest/get/" + id.ToString()).Result;
            questionView q = response.Content.ReadAsAsync<questionView>().Result;
            ViewBag.q = q;

            HttpResponseMessage response1 = GlobalVariable.webApiClient.GetAsync("test/Quest/Rep/" + id.ToString()).Result;
            IEnumerable<reponce> lsr = response1.Content.ReadAsAsync<IEnumerable<reponce>>().Result;
            ViewBag.lsr = lsr;
            return View();
        }

        
        public ActionResult Create(int id)
        {
            List<SelectListItem> lst = new List<SelectListItem>() {
                new SelectListItem { Text = "Text", Value="Text" },
                new SelectListItem { Text = "Image", Value="Image" },
            };
            ViewBag.lst = lst;
            ViewBag.id = id;

            questionView q = new questionView();
            q.test = new testview() { id = id};
            return View(q);
        }

       
        [HttpPost]
        public ActionResult Create(questionView q)
        {
            int i = q.test.id;
            q.test = null;
            HttpResponseMessage response = GlobalVariable.webApiClient.PostAsJsonAsync<questionView>("test/AddQ/"+i.ToString(), q).Result;
            TempData["SM"] = "Ajouter Avec Success";

            return RedirectToRoute(new
            {
                controller = "Test",
                action = "Details",
                id = i
            });


        }

        
        public ActionResult Edit(int id)
        {
            List<SelectListItem> lst = new List<SelectListItem>() {
                new SelectListItem { Text = "Text", Value="Text" },
                new SelectListItem { Text = "Image", Value="Image" },
            };
            ViewBag.lst = lst;
            HttpResponseMessage response1 = GlobalVariable.webApiClient.GetAsync("test/Quest/get/" + id.ToString()).Result;
            questionView q = response1.Content.ReadAsAsync<questionView>().Result;
            ViewBag.id = q.test.id;
            return View(q);
        }

        
        [HttpPost]
        public ActionResult Edit(questionView q)
        {
            HttpResponseMessage response = GlobalVariable.webApiClient.PutAsJsonAsync<questionView>("test/Quest/" +q.test.id.ToString(), q).Result;
            TempData["SM"] = "Modifier Avec Success";

            return RedirectToRoute(new
            {
                controller = "Test",
                action = "Details",
                id = q.test.id
            });
        }

        
        public ActionResult Delete(int idq , int idt)
        {
            HttpResponseMessage response = GlobalVariable.webApiClient.DeleteAsync("test/Quest/" + idq.ToString()).Result;
            TempData["SM"] = "Supprimer Avec Success";
            return RedirectToRoute(new
            {
                controller = "Test",
                action = "Details",
                id = idt
            });
        }

        
    }
}
