using Intern.Entities;
using Intern.ViewModels;
using Intern.ViewModels.Authen;
using Intern.ViewModels.ChangeAccount;
using Intern.ViewModels.Order;
using Intern.ViewModels.PagingCommon;

namespace Intern.Services
{
    public interface IProductServices
    {
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
        Task<AccountBag> UpdateAccountBagById(int[] request);
        Task<SignInResponse> CheckLogin(SignInRequest request);
        Task<SignUpRequest> CreateAccount(SignUpRequest request);
        Task<RepassResponse> RePass(RepassRequest request);
        Task<ReInfoRequest> ReInfo(ReInfoRequest request);
        Task<AccountCustom> GetContacts(int accountId);
        Task<AddAccShipContactRequest> AddNewAccountShipContact(AddAccShipContactRequest request);
        Task<int> DeleteShipContact(int idAccountShipContact);
        Task<CreateOrder> GetCalculbag(int[] request);
        Task<int> CreateBill(CreateBillRequest request);

    }
}
