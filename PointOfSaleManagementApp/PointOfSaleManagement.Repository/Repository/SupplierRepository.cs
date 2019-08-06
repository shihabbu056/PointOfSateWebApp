using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleManagement.DatabaseContext.DatabaseContext;
using PointOfSaleManagement.Models.Models;

namespace PointOfSaleManagement.Repository.Repository
{
    public class SupplierRepository
    {
        PointOfSaleDbContext db = new PointOfSaleDbContext();

        public bool Add(Supplier supplier)
        {
            int isExecuted = 0;

            db.Suppliers.Add(supplier);
            isExecuted = db.SaveChanges();

            if (isExecuted > 0)
            {
                return true;
            }

            return false;
        }
        public bool Delete(Supplier supplier)
        {
            int isExecuted = 0;
            Supplier aSupplier = db.Suppliers.FirstOrDefault(c => c.Id == supplier.Id);

            db.Suppliers.Remove(aSupplier);
            isExecuted = db.SaveChanges();

            if (isExecuted > 0)
            {
                return true;
            }


            return false;
        }
        public bool Update(Supplier supplier)
        {
            int isExecuted = 0;
            //Method 1
            //Student aStudent = db.Students.FirstOrDefault(c => c.ID == student.ID);
            //if (aStudent != null)
            //{
            //    aStudent.Name = student.Name;
            //    isExecuted = db.SaveChanges();
            //}

            //Method 2
            db.Entry(supplier).State = EntityState.Modified;
            isExecuted = db.SaveChanges();
            if (isExecuted > 0)
            {
                return true;
            }
            return false;
        }
        public List<Supplier> GetAll()
        {
            return db.Suppliers.ToList();
        }
        public Supplier GetByID(Supplier supplier)
        {
            Supplier aSupplier = db.Suppliers.FirstOrDefault(c => c.Id == supplier.Id);
            return aSupplier;
        }
    }
}
