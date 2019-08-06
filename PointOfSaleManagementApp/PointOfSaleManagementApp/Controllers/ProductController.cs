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
    public class ProductController : Controller
    {
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();
        Product _product = new Product();
        // GET: Product
        public ActionResult Index()
        {
            //return View(GetAllProduct());
            var categorys = _categoryManager.GetAll();
            var product = _productManager.GetAll();

            List<ProductViewModel> productVm = new List<ProductViewModel>();
            foreach (var productItem in product)
            {
                var products = new ProductViewModel();
                products.Id = productItem.Id;
                products.Name = productItem.Name;
                products.Code = productItem.Code;
                products.Description = productItem.Description;
                products.ReorderLevel = productItem.ReorderLevel;
                products.Image = productItem.Image;
                products.Category = categorys.Where(x => x.Id == productItem.CategoryId).FirstOrDefault();
                productVm.Add(products);
            }
            return View(productVm);
        }

        IEnumerable<Product> GetAllProduct()
        {
            var categorys = _categoryManager.GetAll();
            var product = _productManager.GetAll();

            List<Product> products = new List<Product>();
            foreach (var productItem in product)
            {
                var productVM = new Product();
                productVM.Id = productItem.Id;
                productVM.Name = productItem.Name;
                productVM.Code = productItem.Code;
                productVM.Description = productItem.Description;
                productVM.ReorderLevel = productItem.ReorderLevel;
                productVM.ImagePath = productItem.ImagePath;
                productVM.Category = categorys.Where(x => x.Id == productItem.CategoryId).FirstOrDefault();
                products.Add(productVM);
            }
            return products;
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

        private void CreateProduct(ProductViewModel customerVm, byte[] imageData)
        {
            Product cust = new Product
            {
                Name = customerVm.Name,
                Code = customerVm.Code,
                CategoryId = customerVm.CategoryId,
                ReorderLevel = customerVm.ReorderLevel,
                Description = customerVm.Description,
                Image = imageData,
                ImagePath = "~/images/product/" + customerVm.ImagePath + DateTime.Now,
                IsDeleted = false
            };
            _productManager.Update(cust);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var categorys = _categoryManager.GetAll();
            ProductViewModel productVm = new ProductViewModel();
            productVm.Categories = categorys;
            productVm.ReorderLevel = 1000;
            return View(productVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Exclude = "Image")]ProductViewModel productVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var imageData = GetImageData(productVm.Name);
                    if (HasFile(imageData))
                    {
                        CreateProduct(productVm, imageData);
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
        public ActionResult Edit(int id)
        {
            _product.Id = id;
            var product = _productManager.GetByID(_product);
            ProductViewModel productEdit = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.Code,
                ReorderLevel = product.ReorderLevel,
                Description = product.Description,
                Image = product.Image,
                CategoryId = product.CategoryId
            };
            ViewBag.CategoryId = new SelectList(_categoryManager.GetAll(), "Id", "Name", product.CategoryId);
            return View(productEdit);
        }
        [HttpPost]
        public ActionResult Edit(ProductViewModel productVm)
        {
            if (ModelState.IsValid)
            {
                var imageData = GetImageData(productVm.Name);
                if (HasFile(imageData))
                {
                    CreateProduct(productVm, imageData);
                    ViewBag.SuccessMsg = "Product Update Successfully.";
                }
                //if (product.ImageUpload != null)
                //{
                //    string fileName = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
                //    string extension = Path.GetExtension(product.ImageUpload.FileName);
                //    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                //    product.ImagePath = "~/images/product/" + fileName;
                //    product.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/product/"), fileName));
                //}
                
                else
                {
                    ViewBag.FailMsg = "Update Vailed!";
                }
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ViewBag.FailMsg = "Validation Error!";
            }

            return View(productVm);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _product.Id = (int)id;
            Product product = _productManager.GetByID(_product);
            ProductViewModel productVm = new ProductViewModel();
            //Student student = db.Students.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(productVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _product.Id = id;
            _productManager.Delete(_product);
            return RedirectToAction("Index");
        }
    }
}