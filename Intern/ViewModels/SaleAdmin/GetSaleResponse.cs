using Intern.Entities;

namespace Intern.ViewModels.SaleAdmin
{
    public class GetSaleResponse
    {
        public List<Sales> shipVouchers { get; set; }
        public List<Sales> voucherVouchers { get; set; }

    }
}
