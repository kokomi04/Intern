using Intern.Entities;
using Intern.ViewModels;
using Intern.ViewModels.Analysis;
using Intern.ViewModels.BillAdmin;
using Intern.ViewModels.Order;
using Intern.ViewModels.RemakeAdmin;
using Intern.ViewModels.SaleAdmin;
using Intern.ViewModels.Upload;
using Microsoft.AspNetCore.Mvc;

namespace Intern.Services
{
    public interface IAdminServices
    {
        Task<AnalysisData> GetAnalysisData();
        Task<AllProperty> GetAllProperty();
        Task<List<Product>> SearchTop5Product(string? search);
        Task<int> Upload(UploadRequest request, Data data);
        Task<Bill> CreateBillInShop(int idEmployee);
        Task<AllBillDetails> GetAllBillDetailOfBill(int idBill);
        Task<int> AddProduct2BillDetail(int idProduct, int idBill);
        Task<int> UpdateBillDetailQuantity(int idBillDetail, int quantity);
        Task<Product> RemakeProduct(RemakeProduct product);
        Task<int> CreateProperty(CreateAndRemakeProperty request);
        Task<int> RemakeProperty(CreateAndRemakeProperty request);
        Task<List<GetBillTypeRequest>> GetAllBillType(int opt);
        Task<GetSaleResponse> GetSales();
        Task<int> CreateSales(CreateSaleRequest request);
    }
}
