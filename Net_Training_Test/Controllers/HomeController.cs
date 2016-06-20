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


        //Main index page
        //if param sort = 0 then no sort
        //sort = 1 - sorting by Surname
        //sort = 2 - sorting by YearBorn
        [HttpGet]
        public ActionResult Index(int sort = 0)
        {
            //Loading file with data
            //Request.PhysicalApplicationPath - Physical Application Path
            //because for xmlloader required absolute path or url
            People = new Repository(Request.PhysicalApplicationPath + "/Content/users.xml");
            //View list of people
            return View(People.getList(sort));
        }
        
        //View edit form for add person 
        [HttpGet]
        public ActionResult create()
        {     
            return View(new Person());
        }

        //Receives and processes the post request of create person
        [HttpPost]
        public ActionResult create(Person person)
        {
            //Loading file with data
            People = new Repository(Request.PhysicalApplicationPath + "/Content/users.xml");
            //Adding person to file data
            People.addPerson(person);
            //Redirect to main list 
            return RedirectToAction("Index");
        }

        //View edit form for add the person 
        [HttpGet]
        public ActionResult edit(string id)
        {
            //Loading file with data
            People = new Repository(Request.PhysicalApplicationPath + "/Content/users.xml");
            //View edit form for edit the person
            return View(People.getPerson(id));
        }

        //Receives and processes the post request of edit person
        [HttpPost]
        public ActionResult edit(Person person)
        {
            //Loading file with data
            People = new Repository(Request.PhysicalApplicationPath + "/Content/users.xml");
            //Updating person to file data
            People.updatePerson(person);
            //Redirect to main list 
            return RedirectToAction("Index");
        }

        public ActionResult delete(int id = 0)
        {
            //Loading file with data
            People = new Repository(Request.PhysicalApplicationPath + "/Content/users.xml");
            //Deleting person from file data
            People.delPerson(id);
            //Redirect to main list 
            return RedirectToAction("Index");
        }


        //Searching by Surname, Name and Phone
        [HttpPost]
        public ActionResult Index(string Surname, string Name, string Phone)
        {
            //Loading file with data
            People = new Repository(Request.PhysicalApplicationPath + "/Content/users.xml");
            //View list of found people
            return View(People.searchPerson(Surname, Name, Phone));
        }

    }
}