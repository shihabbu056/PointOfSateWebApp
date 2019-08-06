using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSaleManagementApp.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        [Display(Name = "Customer Name")]
        public string Name { get; set; }
        public string Address { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public long Contact { get; set; }
        public int LoyaltyPoint { get; set; }
        [Display(Name = "Customer Photo")]
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
    }
}