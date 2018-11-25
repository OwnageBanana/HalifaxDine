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


        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Create
        public ActionResult Order(MenuItemModel item)
        {
            string account_id = User.Identity.GetUserId();

            var transaction = dao.getclientTransaction(account_id);

            ViewBag.Trans_Id = transaction.Trans_Id;

            if (item.Menu_Id == 0)
                return View(dao.GetClientOrder(account_id));

            bool orderCreated = true;

            TransactionModel clientOrder = dao.getclientTransaction(account_id);
            //if an order isn't created yet, create the order
            if (clientOrder == null)
            {
                int branch_id = -1;
                bool exists = false;
                //get the cookie for the branch selected
                if (Request.Cookies["UserSettings"] != null)
                {
                    string cookieId= Request.Cookies["UserSettings"]["BranchId"];
                    exists = int.TryParse(cookieId, out branch_id);
                }
                if (!exists)
                {
                    return RedirectToAction("SelectBranch", "Branch");
                }
                ClientModel client = dao.GetClientRow(account_id);
                clientOrder = new TransactionModel { Client_Id = client.Client_Id, Branch_Id = branch_id, Trans_Date = DateTime.Today.Date, Trans_Status = TransactionStatus.UNPAID };
                orderCreated = dao.InsertClientOrderRow(clientOrder);
            }
            //add the menu item to the oder


            dao.InsertClientOrderItemRow(transaction.Trans_Id, item.Menu_Id);

            ViewBag.Trans_Id = transaction.Trans_Id;

            return View(dao.GetClientOrder(account_id));
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(TransactionModel collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult PayOrder(int Trans_Id)
        {

            dao.CloseClientOrder(Trans_Id);
            return RedirectToAction("index", "Home");
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
