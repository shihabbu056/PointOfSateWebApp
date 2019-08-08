using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using PointOfSaleManagement.Models.Models;

namespace PointOfSaleManagementApp.ViewModels
{
    public class PurchaseViewModel
    {
        public int Id { get; set; }
        [Required]
        public string InvoiceNo { get; set; }
        [Display(Name = "Purchase Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name = "Supplier Name")]
        public int? SupplierId { get; set; }
        //public int CategoryId { get; set; }
        //public int? ProductId { get; set; }
        public List<PurchaseDetail> PurchaseDetails { get; set; }
        [NotMapped]
        public List<Supplier> Suppliers { get; set; }
        [NotMapped]
        public List<Category> Categories { get; set; }
    }
}