using Intern.Entities;

namespace Intern.ViewModels.Order
{
    public class AllBillDetails
    {
        public Bill Bill { get; set; }
        public List<BillDetailAndProduct> billDetailAndProducts { get; set; }
        public List<Sales>? Sales { get; set; }
    }
}
