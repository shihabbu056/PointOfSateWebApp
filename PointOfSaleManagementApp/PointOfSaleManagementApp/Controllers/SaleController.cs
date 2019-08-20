using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PointOfSaleManagementApp.ViewModels;

namespace PointOfSaleManagementApp.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SaleOut()
        {
            ViewBag.ProductDropDown = new SelectListItem[] { new SelectListItem() { Value = "", Text = "Select..." } };
            return View();
        }
        [HttpPost]
        public ActionResult SaleOut(SaleViewModel saleViewModel)
        {
            return View();
        }
    }
}