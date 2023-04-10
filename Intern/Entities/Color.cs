using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("Colors")]
    public class Color
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ColorId { get; set; } 

        [MaxLength(20)]
        public string ColorCode { get; set; }
        [MaxLength(100)]
        public string ColorDetail { get; set; }

        public List<Product> Products { get; set; }
    }
}
