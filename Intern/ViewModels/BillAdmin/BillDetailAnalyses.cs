using Intern.Entities;

namespace Intern.ViewModels.BillAdmin
{
    public class BillDetailAnalyses
    {
        public int billDetailId { get; set; }
        public byte[] productImg { get; set; }
        public string productName { get; set; }
        public int quantity { get; set; }
        public int quantityInventory { get; set; }
        public int shellprice { get; set; }
        public int total { get; set; }
    }
}
