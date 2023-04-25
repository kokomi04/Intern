namespace Intern.ViewModels.Analysis
{
    public class AnalysisData
    {
        public int productCountShelling { get; set; }
        public int voucherCountUsing { get; set; }
        public int productQuantityInventory { get; set; }
        public int custumerCountActive { get; set; }
        public List<TopAccountPaid> top5AccountPaids { get; set; }
        public List<TopProductSold> top5ProductSolds { get; set; }
        public List<int> totalBillMonth { get; set; }
        public List<MonthAnalysis> analysisProfit12Month { get; set; }
    }
}
