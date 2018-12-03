using HalifaxDine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace HalifaxDine.Controllers
{
    public class OrderController : Controller
    {
        DatabaseAccess dao;

        public OrderController()
        {
            dao = new DatabaseAccess();
        }


        [Authorize(Roles = "Attender,Chef")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Order/Create
        [Authorize(Roles = "Client, Attender,Chef")]
        public ActionResult Order(MenuItemModel item, int? Client_Id = null)
        {
            string account_id = User.Identity.GetUserId();

            bool orderCreated = true;
            int branch_id = -1;
            bool branchIdSet = false;

            var employee = dao.GetEmployeeRow(account_id);

            TransactionModel clientOrder = null;
            if (employee != null)
            {
                if (Client_Id == null)
                {
                    return RedirectToAction("Index");
                }

                var client = dao.GetClientRow(Client_Id.Value);
                if (client == null)
                {
                    ViewBag.Error = "Client Id was not found";
                    return RedirectToAction("Index");
                }
                account_id = client.Account_Id;
                clientOrder = dao.getclientTransaction(account_id);
                branch_id = employee.Branch_Id;
                branchIdSet = true;
                ViewBag.Client_Id = client.Client_Id;
            }
            else
            {
                clientOrder = dao.getclientTransaction(account_id);
            }

            //get the cookie for the branch selected
            if (Request.Cookies["UserSettings"] != null)
            {
                string cookieId = Request.Cookies["UserSettings"]["BranchId"];
                branchIdSet = int.TryParse(cookieId, out branch_id);
            }
            if (!branchIdSet)
            {
                return RedirectToAction("SelectBranch", "Branch");
            }

            //if an order isn't created yet, create the order
            if (clientOrder == null)
            {
                clientOrder = new TransactionModel();


                ClientModel client = dao.GetClientRow(account_id);
                clientOrder = new TransactionModel { Client_Id = client.Client_Id, Branch_Id = branch_id, Trans_Date = DateTime.Today.Date, Trans_Status = TransactionStatus.UNPAID };
                orderCreated = dao.InsertClientOrderRow(clientOrder);
            }

            var transaction = dao.getclientTransaction(account_id);


            ViewBag.Tax = dao.GetBranchData(branch_id).FirstOrDefault()?.Branch_Tax;
            ViewBag.Trans_Id = transaction.Trans_Id;

            ViewBag.MenuInfo = dao.GetMenuData();

            if (item.Menu_Id == 0)
                return View(dao.GetClientOrder(account_id));


            dao.InsertClientOrderItemRow(transaction.Trans_Id, item.Menu_Id);

            return View(dao.GetClientOrder(account_id));
        }


        [Authorize(Roles = "Client,Attender,Chef")]
        public ActionResult Menu()
        {
            return PartialView("_MenuPartial",dao.GetMenuData());
        }


        [Authorize(Roles = "Client,Attender,Chef")]
        public ActionResult PayOrder(int Trans_Id)
        {

            dao.CloseClientOrder(Trans_Id);
            return RedirectToAction("index", "Home");
        }

        [Authorize(Roles = "Client,Attender,Chef")]
        public ActionResult Delete(int Trans_Id, int Menu_Id)
        {

            dao.DeleteClientItemRow(Trans_Id, Menu_Id);
            return RedirectToAction("order", "order");
        }
    }
}
