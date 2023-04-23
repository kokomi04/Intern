using Intern.Entities;
using Intern.ViewModels;
using Intern.ViewModels.Authen;
using Intern.ViewModels.ChangeAccount;
using Intern.ViewModels.PagingCommon;

namespace Intern.Services
{
    public interface IProductServices
    {
        //Task<PageResult<Product>> GetProductDetails(ProductPagingRequest request);
        Task<List<Product>> NextPage(int pageIndex);
        Task<List<Product>> SearchProduct(string search);
        Task<List<Product>> GetProductHome();
        Task<List<Product>> DressCategory();
        Task<List<Product>> PanCategory();
        Task<List<Product>> ShirtCategory();
        Task<ProductDetail> GetProductId(int productId);
        Task AddProductToBag(AddProToBagRequest request);
        Task<List<GetProductBagResponse>> GetProductBagByAccountId(int accountId);
        Task<int> DeleteBag(int accountBagId);
        Task<SignInResponse> CheckLogin(SignInRequest request);
        Task<SignUpRequest> CreateAccount(SignUpRequest request);
        Task<RepassResponse> RePass(RepassRequest request);
        Task<ReInfoRequest> ReInfo(ReInfoRequest request);
        Task<AccountCustom> GetContacts(int accountId);
    }
}
