using HalifaxDine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HalifaxDine.Controllers
{
        [Authorize(Roles ="Admin, Chef")]
    public class MenuIngredientController : Controller
    {
        DatabaseAccess dao;
        public MenuIngredientController()
        {
            dao = new DatabaseAccess();
        }

        //GET: menu_ingredient item
    public ActionResult MenuIngredientInfo()
    {
        var model = dao.GetMenuIngredientData();
        return View(model);
    }
}
}