using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace PointOfSaleManagement.DatabaseContext.Migrations
{
    

    internal sealed class Configuration : DbMigrationsConfiguration<PointOfSaleManagement.DatabaseContext.DatabaseContext.PointOfSaleDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PointOfSaleManagement.DatabaseContext.DatabaseContext.PointOfSaleDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
