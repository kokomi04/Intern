using Intern.EF;
using Intern.Entities;
using Intern.ViewModels;
using Intern.ViewModels.Analysis;
using Intern.ViewModels.Order;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Intern.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly MyDbContext _context;

        public AdminServices(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Bill> CreateBillInShop(int idEmployee)
        {
            using (var trans = await _context.Database.BeginTransactionAsync())
            {
                int maxBillId = 0;
                if (await _context.Bills.CountAsync() > 0)
                    maxBillId = await _context.Bills.MaxAsync(x => x.BillId);

                var bill = new Bill()
                {
                    BillId = maxBillId + 1,
                    IdEmployee = idEmployee,
                    ShipMethodId = 3,
                    BillCode = "HD" + maxBillId + 1,
                    BillStatusId = 7,
                    CreateDate = DateTime.Now,
                    BuyMethodId = 4,
                };

                int maxBillDetailId = 0;
                if (await _context.BillDetails.CountAsync() > 0)
                    maxBillDetailId = await _context.BillDetails.MaxAsync(x => x.BillDetailId);
                var billDetail = new BillDetail()
                {
                    BillDetailId= maxBillDetailId + 1,
                    BillId = bill.BillId,
                    ProductId= 1,
                    Quantity = 5,
                };
                int priceProduct = _context.Products.FirstOrDefault(x => x.ProductId == billDetail.ProductId).Price;
                billDetail.Price = priceProduct * billDetail.Quantity;

                await _context.Bills.AddAsync(bill);
                await _context.BillDetails.AddAsync(billDetail);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();
                return bill;
            }
        }

        public async Task<AllBillDetails> GetAllBillDetailOfBill(int idBill)
        {
            var bill = await _context.Bills.FindAsync(idBill);
            if (bill == null)
                return null;

            var billDetailAndProducts = new List<BillDetailAndProduct>();
            var billDetails = _context.BillDetails.Where(x => x.BillId == idBill).AsQueryable();
            if (await billDetails.CountAsync() > 0)
                foreach (var billDetail in billDetails)
                {
                    var product = await _context.Products.Include(x=>x.ProductImgs).FirstOrDefaultAsync(x=>x.ProductId==billDetail.ProductId);
                    var billDetailAndProduct = new BillDetailAndProduct()
                    {
                        BillDetail = billDetail,
                        Product = product
                    };
                    billDetailAndProducts.Add(billDetailAndProduct);
                }

            var allBillDetails = new AllBillDetails()
            {
                Bill = bill,
                billDetailAndProducts = billDetailAndProducts,
                Sales = null
            };
            return allBillDetails;
        }

        public async Task<AllProperty> GetAllProperty()
        {
            var allProperty = new AllProperty();
            allProperty.categoryTypes = await _context.CategoryTypes.ToListAsync();
            allProperty.producers = await _context.Producers.ToListAsync();
            allProperty.brands = await _context.Brands.ToListAsync();
            allProperty.colors = await _context.Colors.ToListAsync();
            allProperty.sizes = await _context.Sizes.ToListAsync();
            allProperty.productStatuses = await _context.ProductStatuses.ToListAsync();
            return allProperty;
        }

        public async Task<AnalysisData> GetAnalysisData()
        {
            int productCountShelling = await _context.Products.Where(x => x.ProductStatusId == 1).CountAsync();
            int voucherCountUsing = await _context.Sales.Where(x => x.SalesStatusId == 1).CountAsync();
            int productQuantityInventory = await _context.Products.Where(x => x.ProductStatusId == 1).SumAsync(x => x.Quantity);
            int custumerCountActive = await _context.Accounts.Where(x => x.AccountStatusId == 1 && x.RoleId == 3).CountAsync();
            var query = from p in _context.Products
                        join bd in _context.BillDetails
                        on p.ProductId equals bd.ProductId
                        join b in _context.Bills
                        on bd.BillId equals b.BillId
                        where b.BillStatusId == 3 && b.CreateDate.Month == DateTime.Now.Month
                        orderby bd.Quantity descending
                        group new TopProductSold()
                        {
                            name = p.ProductName,
                            sold = bd.Quantity,
                            inventory = p.Quantity - bd.Quantity,
                            productCode = p.ProductDetail
                        } by p.ProductId;
            var top5ProductSolds = new List<TopProductSold>();
            return null;
        }

        public async Task<List<Product>> SearchTop5Product(string search)
        {
            return await _context.Products.Where(x => x.ProductName.Contains(search))
                                            .Take(5).Include(x => x.ProductImgs).ToListAsync();
        }
    }
}
