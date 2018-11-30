using HalifaxDine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace HalifaxDine.Controllers
{
    public class EmployeeController : Controller
    {
        DatabaseAccess dao;

        public EmployeeController()
        {
            dao = new DatabaseAccess();
        }
        // GET: Employee
    [Authorize(Roles ="HeadManager,BranchManager,Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "HeadManager,BranchManager,Admin")]
        public ActionResult GetEmployeeInfo(int Employee_Id)
        {
            EmployeeModel model = dao.GetEmployeeData(null, Employee_Id).FirstOrDefault();
            return View(model);
        }

        [Authorize(Roles = "HeadManager,BranchManager,Admin")]
        public ActionResult GetEmployeeList()
        {
            IEnumerable<EmployeeModel> model = dao.GetEmployeeData().OrderBy(x=>x.Branch_Id).ThenBy(x=>x.Privilege_Id);
            return View(model);
        }


        [Authorize(Roles ="HeadManager,BranchManager,Admin")]
        public ActionResult EditEmployee(EmployeeModel model)
        {
            dao.UpdateEmployee(model);
            return RedirectToAction("GetEmployeeInfo", new { Employee_Id = model.Emp_Id });
        }

    [Authorize(Roles ="HeadManager,BranchManager,Admin,Attender,Chef")]
        public ActionResult GetSelf()
        {
            string Account_Id = User.Identity.GetUserId();

            return View(dao.GetEmployeeRow(Account_Id));
        }
    }
}