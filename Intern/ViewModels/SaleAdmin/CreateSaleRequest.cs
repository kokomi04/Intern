namespace Intern.ViewModels.SaleAdmin
{
    public class CreateSaleRequest
    {
        public int SaleTypeId { get; set; }
        public string SalesCode { get; set; }
        public string SalesName { get; set; }
        public int? SalesPercent { get; set; }
        public int? SalesInt { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
