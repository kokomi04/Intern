using Intern.Entities;

namespace Intern.ViewModels.Order
{
    public class GetOrderResponse
    {
        public AccountShipContact AccountShipContact { get; set; }
        public Bill Bill { get; set; }
        public ShipMethod ShipMethod { get; set; }
        public int? FreeShip { get; set; }
        public int? VoucherSIXDO { get; set; }
        public BuyMethod BuyMethod { get; set; }
        public BillStatus BillStatus { get; set; }
        public List<BillDetailAndProduct> ProductBillDetails { get; set; }
    }
}
