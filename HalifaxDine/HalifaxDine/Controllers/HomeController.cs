using HalifaxDine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HalifaxDine.Controllers
{
    [Authorize()]
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }


            DatabaseAccess db = new DatabaseAccess();

            var t = db.GetCustomerData();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize(Roles="Admin")]
        public ActionResult AdminFunction()
        {
            ViewBag.Message = "Function page.";

            return RedirectToAction("AdminRegister", "Account", null);
        }

        public ActionResult ManagerFunction()
        {
            ViewBag.Message = "Function page.";

            return View();
        }
        public ActionResult ClientFunction()
        {
            ViewBag.Message = "Function page.";

            return View();
        }
        public ActionResult ChefFunction()
        {
            ViewBag.Message = "Function page.";

            return View();
        }

    }
}