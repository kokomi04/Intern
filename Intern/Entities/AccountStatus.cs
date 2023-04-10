using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("AccountStatus")]
    public class AccountStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountStatusId { get; set; } 

        [MaxLength(20)]
        public string AccountStatusCode { get; set; }
        [MaxLength(100)]
        public string AccountStatusDetail { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
