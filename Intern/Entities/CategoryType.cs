using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("CategoryTypes")]
    public class CategoryType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryTypeId { get; set; } 

        [MaxLength(20)]
        public string CategoryTypeCode { get; set; }
        [MaxLength(100)]
        public string CategoryTypeDetail { get; set; }
    }
}
