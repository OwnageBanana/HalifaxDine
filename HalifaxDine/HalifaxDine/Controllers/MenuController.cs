using HalifaxDine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HalifaxDine.Controllers
{
    public class MenuController : Controller
    {

        DatabaseAccess dao;
        public MenuController()
        {
            dao = new DatabaseAccess();
        }

        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles="Client,Attender")]
        public ActionResult MenuInfo()
        {
            var model = dao.GetMenuData();
            return View(model);
        }

        [Authorize(Roles="Admin,BranchManager,HeadManager,Chef")]
        public ActionResult EditMenuItems()
        {
            var model = dao.GetMenuData();
            return View(model);
        }

        [Authorize(Roles = "Admin,BranchManager,HeadManager,Chef")]
        public ActionResult Create()
        {
                return View();
        }

        [Authorize(Roles = "Admin,BranchManager,HeadManager,Chef")]
        public ActionResult Add(MenuItemModel model)
        {
            bool success = dao.InsertMenuItemRow(model);

            return RedirectToAction("EditMenuItems");
        }

        [Authorize(Roles = "Admin,BranchManager,HeadManager,Chef")]
        public ActionResult Delete(MenuItemModel model)
        {
            bool success = dao.DeleteMenuItemRow(model);
            return RedirectToAction("EditMenuItems");
        }

        [Authorize(Roles = "Admin,BranchManager,HeadManager,Chef")]
        public ActionResult Update(MenuItemModel model)
        {
            return View(model);
        }

        [Authorize(Roles = "Admin,BranchManager,HeadManager,Chef")]
        public ActionResult UpdateInfo(MenuItemModel model)
        {
            bool success = dao.UpdateMenuItemRow(model);
            return RedirectToAction("Update", model );

        }
    }
}