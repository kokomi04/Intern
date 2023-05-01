using Intern.Entities;

namespace Intern.ViewModels.Order
{
    public class OrderItem
    {
        public int accountBagId { get; set; }
        public int quantity { get; set; }
        public Product product { get; set; }
        public CategoryType categoryType { get; set; }
    }
}
