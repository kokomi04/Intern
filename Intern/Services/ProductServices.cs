//using Microsoft.EntityFrameworkCore;
using Intern.EF;
using Intern.Entities;
using Intern.ViewModels;
using Intern.ViewModels.PagingCommon;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Intern.Services
{
    public class ProductServices : IProductServices
    {
        private readonly MyDbContext _context;

        public ProductServices(MyDbContext context)
        {
            _context = context;
        }


        //public async Task<PageResult<Product>> GetProductDetails(ProductPagingRequest request)
        //{
        //    var productDetails = new List<Product>();

        //    var parentProductDetails = _context.Products.Where(x => x.ParentId != null);

        //    if (!String.IsNullOrEmpty(request.keyWord))
        //        parentProductDetails = parentProductDetails.Where(x => x.ProductDetailName.Contains(request.keyWord));

        //    if (request.MinPrice.HasValue)
        //        parentProductDetails = parentProductDetails.Where(x => x.Price >= request.MinPrice);

        //    if (request.MaxPrice.HasValue)
        //        parentProductDetails = parentProductDetails.Where(x => x.Price <= request.MaxPrice);

        //    var lst = parentProductDetails.ToList();

        //    if (lst.Count() > 0)
        //        foreach (var item in lst)
        //        {
        //            var pd = lst.Find(x => x.ParentId == item.ProductDetailId);

        //            if (pd == null)
        //            {
        //                productDetails.Add(item);
        //            }
        //        }

        //    int totalRecord = productDetails.Count();

        //    int totalPage = (productDetails.Count() % request.PageSize > 0) ? productDetails.Count() / request.PageSize + 1 : productDetails.Count() / request.PageSize;

        //    productDetails = productDetails.Skip(request.PageSize * (request.PageIndex - 1)).Take(request.PageSize).ToList();

        //    var pageResult = new PageResult<ProductDetail>()
        //    {
        //        Data = productDetails,
        //        TotalPage = totalPage,
        //        TotalRecord = totalRecord,
        //    };

        //    return pageResult;
        //}

        public async Task<PageResult<Product>> NextPage(int pageIndex = 1)
        {
            var products = _context.Products.AsQueryable();
            int totalRecords = products.Count();
            int totalPages;
            if (products.Count() % 5 == 0)
                totalPages = products.Count() / 5;
            else
                totalPages = products.Count() / 5 + 1;

            var data = products.Skip((5 * (pageIndex - 1))).Take(5).ToList();

            var pageResult = new PageResult<Product>()
            {
                TotalPage = totalPages,
                TotalRecord = totalRecords,
                Data = data
            };
            return pageResult;
        }

        public async Task<List<Product>> GetProductHome()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Account> CheckLogin(SignInRequest request)
        {
            var acc = _context.Accounts.FirstOrDefault(x => (x.AccountUserName == request.UserName) & x.AccountPassWord == request.Password);
            return acc;
        }
    }
}
