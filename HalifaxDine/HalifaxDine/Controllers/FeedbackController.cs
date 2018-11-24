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
        // add authorization to attender
        [Authorize(Roles = "Admin,Mangaer,Attender")]
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

            if (model.Feedback_Comment == null)
                return View();
            ClientModel client = dao.GetClientRow(Account_Id);
            model.Client_Id = client.Client_Id;

            ViewBag.Result = dao.InsertFeedbackRow(model);
            return View();
        }
    }
}