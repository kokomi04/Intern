namespace Intern.ViewModels
{
    public class ProductDetailRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public List<int> PropertyDetailIds { get; set; }
    }
}
