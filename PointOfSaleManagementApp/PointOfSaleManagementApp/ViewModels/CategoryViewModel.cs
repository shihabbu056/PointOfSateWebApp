using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PointOfSaleManagementApp.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        [Required]
        [Remote("IsCategoryNameExist", "Category", HttpMethod = "POST", ErrorMessage = "Category Already Exist, Try Another")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

    }
}