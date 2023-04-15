using Intern.Entities;
using Intern.ViewModels;
using Intern.ViewModels.PagingCommon;

namespace Intern.Services
{
    public interface IProductServices
    {
        //Task<PageResult<Product>> GetProductDetails(ProductPagingRequest request);
        Task<PageResult<Product>> NextPage(int pageIndex);
        Task<List<Product>> GetProductHome();
        Task<Account> CheckLogin(SignInRequest request);

    }
}
