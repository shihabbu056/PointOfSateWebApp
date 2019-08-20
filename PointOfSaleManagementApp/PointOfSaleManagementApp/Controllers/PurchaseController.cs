using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PointOfSaleManagement.Bll.Bll;
using PointOfSaleManagement.Models.Models;
using PointOfSaleManagementApp.ViewModels;

namespace PointOfSaleManagementApp.Controllers
{
    public class PurchaseController : Controller
    {
        CategoryManager _categoryManager = new CategoryManager();
        SupplierManager _supplierManager = new SupplierManager();
        PurchaseManager _purchaseManager = new PurchaseManager();

        [HttpGet]
        public ActionResult PurchaseIn()
        {
            var model = new PurchaseViewModel();
            model.Categories = _categoryManager.GetAll();
            model.Suppliers = _supplierManager.GetAll();
            ViewBag.ProductDropDown = new SelectListItem[] {new SelectListItem() {Value = "", Text = "Select..."}};
            return View(model);
        }
        [HttpPost]
        public ActionResult PurchaseIn(PurchaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                Purchase purchase = Mapper.Map<Purchase>(model);
                _purchaseManager.Add(purchase);
                ViewBag.SuccessMsg = "Purchase Saved Successfylly.";
            }
            else
            {
                ViewBag.FailMsg = "Validation Error!";
            }

            model.Categories = _categoryManager.GetAll();
            model.Suppliers = _supplierManager.GetAll();
            ViewBag.ProductDropDown = new SelectListItem[] {new SelectListItem() {Value = "", Text = "Select..."}};
            return View(model);
        }
        public JsonResult GetByPurchaseDetail(int? productId)
        {
            if (productId == null)
            {
                return null;
            }

            var purchaseDetail = _purchaseManager.GetByPurchaseDetail(productId);
            return Json(purchaseDetail, JsonRequestBehavior.AllowGet);
        }
    }
}