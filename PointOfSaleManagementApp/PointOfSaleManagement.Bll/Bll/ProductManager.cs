using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleManagement.Models.Models;
using PointOfSaleManagement.Repository.Repository;

namespace PointOfSaleManagement.Bll.Bll
{
    public class ProductManager
    {
        ProductRepository _productRepository = new ProductRepository();
        public bool Add(Product product)
        {
            return _productRepository.Add(product);
        }
        public bool Delete(Product product)
        {
            return _productRepository.Delete(product);
        }
        public bool Update(Product product)
        {
            return _productRepository.Update(product);
        }
        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public List<Product> GetAll(int? categoryId)
        {
            return _productRepository.GetAll(categoryId);
        }
        public Product GetByID(Product product)
        {
            return _productRepository.GetByID(product);
        }

        public Product GetByID(int? productId)
        {
            return _productRepository.GetByID(productId);
        }
    }
}
