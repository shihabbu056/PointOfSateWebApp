using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleManagement.Models.Models
{
    public class PurchaseDetail
    {
        public int Id { get; set; }
        public int? PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime ManufacturedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Remarks { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public double NewMrp { get; set; }
    }
}
