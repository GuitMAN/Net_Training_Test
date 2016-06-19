using Net_Training_Test.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Net_Training_Test.Controllers
{
    public class HomeController : Controller
    {
        //private People;

        ///private string xmlurl;
        private Repository People;

        [HttpGet]
        public ActionResult Index()
        {
            //string tt = Request.PhysicalPath;
            People = new Repository(Request.PhysicalApplicationPath + "/Content/users.xml");
            return View(People.getList());
        }

        [HttpGet]
        public ActionResult create()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult create(Person person)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("YearBorn", "Form is not valid");
                return View(person);
            }
            People = new Repository(Request.PhysicalApplicationPath + "/Content/users.xml");
            People.addPerson(person);
            return Redirect("Index");
        }

        [HttpGet]
        public ActionResult edit(string id)
        {
            People = new Repository(Request.PhysicalApplicationPath + "/Content/users.xml");
            return View(People.getPerson(id));
        }

        [HttpPost]
        public ActionResult edit(Person person)
        {
            People = new Repository(Request.PhysicalApplicationPath + "/Content/users.xml");
            People.updatePerson(person);
            return Redirect("Index");
        }

        public ActionResult delete(int id = 0)
        {
            People = new Repository(Request.PhysicalApplicationPath + "/Content/users.xml");
            People.delPerson(id);
            return RedirectToAction("Index");
        }

    }
}