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
        private Repository People;

        [HttpGet]
        public ActionResult Index(int sort = 0)
        {
            People = new Repository(Request.PhysicalApplicationPath + "/Content/users.xml");
            return View(People.getList(sort));
        }

        [HttpGet]
        public ActionResult create()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult create(Person person)
        {
            People = new Repository(Request.PhysicalApplicationPath + "/Content/users.xml");
            People.addPerson(person);
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }

        public ActionResult delete(int id = 0)
        {
            People = new Repository(Request.PhysicalApplicationPath + "/Content/users.xml");
            People.delPerson(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(string Surname, string Name, string Phone)
        {
            People = new Repository(Request.PhysicalApplicationPath + "/Content/users.xml");

            return View(People.searchPerson(Surname, Name, Phone));
        }

    }
}