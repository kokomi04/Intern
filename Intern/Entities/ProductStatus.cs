using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("ProductStatus")]
    public class ProductStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductStatusId { get; set; } 

        [MaxLength(20)]
        public string ProductStatusCode { get; set; }
        [MaxLength(100)]
        public string ProductStatusDetail { get; set; }

        public List<Product> Products { get; set; }
    }
}
