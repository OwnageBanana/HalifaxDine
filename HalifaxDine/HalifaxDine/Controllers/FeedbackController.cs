using HalifaxDine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace HalifaxDine.Controllers
{
    public class FeedbackController : Controller
    {
        DatabaseAccess dao;
        public FeedbackController()
        {
            dao = new DatabaseAccess();
        }
        // GET: Feedback
        [Authorize(Roles = "Admin,Mangaer")]
        public ActionResult Index()
        {
            var model = dao.GetFeedbackData();
            return View(model);
        }

        // GET: Feedback
        [Authorize(Roles = "Client")]
        public ActionResult SubmitFeedback(FeedbackModel model)
        {
            var Account_Id = User.Identity.GetUserId();

            bool exists = false;
            int branch_id = -1;
            if (Request.Cookies["UserSettings"] != null)
            {
                string cookieId = Request.Cookies["UserSettings"]["BranchId"];
                exists = int.TryParse(cookieId, out branch_id);
            }
            if (!exists)
            {
                return RedirectToAction("SelectBranch", "Branch");
            }


            if (model.Feedback_Comment == null)
                return View();


            ClientModel client = dao.GetClientRow(Account_Id);
            model.Client_Id = client.Client_Id;
            model.Branch_Id = branch_id;
            model.DateTime = DateTime.Now;

            ViewBag.Result = dao.InsertFeedbackRow(model);
            return RedirectToAction("Index","Home", null);
        }
    }
}