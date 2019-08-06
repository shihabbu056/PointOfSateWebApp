using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PointOfSaleManagement.Bll.Bll;
using PointOfSaleManagement.Models.Models;
using PointOfSaleManagementApp.ViewModels;

namespace PointOfSaleManagementApp.Controllers
{
    public class CategoryController : Controller
    {
        CategoryManager _categoryManager = new CategoryManager();
        //private ICategoryManager _categoryManagerInterface;
        Category _category = new Category();

        //public CategoryController(ICategoryManager categoryInterface, CategoryManager categoryManager)
        //{
        //    this._categoryManagerInterface = categoryInterface;
        //    this._categoryManager = categoryManager;
        //}
        // GET: Category
        public ActionResult Index()
        {
            return View(_categoryManager.GetAll());
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id, Name, Code, IsDeleted")]CategoryViewModel categoryVm)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category();
                category.Id = categoryVm.Id;
                category.Name = categoryVm.Name;
                category.Code = categoryVm.Code;

                if (_categoryManager.Add(category))
                {
                    ViewBag.SuccessMsg = "Category Saved Successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.FailMsg = "Vailed!";
                }
            }
            else
            {
                ViewBag.FailMsg = "Validation Error!";
            }

            return View();
        }
        public ActionResult Edit(int id)
        {
            _category.Id = id;
            var category = _categoryManager.GetByID(_category);
            CategoryViewModel categoryVm = new CategoryViewModel();
            categoryVm.Id = category.Id;
            categoryVm.Name = category.Name;
            categoryVm.Code = category.Code;
            return View(categoryVm);
        }
        [HttpPost]
        public ActionResult Edit(CategoryViewModel categoryVm)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category();
                category.Id = categoryVm.Id;
                category.Name = categoryVm.Name;
                category.Code = categoryVm.Code;
                if (_categoryManager.Update(category))
                {
                    ViewBag.SuccessMsg = "Update Successfully.";
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ViewBag.FailMsg = "Update Vailed!";
                }
            }
            else
            {
                ViewBag.FailMsg = "Validation Error!";
            }

            return View(categoryVm);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _category.Id = (int)id;
            var category = _categoryManager.GetByID(_category);
            //Student student = db.Students.Find(id);
            var categoryVm = new CategoryViewModel();
            categoryVm.Id = category.Id;
            categoryVm.Name = category.Name;
            categoryVm.Code = category.Code;
            if (categoryVm == null)
            {
                return HttpNotFound();
            }
            return View(categoryVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _category.Id = id;
            _categoryManager.Delete(_category);
            return RedirectToAction("Index");
        }
        //public JsonResult IsCategoryNameExist(string categoryName)
        //{
        //    POSManagementSystemBdContext dbpri = new POSManagementSystemBdContext();
        //    Category cat = dbpri.Categories.Find(categoryName);
        //    if (cat == null)
        //    {
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //    return Json("Category Already Exist, Try Another", JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult Delete(int id, FormCollection formCollection)
        //{
        //    try
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}