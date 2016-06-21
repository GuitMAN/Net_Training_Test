using Net_Training_Test.Models;
using System.Web.Mvc;


namespace Net_Training_Test.Controllers
{
    public class HomeController : Controller
    {
        private Repository People;
        private void Loader()
        {   //Loading file with data
            //Request.PhysicalApplicationPath - Physical Application Path
            //because for xmlloader required absolute path or url
            People = new Repository(Request.PhysicalApplicationPath + "/Content/users.xml");
        }


        //Main index page
        //if param sort = 0 then no sort
        //sort = 1 - sorting by Surname
        //sort = 2 - sorting by YearBorn
        [HttpGet]
        public ActionResult Index(int sort = 0)
        {
            Loader();
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
            //If the entered data is incorrect, then go back to fix
            if (!ModelState.IsValid)
                return View(person);

            //Loading file with data
            Loader();

            //Adding person to file data
            People.addPerson(person);
            //Redirect to main list 
            return RedirectToAction("Index");
        }

        //View edit form for add the person 
        [HttpGet]
        public ActionResult edit(string id = "")
        {
            //Loading file with data
            Loader();
            //View edit form for edit the person
            return View(People.getPerson(id));
        }

        //Receives and processes the post request of edit person
        [HttpPost]
        public ActionResult edit(Person person)
        {
            //If the entered data is incorrect, then go back to fix
            if (!ModelState.IsValid)
                return View(person);

            //Loading file with data
            Loader();
            //Updating person to file data
            People.updatePerson(person);
            //Redirect to main list 
            return RedirectToAction("Index");
        }

        public ActionResult delete(string id = "")
        {
            //Loading file with data
            Loader();
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
            Loader();
            //View list of found people
            return View(People.searchPerson(Surname, Name, Phone));
        }

    }
}