using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PointOfSaleManagement.Bll.Bll;
using PointOfSaleManagement.Models.Models;
using PointOfSaleManagementApp.ViewModels;

namespace PointOfSaleManagementApp.Controllers
{
    public class CustomerController : Controller
    {
        CustomerManager _customerManager = new CustomerManager();
        Customer _customer = new Customer();
        // GET: Customer
        public ActionResult Index()
        {
            var customers = _customerManager.GetAll();
            List<CustomerViewModel> customerVm = new List<CustomerViewModel>();
            foreach (var customersPersion in customers)
            {
                var customersVm = new CustomerViewModel();
                customersVm.Id = customersPersion.Id;
                customersVm.Name = customersPersion.Name;
                customersVm.Code = customersPersion.Code;
                customersVm.Email = customersPersion.Email;
                customersVm.Contact = customersPersion.Contact;
                customersVm.LoyaltyPoint = customersPersion.LoyaltyPoint;
                customersVm.Image = customersPersion.Image;
                customerVm.Add(customersVm);
            }
            return View(customerVm);
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
                    imgFile.SaveAs(Server.MapPath("~/images/customer/" + fileName));
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

        private void CreateCustomer(Customer customer, byte[] imageData)
        {
            Customer cust = new Customer
            {
                Name = customer.Name,
                Code = customer.Code,
                Address = customer.Address,
                Email = customer.Email,
                Contact = customer.Contact,
                LoyaltyPoint = customer.LoyaltyPoint,
                Image = imageData,
                ImagePath = "~/images/customer/" + customer.ImagePath + DateTime.Now,
                IsDeleted = false
            };
            _customerManager.Add(cust);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Exclude = "Image")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var imageData = GetImageData(customer.Name);
                if (HasFile(imageData))
                {
                    CreateCustomer(customer, imageData);
                    ViewBag.SuccessMsg = "Customer Saved Successfully.";
                }
                //if (customer.ImageUpload != null)
                //{
                //    string fileName = Path.GetFileNameWithoutExtension(customer.ImageUpload.FileName);
                //    string extension = Path.GetExtension(customer.ImageUpload.FileName);
                //    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                //    customer.ImagePath = "~/images/customer/" + fileName;
                //    customer.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/customer/"), fileName));
                //}

                //bool isSaved = _customerManager.Add(customer);
                //if (isSaved)
                //{
                //    ViewBag.SuccessMsg = "Customer Saved Successfully.";
                //}
                else
                {
                    ViewBag.FailMsg = "Vailed!";
                }
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.FailMsg = "Validation Error!";
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            _customer.Id = id;
            var customer = _customerManager.GetByID(_customer);
            return View(customer);
        }
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                //if (customer.ImageUpload != null)
                //{
                //    string fileName = Path.GetFileNameWithoutExtension(customer.ImageUpload.FileName);
                //    string extension = Path.GetExtension(customer.ImageUpload.FileName);
                //    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                //    customer.ImagePath = "~/images/customer/" + fileName;
                //    customer.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/customer/"), fileName));
                //}

                bool isSaved = _customerManager.Add(customer);
                if (isSaved)
                {
                    ViewBag.SuccessMsg = "Customer Update Successfully.";
                }
                else
                {
                    ViewBag.FailMsg = "Vailed!";
                }
                return RedirectToAction("Index", "Customer");
            }
            else
            {
                ViewBag.FailMsg = "Validation Error!";
            }

            return View(customer);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _customer.Id = (int)id;
            Customer customer = _customerManager.GetByID(_customer);
            //Student student = db.Students.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _customer.Id = id;
            _customerManager.Delete(_customer);
            return RedirectToAction("Index");
        }
    }
}