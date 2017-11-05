using Newtonsoft.Json;
using ProjektiTietokanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektiTietokanta.Controllers
{
    public class ClaimInfoController : Controller
    {
        // GET: ClaimInfo
        public ActionResult Claim() {
            return View();
        }

        public JsonResult GetAllEmployees() {
            ProjektiYlläpitoEntities2 yllapito = new ProjektiYlläpitoEntities2();

            var projects = (from h in yllapito.Henkilot3
                            join p in yllapito.Projektit3 on h.Esimies equals p.Esimies
                            join t in yllapito.Tunnit3 on h.Henkilot_id equals t.Henkilot_id
                            select new
                            {
                                Employee = h.Etunimi,
                                ProjectName = p.Nimi,
                                Hours = t.Tunnit
                            }).ToList();


            yllapito.Dispose();

            string json = JsonConvert.SerializeObject(projects);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}