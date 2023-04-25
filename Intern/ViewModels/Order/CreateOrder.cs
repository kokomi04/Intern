using Intern.Entities;

namespace Intern.ViewModels.Order
{
    public class CreateOrder
    {
        public List<Sales> salesOfBill { get; set; }
        public List<BuyMethod> buyMethods { get; set; }
        public List<ShipMethod> shipMethods { get; set; }
        public List<OrderItem> orderItems { get; set; }
        public List<AccountShipContact> accountShipContacts { get; set; }
    }
}
