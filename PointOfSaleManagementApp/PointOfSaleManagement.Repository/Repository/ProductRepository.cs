﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleManagement.DatabaseContext.DatabaseContext;
using PointOfSaleManagement.Models.Models;

namespace PointOfSaleManagement.Repository.Repository
{
    public class ProductRepository
    {
        PointOfSaleDbContext db = new PointOfSaleDbContext();

        public bool Add(Product product)
        {
            int isExecuted = 0;

            db.Products.Add(product);
            isExecuted = db.SaveChanges();

            if (isExecuted > 0)
            {
                return true;
            }

            return false;
        }
        public bool Delete(Product product)
        {
            int isExecuted = 0;
            Product aProduct = db.Products.FirstOrDefault(c => c.Id == product.Id);

            db.Products.Remove(aProduct);
            isExecuted = db.SaveChanges();

            if (isExecuted > 0)
            {
                return true;
            }


            return false;
        }
        public bool Update(Product product)
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
            db.Entry(product).State = EntityState.Modified;
            isExecuted = db.SaveChanges();
            if (isExecuted > 0)
            {
                return true;
            }
            return false;
        }
        public List<Product> GetAll()
        {
            return db.Products.ToList();
        }
        public Product GetByID(Product product)
        {
            Product aProduct = db.Products.FirstOrDefault(c => c.Id == product.Id);
            return aProduct;
        }
    }
}