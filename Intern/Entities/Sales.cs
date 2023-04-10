using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("Sales")]
    public class Sales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalesId { get; set; } 
        public int SalesStatusId { get; set; }
        public int SalesTypeId { get; set; }
        public string SalesCode { get; set; }
        public string SalesName { get; set; }
        public int? SalesPersen { get; set; }
        public int SaleInt { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<BillSales> BillSales { get; set; } 
        public SalesType SalesType { get; set; }
        public SalesStatus SalesStatus { get; set; }

    }
}
