using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("Bills")]
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BillId { get; set; } 
        public int? AccountShipContactId { get; set; }
        public int BuyMethodId { get; set; }
        public int BillStatusId { get; set; }
        public int ShipMethodId { get; set; }
        public int? IdEmployee { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ShipToBuyerDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? ProductReturnDate { get; set; } = null;
        public string? BuyerNotification { get; set; }
        public int? ShipPrice { get; set; }
        public string BillCode { get; set; }
        public double TotalBill { get; set; }

        public List<BillDetail> BillDetail { get; set; }
        public List<BillSales> BillSales { get; set; }
        public BuyMethod BuyMethod { get; set; }
        public BillStatus BillStatus { get; set; }
        public ShipMethod ShipMethod { get; set; }
        public AccountShipContact AccountShipContact { get; set; }

        [ForeignKey(nameof(IdEmployee))]
        public Account? Employee { get; set; }
    }
}
