using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PointOfSaleManagement.Bll.Bll;
using PointOfSaleManagement.Models.Models;
using PointOfSaleManagementApp.ViewModels;

namespace PointOfSaleManagementApp.Controllers
{
    public class SupplierController : Controller
    {
        SupplierManager _supplierManager = new SupplierManager();
        // GET: Supplier
        public ActionResult Index()
        {
            var suppliers = _supplierManager.GetAll();
            List<SupplierViewModel> supplierVm = new List<SupplierViewModel>();
            foreach (var supplierPersion in suppliers)
            {
                var supplier = new SupplierViewModel();
                supplier.Id = supplierPersion.Id;
                supplier.Name = supplierPersion.Name;
                supplier.Code = supplierPersion.Code;
                supplier.Contact = supplierPersion.Contact;
                supplier.ContactPerson = supplierPersion.ContactPerson;
                supplier.Address = supplierPersion.Address;
                supplier.Image = supplierPersion.Image;
                supplierVm.Add(supplier);
            }
            return View(supplierVm);
        }
        public static string GetFileNameToSave(string s)
        {
            return s
                .Replace("\\", "")
                .Replace("/", "")
                .Replace("\"", "")
                .Replace("*", "")
                .Replace(":", "")
                .Replace("?", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("|", "");
        }
        private byte[] GetImageData(string imgName)
        {
            byte[] imageData = null;

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase imgFile = Request.Files["Image"];
                if (imgFile != null && imgFile.ContentLength > 0)
                {
                    var fileName = GetFileNameToSave(imgName + DateTime.Now);
                    imgFile.SaveAs(Server.MapPath("~/images/supplier/" + fileName));
                    //imgFile.SaveAs(Server.MapPath(Path.Combine()));

                    using (var binary = new BinaryReader(imgFile.InputStream))
                    {
                        imageData = binary.ReadBytes(imgFile.ContentLength);
                    }
                }
            }
            return imageData;
        }
        public bool HasFile(byte[] file)
        {

            return file != null;
        }
        private void CreateSupplier(SupplierViewModel supplierVm, byte[] imageData)
        {
            Supplier cust = new Supplier
            {
                Name = supplierVm.Code,
                Code = supplierVm.Name,
                Address = supplierVm.Address,
                Email = supplierVm.Email,
                Contact = supplierVm.Contact,
                ContactPerson = supplierVm.ContactPerson,
                Image = imageData,
                ImagePath = "~/images/supplier/" + supplierVm.ImagePath + DateTime.Now,
                IsDeleted = false
            };
            _supplierManager.Add(cust);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add([Bind(Exclude = "Image")] SupplierViewModel supplierVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var imageData = GetImageData(supplierVm.Name);
                    if (HasFile(imageData))
                    {
                        CreateSupplier(supplierVm, imageData);
                        ViewBag.SuccessMsg = "Customer Saved Successfully.";
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
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Supplier supplier)
        {
            return View();
        }
    }
}