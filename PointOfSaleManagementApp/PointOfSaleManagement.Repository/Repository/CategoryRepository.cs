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
    public class CategoryRepository
    {
        PointOfSaleDbContext db = new PointOfSaleDbContext();

        public bool Add(Category category)
        {
            int isExecuted = 0;

            db.Categories.Add(category);
            isExecuted = db.SaveChanges();

            if (isExecuted > 0)
            {
                return true;
            }

            return false;
        }
        public bool Delete(Category category)
        {
            int isExecuted = 0;
            Category aCategory = db.Categories.FirstOrDefault(c => c.Id == category.Id);

            db.Categories.Remove(aCategory);
            isExecuted = db.SaveChanges();

            if (isExecuted > 0)
            {
                return true;
            }


            return false;
        }
        public bool Update(Category category)
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
            db.Entry(category).State = EntityState.Modified;
            isExecuted = db.SaveChanges();
            if (isExecuted > 0)
            {
                return true;
            }
            return false;
        }
        public List<Category> GetAll()
        {
            return db.Categories.ToList();
        }
        public Category GetByID(Category category)
        {
            Category aStudent = db.Categories.FirstOrDefault(c => c.Id == category.Id);
            return aStudent;
        }

        public Category GetByID(int? categoryId)
        {
            Category aStudent = db.Categories.FirstOrDefault(c => c.Id == categoryId);
            return aStudent;
        }

        public bool IsCategoryNameDuplicate(string categoryName)
        {
            var aCategory = db.Categories.FirstOrDefault(c => c.Name == categoryName);
            if (aCategory != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
