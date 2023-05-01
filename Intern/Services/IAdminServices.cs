using Intern.Entities;
using Intern.ViewModels;
using Intern.ViewModels.Analysis;
using Intern.ViewModels.Order;

namespace Intern.Services
{
    public interface IAdminServices
    {
        Task<AnalysisData> GetAnalysisData();
        Task<AllProperty> GetAllProperty();
        Task<List<Product>> SearchTop5Product(string search);
        Task<Bill> CreateBillInShop(int idEmployee);
        Task<AllBillDetails> GetAllBillDetailOfBill(int idBill);
    }
}
