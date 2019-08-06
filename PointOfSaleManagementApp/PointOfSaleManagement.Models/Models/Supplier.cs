using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleManagement.Models.Contracts;

namespace PointOfSaleManagement.Models.Models
{
    public class Supplier : IModel, IDeletable
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public long Contact { get; set; }
        public string ContactPerson { get; set; }
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
        public bool IsDeleted { get; set; }

        public bool WithDeleted()
        {
            return IsDeleted;
        }
    }
}
