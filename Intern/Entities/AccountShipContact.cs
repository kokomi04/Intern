using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("AccountShipContacts")]
    public class AccountShipContact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountShipContactId { get; set; } 
        public int AccountId { get; set; }
        public int AccountShipContactStatusId { get; set; }
        public string ReceiverName { get; set; }
        public string AccountDetailAddress { get; set; }
        public string AccountPhoneNumber { get; set; }
        public string DistrictId { get; set; }
        public string ProvinceId { get; set; }
        public string WardCode { get; set; }

        public Account Account { get; set; }
        public AccountShipContactStatus AccountShipContactStatus { get; set; }
        public Bill Bill { get; set; }
    }
}
