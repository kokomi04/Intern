using Intern.EF;
using Intern.Entities;
using Intern.ViewModels;
using Intern.ViewModels.Analysis;
using Intern.ViewModels.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            var sales = await _context.Sales.ToListAsync();
            var allBillDetails = new AllBillDetails()
            {
                Bill = bill,
                billDetailAndProducts = billDetailAndProducts,
                Sales = sales,
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
            var query1 = from p in _context.Products
                        join bd in _context.BillDetails
                        on p.ProductId equals bd.ProductId
                        join b in _context.Bills
                        on bd.BillId equals b.BillId
                        where b.BillStatusId == 1 && b.CreateDate.Month == DateTime.Now.Month
                        group new { p, bd } by p.ProductId into t
                        select new TopProductSold()
                        {
                            name = t.FirstOrDefault().p.ProductName,
                            sold = t.Sum(x => x.bd.Quantity),
                            inventory = t.Sum(x => x.p.Quantity - x.bd.Quantity),
                            productCode = t.FirstOrDefault().p.ProductDetail
                        };
            var top5ProductSolds = await query1.Take(5).OrderByDescending(x=>x.sold).ToListAsync();

            var query2 = from a in _context.Accounts
                        join asc in _context.AccountShipContacts
                        on a.AccountId equals asc.AccountId
                        join b in _context.Bills
                        on asc.AccountShipContactId equals b.AccountShipContactId
                        join bd in _context.BillDetails
                        on b.BillId equals bd.BillId
                        where b.BillStatusId == 1 && b.CreateDate.Month == DateTime.Now.Month
                        group new { a, bd } by a.AccountId into t
                        select new TopAccountPaid()
                        {
                            accountCode = t.FirstOrDefault().a.AccountUserName,
                            sdt = t.FirstOrDefault().a.Sdt,
                            name = t.FirstOrDefault().a.AccountName,
                            totalPaid = t.Sum(x =>x.bd.Price)
                        };
            var top5AccountPaids = await query2.Take(5).OrderByDescending(x=>x.totalPaid).ToListAsync();

            var totalBillMonth = new List<int>();
            for (int i = 0; i < 12; i++)
            {
                var billMonth = await _context.Bills.CountAsync(x => x.CreateDate.Month == i + 1);
                totalBillMonth.Add(billMonth);
            }

            var analysisProfit12Month = new List<MonthAnalysis>();
            for (int i = 1; i <= 12; i++)
            {
                var bills = await _context.Bills.Where(x => x.CreateDate.Month == i && x.BillStatusId == 3).ToListAsync();
                int soldTotal = 0;
                int profitBefore = 0;
                foreach (var bill in bills)
                {
                    var totalProductOdBill = await _context.BillDetails.DistinctBy(x => x.ProductId).CountAsync();
                    soldTotal += totalProductOdBill;

                    int totalPriceBill = 0; ;
                    var billDetails = await _context.BillDetails.Where(x=>x.BillId==bill.BillId).Include(x=>x.Product).ToListAsync();
                    foreach (var billDetail in billDetails)
                    {
                        totalPriceBill += billDetail.Price;
                        profitBefore += billDetail.Product.Price * billDetail.Quantity - billDetail.Price;
                    }
                    var billSales = await _context.BillSales.Where(x=>x.BillId==bill.BillId).Include(x=>x.Sales).ToListAsync();

                    foreach (var item in billSales)
                    {
                        if(item.Sales.SaleTypeId == 1)
                        {
                            var freeShip = item.Sales.SalesInt;
                            profitBefore -= (int)freeShip;
                        }
                        else if(item.Sales.SaleTypeId == 2)
                        {
                            var voucher = item.Sales.SalesInt + item.Sales.SalesPercent;
                            if (voucher > 100)
                                profitBefore -= (int)voucher;
                            else
                                profitBefore -= (int)voucher * totalPriceBill / 100;
                        }
                    }
                }

                int? shipLossTotal = await _context.Bills.Where(x => x.CreateDate.Month == i && x.BillStatusId == 5).SumAsync(x => x.ShipPrice);
                var monthAnalysis = new MonthAnalysis()
                {
                    shippedBillTotal = bills.Count,
                    soldTotal = soldTotal,
                    backBillTotal = await _context.Bills.Where(x => x.CreateDate.Month == i && x.BillStatusId == 5).CountAsync(),
                    profitBefore = profitBefore,
                    shipLossTotal = shipLossTotal,
                    resultProfit = profitBefore - shipLossTotal,
                };
                analysisProfit12Month.Add(monthAnalysis);
            }

            var analysisData = new AnalysisData()
            {
                productCountShelling = productCountShelling,
                voucherCountUsing = voucherCountUsing,
                productQuantityInventory = productCountShelling,
                custumerCountActive = custumerCountActive,
                top5ProductSolds = top5ProductSolds,
                analysisProfit12Month = analysisProfit12Month,
                top5AccountPaids = top5AccountPaids,
                totalBillMonth = totalBillMonth,
            };
            return analysisData;
        }

        public async Task<List<Product>> SearchTop5Product(string? search)
        {
            return await _context.Products.Where(x => x.ProductName.Contains(search))
                                            .Take(5).Include(x => x.ProductImgs).ToListAsync();
        }
    }
}
