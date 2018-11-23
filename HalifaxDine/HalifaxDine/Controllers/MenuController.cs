using HalifaxDine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HalifaxDine.Controllers
{
    public class MenuController : Controller
    {

        DatabaseAccess dao;
        public MenuController()
        {
            dao = new DatabaseAccess();
        }

        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuInfo()
        {
            var model = dao.GetMenuData();
            return View(model);
        }
    }
}