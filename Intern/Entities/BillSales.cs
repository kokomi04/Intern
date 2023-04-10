using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("BillSales")]
    public class BillSales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillSalesId { get; set; } 
        public int BillId { get; set; }
        public int SalesId { get; set; }

        public Bill Bill { get; set; }
        public Sales Sales { get; set; }
    }
}
