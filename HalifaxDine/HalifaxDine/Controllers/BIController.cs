using HalifaxDine.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HalifaxDine.Controllers
{
    public class BIController : Controller
    {

        DatabaseAccess dao;
        private string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };


        public BIController()
        {
            dao = new DatabaseAccess();
        }
        // GET: BI
        [Authorize(Roles = "HeadManager,Admin")]
        public ActionResult Popularity()
        {
            return View();
        }

        [Authorize(Roles = "BranchManager,Admin")]
        public ActionResult BranchPopularity()
        {
            int branch_id = -1;
            EmployeeModel employee = dao.GetEmployeeRow(User.Identity.GetUserId());
            branch_id = employee.Branch_Id;
            //get the cookie for the branch selected
            if (Request.Cookies["UserSettings"] != null && employee.Privilege_Id == (int)Role.Admin)
            {
                string cookieId = Request.Cookies["UserSettings"]["BranchId"];
                int.TryParse(cookieId, out branch_id);
            }
            ViewBag.Branch_Id = branch_id;
            return View();
        }

        [Authorize(Roles = "HeadManager,Admin")]
        public ActionResult Sales()
        {
            return View();
        }

        [Authorize(Roles = "BranchManager,Admin")]
        public ActionResult BranchSales()
        {
            int branch_id = -1;
            EmployeeModel employee = dao.GetEmployeeRow(User.Identity.GetUserId());
            branch_id = employee.Branch_Id;
            //get the cookie for the branch selected
            if (Request.Cookies["UserSettings"] != null && employee.Privilege_Id == (int)Role.Admin)
            {
                string cookieId = Request.Cookies["UserSettings"]["BranchId"];
                int.TryParse(cookieId, out branch_id);
            }
            ViewBag.Branch_Id = branch_id;
            return View();
        }


        public ActionResult MonthlyPopularityData(int? Branch_Id = null)
        {

            IEnumerable<PopularityModel> model = dao.GetBranchMonthlyPopularity();

            if (Branch_Id != null)
                model = model.Where(x => x.Branch_id == Branch_Id);

            //this makes a dictionary where month int is the key and the val is the list of objects which share that month val.
            var monthDict = model.GroupBy(x => x.Month, x => x, (key, g) => new { Month = key, vals = g.ToList() }).OrderBy(x => x.Month);

            List<PopularityViewModel> result = new List<PopularityViewModel>();
            //converting the month data into the viewmodel for the chart
            foreach (var group in monthDict)
            {
                PopularityViewModel tmp = new PopularityViewModel();
                tmp.name = months[group.Month - 1];
                tmp.x = group.vals.Select(x => x.Branch_Province).ToArray();
                tmp.y = group.vals.Select(x => x.Avg_Rating).ToArray();

                result.Add(tmp);
            }

            return Json(result.ToArray<PopularityViewModel>(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult OverallPopularityData(int? Branch_Id = null)
        {

            IEnumerable<PopularityModel> model = dao.GetBranchPopularity().OrderBy(x => x.Branch_Province);

            if (Branch_Id != null)
                model = model.Where(x => x.Branch_id == Branch_Id);

            PopularityViewModel result = new PopularityViewModel();

            result.x = model.Select(x => x.Branch_Province).ToArray<string>();
            result.y = model.Select(x => x.Avg_Rating).ToArray<float>();

            //result needs to be in an array for the JS library
            return Json(new PopularityViewModel[] { result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ItemPopularity(int? Branch_Id = null)
        {

            IEnumerable<PopularityModel> model = dao.GetBranchItemPopularity();

            if (Branch_Id != null)
                model = model.Where(x => x.Branch_id == Branch_Id);

            PopularityViewModel result = new PopularityViewModel();

            result.x = model.Select(x => x.Menu_Desc).ToArray<string>();
            result.y = model.Select(x => (float)x.Item_Count).ToArray<float>();

            //result needs to be in an array for the JS library
            return Json(new PopularityViewModel[] { result }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult MonthlySalesData(int? Branch_Id = null)
        {

            IEnumerable<SalesModel> model = dao.GetMonthlySalesTotal();

            if (Branch_Id != null)
                model = model.Where(x => x.Branch_Id == Branch_Id);

            //this makes a dictionary where month int is the key and the val is the list of objects which share that month val.
            var monthDict = model.GroupBy(x => x.Month, x => x, (key, g) => new { Month = key, vals = g.ToList() }).OrderBy(x => x.Month);

            List<SalesViewModel> result = new List<SalesViewModel>();
            //converting the month data into the viewmodel for the chart
            foreach (var group in monthDict)
            {
                SalesViewModel tmp = new SalesViewModel();
                tmp.name = months[group.Month - 1];
                tmp.x = group.vals.Select(x => x.Branch_Province).ToArray();
                tmp.y = group.vals.Select(x => x.Sales).ToArray();

                result.Add(tmp);
            }

            return Json(result.ToArray<SalesViewModel>(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult OverallSalesData(int? Branch_Id = null)
        {

            IEnumerable<SalesModel> model = dao.GetSalesTotal().OrderBy(x => x.Branch_Province);

            if (Branch_Id != null)
                model = model.Where(x => x.Branch_Id == Branch_Id);

            SalesViewModel result = new SalesViewModel();

            result.x = model.Select(x => x.Branch_Province).ToArray<string>();
            result.y = model.Select(x => x.Sales).ToArray<float>();

            //result needs to be in an array for the JS library
            return Json(new SalesViewModel[] { result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ItemRevenue(int? Branch_Id = null)
        {

            IEnumerable<SalesModel> model = dao.GetitemRevenueData();

            if (Branch_Id != null)
                model = model.Where(x => x.Branch_Id == Branch_Id);

            SalesViewModel result = new SalesViewModel();

            result.x = model.Select(x => x.Menu_Name).ToArray<string>();
            result.y = model.Select(x => (float)x.Sales).ToArray<float>();

            //result needs to be in an array for the JS library
            return Json(new SalesViewModel[] { result }, JsonRequestBehavior.AllowGet);
        }
    }
}