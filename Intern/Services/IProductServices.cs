using Intern.Entities;
using Intern.ViewModels;
using Intern.ViewModels.PagingCommon;

namespace Intern.Services
{
    public interface IProductServices
    {
        Task<int> BuyProduct(ProductDetailRequest request);
        Task<int> UpdateQuantity(ProductDetailRequest request);
        Task<PageResult<ProductDetail>> GetProductDetails(ProductPagingRequest request);
    }
}
