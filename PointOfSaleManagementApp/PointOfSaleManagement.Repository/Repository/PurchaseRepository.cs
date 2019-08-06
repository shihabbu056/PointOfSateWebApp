using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleManagement.DatabaseContext.DatabaseContext;
using PointOfSaleManagement.Models.Models;

namespace PointOfSaleManagement.Repository.Repository
{
    public class PurchaseRepository
    {
        PointOfSaleDbContext db = new PointOfSaleDbContext();

        public bool Add(Purchase purchase)
        {
            int isExecuted = 0;

            db.Purchases.Add(purchase);
            isExecuted = db.SaveChanges();

            if (isExecuted > 0)
            {
                return true;
            }

            return false;
        }
    }
}
