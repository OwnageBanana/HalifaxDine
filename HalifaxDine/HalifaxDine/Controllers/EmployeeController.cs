using HalifaxDine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HalifaxDine.Controllers
{
    [Authorize(Roles ="HeadManager,BranchManager,Admin")]
    public class EmployeeController : Controller
    {
        DatabaseAccess dao;

        public EmployeeController()
        {
            dao = new DatabaseAccess();
        }
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEmployeeInfo(int Employee_Id)
        {
            EmployeeModel model = dao.GetEmployeeData(null, Employee_Id).FirstOrDefault();
            return View(model);
        }

        public ActionResult EditEmployee(EmployeeModel model)
        {
            dao.UpdateEmployee(model);
            return RedirectToAction("GetEmployeeInfo", new { Employee_Id = model.Emp_Id });
        }
    }
}