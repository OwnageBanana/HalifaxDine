using HalifaxDine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HalifaxDine.Controllers
{
    public class HomeController : Controller
    {
        DatabaseAccess dao;
        public HomeController()
        {
            dao = new DatabaseAccess();
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Halifax dine is an atlantic dining experience! we serve" +
                "some of the freshest food that atlantic canada has to offer for the whole family.";
            return View();
        }

        [Authorize(Roles = "Client")]
        public ActionResult Contact()
        {

            bool exists = false;
            int branch_id = -1;
            if (Request.Cookies["UserSettings"] != null)
            {
                string cookieId = Request.Cookies["UserSettings"]["BranchId"];
                exists = int.TryParse(cookieId, out branch_id);
            }
            if (!exists)
            {
                return RedirectToAction("SelectBranch", "Branch");
            }

            BranchModel model = dao.GetBranchData(branch_id).FirstOrDefault();

            return View(model);
        }

        [Authorize(Roles="Admin")]
        public ActionResult AdminFunction()
        {
            ViewBag.Message = "Function page.";

            return RedirectToAction("AdminRegister", "Account", null);
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
            //collect ingredient
            return RedirectToAction("IngredientInfo", "Ingredient", null);
        }
        [Authorize(Roles = "Chef")]
        public ActionResult ChefFunction_Menu()
        {
            ViewBag.Message = " Chef Function page.";
            //collect ingredient list in the menu
            return RedirectToAction("MenuIngredientInfo", "MenuIngredient", null);
        }

        [Authorize(Roles = "Attender")]
        public ActionResult AttenderFunction()
        {
            ViewBag.Message = "Attender Function page.";
            //collect feedback
            return RedirectToAction("Index", "Feedback", null);
        }

        //branch mangager function
        [Authorize(Roles = "BranchManager")]
        public ActionResult BranchManagerFunction()
        {
            ViewBag.Message = "BranchMangager Function page.";
            //branch manager should assign to individual branch
            return RedirectToAction("SelectBranch", "Branch", null);
        }

        //head mangager function
        [Authorize(Roles = "HeadManager")]
        public ActionResult HeadManagerFunction()
        {
            ViewBag.Message = "HeadManager Function page.";
            //compare result of all restaurant
            return RedirectToAction("BranchInfo", "Branch", null);
        }
    }
}