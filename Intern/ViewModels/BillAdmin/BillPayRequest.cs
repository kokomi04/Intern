namespace Intern.ViewModels.BillAdmin
{
    public class BillPayRequest
    {
        public int idBill { get; set; }
        public int idBuyMethod { get; set; }
        public int idEmployee { get; set; }
        public int idVoucher { get; set; }
        public string customAddress { get; set;}
        public string customerName { get; set; }
        public string customerSdt { get; set; }
    }
}
