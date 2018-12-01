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
        [Authorize(Roles = "HeadManager,Admin")]
        public ActionResult BranchInfo()
        {
            IEnumerable<BranchModel> model = dao.GetBranchData();
            return View(model);
        }

        // GET: Branch
        public ActionResult SelectBranch(BranchModel model)
        {
            if (model.Branch_Province == null || model.Branch_Province == "")
            {
                IEnumerable<BranchModel> branchData = dao.GetBranchData();
                return View(branchData);
            }

            HttpCookie myCookie = new HttpCookie("UserSettings");
            myCookie["BranchId"] = model.Branch_Id.ToString();
            Response.Cookies.Add(myCookie);
            return RedirectToAction("Index","home");
        }

        // update branch info
        [Authorize(Roles = "Admin,,HeadManager")]
        public ActionResult Initialize(BranchModel model)
        {
            return View(model);
        }

        // Update Branch Info
        [Authorize(Roles = "Admin,HeadManager")]
        public ActionResult InitiazlizeBranch(BranchModel model)
        {
            bool success = dao.UpdateBranch(model);
            return RedirectToAction("Initialize", model);

        }
    }
}