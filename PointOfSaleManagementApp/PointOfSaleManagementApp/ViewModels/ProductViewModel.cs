using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using PointOfSaleManagement.Models.Models;

namespace PointOfSaleManagementApp.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int ReorderLevel { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        [Display(Name = "Product Image")]
        public string ImagePath { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}