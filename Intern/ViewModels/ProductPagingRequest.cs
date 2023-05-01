using Intern.ViewModels.PagingCommon;

namespace Intern.ViewModels
{
    public class ProductPagingRequest : PagingRequestBase
    {
        public string? keyWord { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}
