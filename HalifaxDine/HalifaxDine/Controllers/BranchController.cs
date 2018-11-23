using HalifaxDine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HalifaxDine.Controllers
{
    public class BranchController : Controller
    {
        DatabaseAccess dao;
        public BranchController()
        {

            dao = new DatabaseAccess();
        }
        // GET: Branch
        public ActionResult Index()
        {
            return View();
        }

        // GET: Branch
        public ActionResult BranchInfo()
        {
            var model = dao.GetBranchData();
            return View(model);
        }
    }
}