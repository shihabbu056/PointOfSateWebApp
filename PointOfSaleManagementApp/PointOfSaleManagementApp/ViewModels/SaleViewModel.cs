using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using PointOfSaleManagement.Models.Models;

namespace PointOfSaleManagementApp.ViewModels
{
    public class SaleViewModel
    {
        public int Id { get; set; }
        public string SaleInvoice { get; set; }
        public int CustomerId { get; set; }
        public int PurchaseDetailId { get; set; }
        public string Date { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal PayableAmount { get; set; }
        public List<SaleDetail> SaleDetails { get; set; }
        [NotMapped]
        public List<Customer> Customers { get; set; }
        [NotMapped]
        public List<PurchaseDetail> PurchaseDetails { get; set; }
    }
}