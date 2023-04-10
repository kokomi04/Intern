using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("ProductImgs")]
    public class ProductImg
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductImgId { get; set; } 
        public int ProductId { get; set; }
        public int CountImg { get; set; }
        public byte[] ProductImage { get; set; }

        public Product Product { get; set; }
    }
}
