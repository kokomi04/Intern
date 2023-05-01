namespace Intern.ViewModels.Order
{
    public class CreateBillRequest
    {
        public int[] AccountBags { get; set; }
        public int AccountShipContactId { get; set; }
        public int BuyOptId { get; set; }
        public string? BuyerNotification { get; set; }
        public int ShipOptId { get; set; }
        public int ShipPrice { get; set; }
        public int? ShipVoucher { get; set; }
        public int? VoucherVoucher { get; set; }
    }
}
