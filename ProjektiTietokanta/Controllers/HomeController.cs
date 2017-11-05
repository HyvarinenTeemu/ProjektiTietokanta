using Newtonsoft.Json;
using ProjektiTietokanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektiTietokanta.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {

            ViewBag.Title = "Projekti ylläpito";
                return View();
        }

        public JsonResult GetProjects() {
            ProjektiYlläpitoEntities2 yllapito = new ProjektiYlläpitoEntities2();

            var projects = (from p in yllapito.Projektit3
                            select new {
                                ProjectName = p.Nimi,
                                ProjectOpened = p.Avattu,
                                ProjectLeader = p.Esimies
                            }).ToList();
            

            yllapito.Dispose();

            string json = JsonConvert.SerializeObject(projects);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}