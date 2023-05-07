namespace Intern.ViewModels.BillAdmin
{
    public class GetBillTypeRequest
    {
        public int billId { get; set; }
        public string billCode { get; set; }
        public DateTime CreateBill { get; set; }
        public string? customerName { get; set; }
        public string? reveceiName { get; set; }
        public string? reveceiSdt { get; set; }
        public string shipMethodName { get; set; }
        public string? voucherCode { get; set; }
        public string? voucherShipCode { get; set; }
        public int? shipPrice { get; set; }
        public string? notification { get; set; }

        public int totalBill { get; set; }
        public int? shipVoucher { get; set; }
        public int? voucherVoucher { get; set; }

        public string billStatus { get; set; }
        public string buyMethod { get; set; }
        public string buyStatus { get; set; }
        public string reveceiMethod { get; set; }
        public string shipStatus { get; set; }

        public string? reveceiContact { get; set; }

        public int billStatusId { get; set; }


        public List<BillDetailAnalyses> billDetailAnalyses { get; set; }

        
    }
}
