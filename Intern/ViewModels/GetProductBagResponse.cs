using Intern.Entities;

namespace Intern.ViewModels
{
    public class GetProductBagResponse
    {
        public Product Product { get; set; }
        public AccountBag AccountBag { get; set; }
        public CategoryType CategoryType { get; set; }
    }
}
