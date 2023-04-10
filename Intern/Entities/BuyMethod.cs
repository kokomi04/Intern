using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("BuyMethods")]
    public class BuyMethod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BuyMethodId { get; set; } 

        [MaxLength(20)]
        public string BuyMethodCode { get; set; }
        [MaxLength(100)]
        public string BuyMethodName { get; set; }
    }
}
