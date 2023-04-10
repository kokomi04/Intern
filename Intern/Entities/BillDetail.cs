using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("BillDetails")]
    public class BillDetail
    {
        [Key]   
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BillDetailId { get; set; } 
        public int BillId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public Product Product { get; set; }
        public Bill Bill { get; set; }
    }
}
