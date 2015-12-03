using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jitter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(); //dec_3 branch. View is a thing called View Bag, kind of like a grocery bag, where this stuff is put in here when its returned. Go to Views folder, find the home subfolder and look at the about file and you will see where they use @ViewBag.
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            //1.Create or Get a list of things
            List<string> my_list_of_things = new List<string>();
            my_list_of_things.Add("Timmy");
            my_list_of_things.Add("Chef");
            my_list_of_things.Add("Greg");

            //One way to send the my_list_things to the view is: return View(my_list_of_things);

            return View(my_list_of_things);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}