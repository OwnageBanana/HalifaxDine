using HalifaxDine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HalifaxDine.Controllers
{
        [Authorize(Roles ="Admin, Manager")]
    public class RoleController : Controller
    {
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var Roles = context.Roles.ToList();
            return View(Roles);

        }
    }
}