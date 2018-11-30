using HalifaxDine.Models;
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
        private string[] months = new string [] { "Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"};


        public BIController()
        {
            dao = new DatabaseAccess();
        }
        // GET: BI
        public ActionResult Popularity()
        {
            return View();
        }

        public ActionResult MonthlyPopularityData()
        {

            IEnumerable<PopularityModel> model = dao.GetBranchMonthlyPopularity();

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

        public ActionResult OverallPopularityData()
        {

            IEnumerable<PopularityModel> model = dao.GetBranchPopularity().OrderBy(x => x.Branch_Province);

            PopularityViewModel result = new PopularityViewModel();

            result.x = model.Select(x => x.Branch_Province).ToArray<string>();
            result.y = model.Select(x => x.Avg_Rating).ToArray<float>();

            //result needs to be in an array for the JS library
            return Json(new PopularityViewModel[] { result }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ItemPopularity()
        {

            IEnumerable<PopularityModel> model = dao.GetBranchItemPopularity();

            PopularityViewModel result = new PopularityViewModel();

            result.x = model.Select(x => x.Menu_Desc).ToArray<string>();
            result.y = model.Select(x => (float)x.Item_Count).ToArray<float>();

            //result needs to be in an array for the JS library
            return Json(new PopularityViewModel[] { result }, JsonRequestBehavior.AllowGet);
        }
    }
}