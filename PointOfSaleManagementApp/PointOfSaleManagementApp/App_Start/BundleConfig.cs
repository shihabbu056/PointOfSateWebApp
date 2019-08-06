using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace PointOfSaleManagementApp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/scripts/plugins/jquery-validation/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
            ));
        }
    }
}