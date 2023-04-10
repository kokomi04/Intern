using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("BillStatus")]
    public class BillStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillStatusId { get; set; } 

        [MaxLength(20)]
        public string BillStatusCode { get; set; }
        [MaxLength(100)]
        public string BillStatusDetail { get; set; }

        public List<Bill> Bills { get; set; }
    }
}
