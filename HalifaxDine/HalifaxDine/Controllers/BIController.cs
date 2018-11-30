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

        public ActionResult PopularityData()
        {

            IEnumerable<PopularityModel> model = dao.GetBranchMonthlyPopularity();
            var monthDict = model.GroupBy(x => x.Month, x => x, (key, g) => new { Month = key, vals = g.ToList() }).OrderBy(x=>x.Month);

            List<PopularityViewModel> result = new List<PopularityViewModel>();
            foreach (var group in monthDict)
            {
                PopularityViewModel tmp = new PopularityViewModel();
                tmp.name = months[group.Month-1];
                tmp.x = group.vals.Select(x=>x.Branch_Province).ToArray();
                tmp.y = group.vals.Select(x=>x.Avg_Rating).ToArray();

                result.Add(tmp);
            }
            //model.Add("Overall", dao.GetBranchPopularity().ToList());
            //model.Add("ItemPopularity", dao.GetBranchItemPopularity().ToList());

            return Json(result.ToArray<PopularityViewModel>(), JsonRequestBehavior.AllowGet);
        }
    }
}