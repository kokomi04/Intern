using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }
        public string AccountUserName { get; set; }
        public string AccountPassWord { get; set; }
        public int AccountStatusId { get; set; }
        public int RoleId { get; set; }
        public string AccountName { get; set; }
        public DateTime? AccountBorn { get; set; }
        public string AccountDetailAddress { get; set; }
        public DateTime AccountCreateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
