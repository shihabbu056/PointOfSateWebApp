using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PointOfSaleManagement.Models.Contracts;

namespace PointOfSaleManagement.Models.Models
{
    public class Product: IModel, IDeletable
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int ReorderLevel { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
        public bool IsDeleted { get; set; }
        public bool WithDeleted()
        {
            return IsDeleted;
        }
    }
}
