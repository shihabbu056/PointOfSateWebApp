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
                products.ImagePath = productItem.ImagePath;
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
        public ActionResult Add(ProductViewModel productVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var imageData = GetImageData(productVm.Name);
                    //Image file project folder save with unique file name
                    if (productVm.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(productVm.ImageUpload.FileName);
                        string extension = Path.GetExtension(productVm.ImageUpload.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                        productVm.ImagePath = "~/images/product/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/images/product/"), fileName);
                        productVm.ImageUpload.SaveAs(fileName);
                    }
                    if (productVm.ImagePath == "/images/No_Image_Available.jpg")
                    {
                        productVm.ImagePath = null;
                    }
                    var product = ProductCreate(productVm);

                    if (_productManager.Add(product))
                    {
                        ViewBag.SuccessMsg = "Product Saved Successfully.";
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
                ImagePath = product.ImagePath,
                CategoryId = product.CategoryId
            };
            ViewBag.CategoryId = new SelectList(_categoryManager.GetAll(), "Id", "Name", product.CategoryId);
            if (product.ImagePath == null)
            {
                product.ImagePath = "~/images/No_Image_Available.jpg";
            }
            return View(productEdit);
        }
        [HttpPost]
        public ActionResult Edit(ProductViewModel productVm)
        {
            if (ModelState.IsValid)
            {
                if (productVm.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(productVm.ImageUpload.FileName);
                    string extension = Path.GetExtension(productVm.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                    productVm.ImagePath = "~/images/product/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/images/product/"), fileName);
                    productVm.ImageUpload.SaveAs(fileName);
                }

                if (productVm.ImagePath == "/images/No_Image_Available.jpg")
                {
                    productVm.ImagePath = null;
                }
                var product = ProductCreate(productVm);

                if (_productManager.Update(product))
                {
                    ViewBag.SuccessMsg = "Update Successfully.";
                }
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

        private static Product ProductCreate(ProductViewModel productVm)
        {
            Product product = new Product();
            product.Id = productVm.Id;
            product.Name = productVm.Name;
            product.Code = productVm.Code;
            product.CategoryId = productVm.CategoryId;
            product.ReorderLevel = productVm.ReorderLevel;
            product.Description = productVm.Description;
            product.ImagePath = productVm.ImagePath;
            product.IsDeleted = false;
            return product;
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

        public JsonResult GetByCategory(int? categoryId)
        {
            if (categoryId == null)
            {
                return null;
            }

            //var products = db.Products.Where(c => c.CategoryId == categoryId).ToList();
            var products = _productManager.GetAll(categoryId);

            return Json(products, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetByProductCode(int? productId)
        {
            if (productId==null)
            {
                return null;
            }

            var productCode = _productManager.GetByID(productId);
            return Json(productCode, JsonRequestBehavior.AllowGet);
        }
    }
}