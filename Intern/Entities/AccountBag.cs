using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("AccountBags")]
    public class AccountBag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountBagId { get; set; } 
        public int AccountId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Account Account { get; set; }
        public Product Product { get; set; }
    }
}
