using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("AccountShipContactStatus")]
    public class AccountShipContactStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountShipContactStatusId { get; set; } 
        [MaxLength(20)]
        public string AccountShipContactStatusCode { get; set; }
        [MaxLength(100)]
        public string AccountShipContactStatusDetail { get; set; }

        public List<AccountShipContact> AccountShipContacts { get; set; }
    }
}
