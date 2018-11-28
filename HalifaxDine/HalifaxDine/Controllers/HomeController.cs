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
        //head mangager function
        [Authorize(Roles = "HeadManager")]
        public ActionResult HeadManagerFunction()
        {
            ViewBag.Message = "HeadManager Function page.";
            //compare result of all restaurant
            return RedirectToAction("BranchInfo", "Branch", null);
        }
        [Authorize(Roles = "Client")]
        public ActionResult ClientFunction()
        {
            ViewBag.Message = "Client Function page.";
            //view menu and place order
            return RedirectToAction("MenuInfo", "Menu", null);
        }
        [Authorize(Roles = "Chef")]
        public ActionResult ChefFunction()
        {
            ViewBag.Message = " Chef Function page.";
            //collect ingredient and prepare food
            return RedirectToAction("IngredientInfo", "Ingredient", null);
        }
        //branch mangager function
        [Authorize(Roles = "BranchManager")]
        public ActionResult BranchManagerFunction()
        {
            ViewBag.Message = "BranchMangager Function page.";
            //branch manager should assign to individual branch  
            return RedirectToAction("SelectBranch", "Branch", null);
        }
        [Authorize(Roles = "Attender")]
        public ActionResult AttenderFunction()
        {   
            ViewBag.Message = "Attender Function page.";
            //collect feedback 
            return RedirectToAction("Index", "Feedback", null);
        }

    }
}