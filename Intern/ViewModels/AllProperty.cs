using Intern.Entities;

namespace Intern.ViewModels
{
    public class AllProperty
    {
        public List<Color> colors { get; set; }
        public List<Producer> producers { get; set; }
        public List<Brand> brands { get; set; }
        public List<Size> sizes { get; set; }
        public List<CategoryType> categoryTypes { get; set; }
        public List<ProductStatus> productStatuses { get; set; }
    }
}
