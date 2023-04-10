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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountId { get; set; }
        [MaxLength(50)]
        public string AccountUserName { get; set; }
        [MaxLength(100)]
        public string AccountPassWord { get; set; }
        public int AccountStatusId { get; set; }
        public int RoleId { get; set; }
        public string AccountName { get; set; }
        public DateTime? AccountBorn { get; set; }
        public string? AccountDetailAddress { get; set; }
        public DateTime AccountCreateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public List<VoteStar> VoteStars { get; set; }
        public List<AccountBag> AccountBags { get; set; }
        public List<AccountShipContact> AccountShipContacts { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public Role Role { get; set; }

    }
}
