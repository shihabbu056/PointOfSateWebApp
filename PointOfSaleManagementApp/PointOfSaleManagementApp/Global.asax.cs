using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using PointOfSaleManagement.Models.Models;
using PointOfSaleManagementApp.ViewModels;

namespace PointOfSaleManagementApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            //Auto Mapper Initialization Scope
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PurchaseViewModel, Purchase>();
                cfg.CreateMap<Purchase, PurchaseViewModel>();
            });
        }
    }
}
