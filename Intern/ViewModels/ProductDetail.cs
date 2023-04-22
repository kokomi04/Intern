using Intern.Entities;

namespace Intern.ViewModels
{
    public class ProductDetail
    {
        public Product product{get;set;}
        public Brand brand{get;set;}
        public Producer producer{get;set;}
        public Size size{get;set;}
        public Color color{get;set;}
        public CategoryType categoryType{get;set;}
    }
}
