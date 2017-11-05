using Newtonsoft.Json;
using ProjektiTietokanta.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektiTietokanta.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Projects
        public ActionResult Hallinnoi() {
            return View();
        }
        
        public ActionResult NewProject() {
            return View();
        }

        public JsonResult AddProject(string projectname, string leader, DateTime opened) {
            ProjektiYlläpitoEntities2 en = new ProjektiYlläpitoEntities2();

            Projektit3 project = new Projektit3();
            project.Nimi = projectname;
            project.Esimies = leader;
            project.Avattu = opened;
            en.Projektit3.Add(project);
            en.SaveChanges();

            en.Dispose();

            return Json("Success");
        }

        public JsonResult RemoveProject(string removestring) {
            ProjektiYlläpitoEntities2 en = new ProjektiYlläpitoEntities2();

            var removeproject = (from p in en.Projektit3
                         where p.Nimi == removestring
                         select p).FirstOrDefault();

            if (removeproject != null) {
                ObjectContext oc = ((IObjectContextAdapter)en).ObjectContext;
                oc.DeleteObject(removeproject);
                oc.SaveChanges();
                
            }
            
            en.Dispose();

            return Json("Success");
        }

        public JsonResult GetprojectValue(string getval) {

            ProjektiYlläpitoEntities2 entity = new ProjektiYlläpitoEntities2();

            var singleProject = (from p in entity.Projektit3
                                 where p.Nimi == getval
                                 select p).FirstOrDefault();

            entity.Dispose();

            string json = JsonConvert.SerializeObject(singleProject);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModifyPost(string projectname, string leader, string employee) {

            ProjektiYlläpitoEntities2 entity = new ProjektiYlläpitoEntities2();

            Henkilot3 henkilot = new Henkilot3();
            henkilot.Etunimi = employee;
            henkilot.Esimies = leader;
            entity.Henkilot3.Add(henkilot);
            entity.SaveChanges();

            entity.Dispose();
            
            return Json("success");

        }

        public JsonResult ClaimHours(string getval) {
            ProjektiYlläpitoEntities2 entity = new ProjektiYlläpitoEntities2();

            var getusers = (from p in entity.Projektit3
                            join h in entity.Henkilot3 on p.Esimies equals h.Esimies
                            where p.Nimi == getval
                            select new {
                                h.Etunimi,
                                p.Nimi
                            }).ToList();

            entity.Dispose();

            string json = JsonConvert.SerializeObject(getusers);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClaimhoursPost(string projectname, string employee, int hours) {

            ProjektiYlläpitoEntities2 entity = new ProjektiYlläpitoEntities2();

            var henkiloid = (from h in entity.Henkilot3
                             where h.Etunimi == employee
                             select h.Henkilot_id).FirstOrDefault();

            var projectid = (from p in entity.Projektit3
                             where p.Nimi == projectname
                             select p.Projekti_id).FirstOrDefault();

            Tunnit3 tunnit = new Tunnit3();

            tunnit.Henkilot_id = henkiloid;
            tunnit.Projekti_id = projectid;
            tunnit.Pvm = DateTime.Now;
            tunnit.Tunnit = hours;

            entity.Tunnit3.Add(tunnit);
            entity.SaveChanges();

            return Json("success");
        }
    }
}