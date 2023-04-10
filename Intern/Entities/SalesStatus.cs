using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("SalesStatus")]
    public class SalesStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalesStatusId { get; set; } 

        [MaxLength(20)]
        public string SalesStatusCode { get; set; }
        [MaxLength(100)]
        public string SalesStatusDetail { get; set; }

        public List<Sales> Sales { get; set; }
    }
}
