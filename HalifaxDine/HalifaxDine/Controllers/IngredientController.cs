using HalifaxDine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HalifaxDine.Controllers
{
        [Authorize(Roles ="Admin, Chef")]
    public class IngredientController : Controller
    {
        DatabaseAccess dao;
        public IngredientController()
        {
            dao = new DatabaseAccess();
        }

        // GET: Ingredient_Info
        public ActionResult IngredientInfo()
        {
            var model = dao.GetIngredientData();
            return View(model);
        }
    }
}