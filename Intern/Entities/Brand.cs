using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("Brands")]
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BrandId { get; set; } 

        [MaxLength(20)]
        public string BrandCode { get; set; }
        [MaxLength(100)]
        public string BrandDetail { get; set; }

        public List<Product> Products { get; set; }
    }
}
