using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("Producers")]
    public class Producer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProducerId { get; set; } 

        [MaxLength(20)]
        public string ProducerCode { get; set; }
        [MaxLength(100)]
        public string ProducerDetail { get; set; }

        public List<Product> Products { get; set; }
    }
}
