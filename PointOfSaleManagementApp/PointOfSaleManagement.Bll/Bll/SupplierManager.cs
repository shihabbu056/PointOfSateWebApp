using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleManagement.Models.Models;
using PointOfSaleManagement.Repository.Repository;

namespace PointOfSaleManagement.Bll.Bll
{
    public class SupplierManager
    {
        SupplierRepository _supplierRepository = new SupplierRepository();
        public bool Add(Supplier supplier)
        {
            return _supplierRepository.Add(supplier);
        }
        public bool Delete(Supplier supplier)
        {
            return _supplierRepository.Delete(supplier);
        }
        public bool Update(Supplier supplier)
        {
            return _supplierRepository.Update(supplier);
        }
        public List<Supplier> GetAll()
        {
            return _supplierRepository.GetAll();
        }
        public Supplier GetByID(Supplier supplier)
        {
            return _supplierRepository.GetByID(supplier);
        }
    }
}
