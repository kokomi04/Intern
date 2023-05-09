using Intern.Entities;

namespace Intern.ViewModels.BillAdmin
{
    public class PrintBillResponse
    {
        public double totalResult { get; set; }
        public string billCode { get; set; }
        public string? employeeName { get; set; }
        public DateTime closeDate { get; set; }
        public double total { get; set; }
        public int shipPrice { get; set; }
        public int freeShip { get; set; }
        public List<BillDetailPrint> billDetailPrints { get; set; }
    }
}
