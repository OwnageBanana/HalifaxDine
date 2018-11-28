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
            Dictionary<string, List<PopularityModel>> model = new Dictionary<string, List<PopularityModel>>();

            model.Add("Monthly", dao.GetBranchMonthlyPopularity().ToList());
            model.Add("Overall", dao.GetBranchPopularity().ToList());
            model.Add("ItemPopularity", dao.GetBranchItemPopularity().ToList());

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}