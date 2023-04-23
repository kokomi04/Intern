using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("ProductImgs")]
    public class ProductImgs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductImgId { get; set; } 
        public int ProductId { get; set; }
        public byte[] ProductImg { get; set; }

        public Product Product { get; set; }
    }
}
