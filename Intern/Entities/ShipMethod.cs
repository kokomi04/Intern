using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("ShipMethods")]
    public class ShipMethod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShipMethodId { get; set; } 

        [MaxLength(20)]
        public string ShipMethodCode { get; set; }
        [MaxLength(100)]
        public string ShipMethodName { get; set; }
        public int ShipPrice { get; set; }

        public List<Bill> Bills { get; set; }
    }
}
