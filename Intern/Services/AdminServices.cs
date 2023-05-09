using Intern.EF;
using Intern.Entities;
using Intern.ViewModels;
using Intern.ViewModels.Analysis;
using Intern.ViewModels.BillAdmin;
using Intern.ViewModels.Order;
using Intern.ViewModels.RemakeAdmin;
using Intern.ViewModels.SaleAdmin;
using Intern.ViewModels.Upload;
using Microsoft.AspNetCore.Mvc;
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
                    BillCode = "HD" + (maxBillId + 1),
                    BillStatusId = 7,
                    CreateDate = DateTime.Now,
                    BuyMethodId = 4,
                    ShipPrice = 0,
                };

                await _context.Bills.AddAsync(bill);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();
                return bill;
            }
        }
        public async Task<int> Upload(UploadRequest request, Data data)
        {
            using (var trans = await _context.Database.BeginTransactionAsync())
            {
                byte[] file1, file2;
                using (var stream = new MemoryStream())
                {
                    await request.file1.CopyToAsync(stream);
                    file1 = stream.ToArray();
                    await request.file2.CopyToAsync(stream);
                    file2 = stream.ToArray();
                }

                var maxProductId = 0;
                if (await _context.Products.CountAsync() > 0)
                    maxProductId = await _context.Products.MaxAsync(x => x.ProductId);
                var product = new Product()
                {
                    ProductId = maxProductId + 1,
                    Price = data.ProductPrice,
                    ShellPrice = data.ProductShellPrice,
                    BrandId = data.Brand,
                    ColorId = data.Color,
                    ProducerId = data.Producer,
                    ProductName = data.ProductName,
                    ProductDetail = data.ProductDetail,
                    SizeId = data.Size,
                    Quantity = data.ProductQuantity,
                    CategoryTypeId = data.category,
                    ProductStatusId = 1,
                    CreatedDate = DateTime.Now,
                    ProductImgs = new List<ProductImgs>()
                    {
                    new ProductImgs() {ProductImg = file1},
                    new ProductImgs() {ProductImg = file2},
                    }
                };
                await _context.Products.AddAsync(product);
                var result = await _context.SaveChangesAsync();
                await trans.CommitAsync();
                return result;
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
                    var product = await _context.Products.Include(x => x.ProductImgs).FirstOrDefaultAsync(x => x.ProductId == billDetail.ProductId);
                    var billDetailAndProduct = new BillDetailAndProduct()
                    {
                        BillDetail = billDetail,
                        Product = product
                    };
                    billDetailAndProducts.Add(billDetailAndProduct);
                }
            var sales = await _context.Sales.Where(x=>x.SaleTypeId==2).ToListAsync();
            var allBillDetails = new AllBillDetails()
            {
                Bill = bill,
                billDetailAndProductList = billDetailAndProducts,
                Sales = sales,
            };
            return allBillDetails;
        }
        public async Task<int> AddProduct2BillDetail(int idProduct, int idBill)
        {
            int maxBillDetailId = 0;
            if (await _context.BillDetails.CountAsync() > 0)
                maxBillDetailId = await _context.BillDetails.MaxAsync(x => x.BillDetailId);

            var billDetail = new BillDetail()
            {
                BillDetailId = maxBillDetailId + 1,
                BillId = idBill,
                ProductId = idProduct,
            };
            int priceProduct = _context.Products.FirstOrDefault(x => x.ProductId == billDetail.ProductId).ShellPrice;
            billDetail.Price = priceProduct * billDetail.Quantity;

            await _context.BillDetails.AddAsync(billDetail);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateBillDetailQuantity(int idBillDetail, int quantity)
        {
            var bd = await _context.BillDetails.FindAsync(idBillDetail);
            if (bd == null) return 0;
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == bd.ProductId);
            bd.Quantity = quantity;
            bd.Price = quantity * product.ShellPrice;
            _context.BillDetails.Update(bd);
            await _context.SaveChangesAsync();

            var b = await _context.Bills.FindAsync(bd.BillId);
            b.TotalBill = await _context.BillDetails.Where(x => x.BillId == b.BillId).SumAsync(x => x.Price);
            _context.Bills.Update(b);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DelBillDetail(int idBillDetail)
        {
            var bd = await _context.BillDetails.FindAsync(idBillDetail);
            if(bd==null) return 0;

            _context.BillDetails.Remove(bd);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdatePayBill(BillPayRequest request)
        {
            using(var trans = await _context.Database.BeginTransactionAsync())
            {
                var bill = await _context.Bills.FindAsync(request.idBill);
                if (bill == null) return 0;
                bill.BuyMethodId = request.idBuyMethod;
                bill.IdEmployee = request.idEmployee;
                bill.CloseDate = DateTime.Now;
                _context.Bills.Update(bill);

                int maxBillSaleId = 0;
                if (await _context.BillSales.CountAsync() > 0)
                    maxBillSaleId = await _context.BillSales.MaxAsync(x => x.BillSalesId);
                var bs = new BillSales()
                {
                    BillId = request.idBill,
                    SalesId = request.idVoucher,
                    BillSalesId = maxBillSaleId + 1,
                };
                await _context.BillSales.AddAsync(bs);

                var result = await _context.SaveChangesAsync();
                await trans.CommitAsync();
                return result;
            }
        }
        public async Task<PrintBillResponse> PrintBill(int idBill)
        {
            var bill = await _context.Bills.Include(x=>x.Employee).FirstOrDefaultAsync(x=>x.BillId==idBill);
            if (bill == null) return null;
            bill.BillStatusId = 3;
            _context.Bills.Update(bill);
            await _context.SaveChangesAsync();

            var billDetailPrints = new List<BillDetailPrint>();
            var bds = await _context.BillDetails.Where(x=>x.BillId== idBill).AsQueryable().ToListAsync();
            if (bds.Count() == 0) return null;
            foreach (var bd in bds)
            {
                var p = await _context.Products.FindAsync(bd.ProductId);
                var bdp = new BillDetailPrint()
                {
                    productName = p.ProductName,
                    price = p.ShellPrice,
                    quantity = bd.Quantity
                };
                billDetailPrints.Add(bdp);
            }

            var pbr = new PrintBillResponse()
            {
                billCode = bill.BillCode,
                closeDate = DateTime.Now.Date,
                freeShip = 0,
                shipPrice = 0,
                employeeName = bill.Employee != null ? bill.Employee.AccountName : null,
                total = bill.TotalBill,
                totalResult = bill.TotalBill,
                billDetailPrints = billDetailPrints,
            };
            return pbr;
        }
        public async Task<int> AdminSetBill(int opt, int idBill, int idEmployee)
        {
            var bill = await _context.Bills.FindAsync(idBill);
            if (bill == null) return 0;
            bill.IdEmployee = idEmployee;

            if (opt == 2) //confirm
            {
                bill.BillStatusId = 2;
                _context.Bills.Update(bill);
            }
            else if(opt == 1) //cancel
            {
                bill.BillStatusId = 6;
                _context.Bills.Update(bill);
            }
            return await _context.SaveChangesAsync();
        }
        public async Task<List<Bill>> GetAllHoldingBill()
        {
            var bills = await _context.Bills.Where(x => x.BillStatusId == 7).Include(x => x.BillDetail).ToListAsync();
            return bills;
        }
        public async Task<Product> FindProductById(int idProduct)
        {
            var product = await _context.Products.Include(x=>x.ProductImgs).FirstOrDefaultAsync(x=>x.ProductId==idProduct);
            if (product == null) return null;
            return product;
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
            var top5ProductSolds = await query1.Take(5).OrderByDescending(x => x.sold).ToListAsync();

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
                             totalPaid = t.Sum(x => x.bd.Price)
                         };
            var top5AccountPaids = await query2.Take(5).OrderByDescending(x => x.totalPaid).ToListAsync();

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
                    var totalProductOfBill = await _context.BillDetails.Where(x=>x.BillId==bill.BillId).CountAsync();
                    soldTotal += totalProductOfBill;

                    int totalPriceBill = 0; ;
                    var billDetails = await _context.BillDetails.Where(x => x.BillId == bill.BillId).Include(x => x.Product).ToListAsync();
                    foreach (var billDetail in billDetails)
                    {
                        totalPriceBill += billDetail.Price;
                        profitBefore += billDetail.Price - billDetail.Product.Price * billDetail.Quantity;
                    }
                    var billSales = await _context.BillSales.Where(x => x.BillId == bill.BillId).Include(x => x.Sales).ToListAsync();

                    foreach (var item in billSales)
                    {
                        if (item.Sales.SaleTypeId == 1)
                        {
                            var freeShip = item.Sales.SalesInt;
                            profitBefore -= (int)freeShip;
                        }
                        else if (item.Sales.SaleTypeId == 2)
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
                productQuantityInventory = productQuantityInventory,
                custumerCountActive = custumerCountActive,
                top5ProductSold = top5ProductSolds,
                analysisProfit12Month = analysisProfit12Month,
                top5AccountPaid = top5AccountPaids,
                totalBillMonth = totalBillMonth,
            };
            return analysisData;
        }

        public async Task<Product> RemakeProduct(RemakeProduct product)
        {
            var p = await _context.Products.FindAsync(product.ProductId);
            if (p == null) return null;

            p.ShellPrice = product.ShellPrice;
            p.BrandId = product.BrandId;
            p.CategoryTypeId = product.CategoryTypeId;
            p.ColorId = product.ColorId;
            p.ProducerId = product.ProducerId;
            p.SizeId = product.SizeId;
            p.ProductName = product.ProductName;
            p.ProductStatusId = product.ProductStatusId;
            p.Quantity = product.Quantity;
            p.ProductDetail = product.ProductDetail;
            p.UpdatedDate = DateTime.Now;

            _context.Products.Update(p);
            await _context.SaveChangesAsync();
            return p;
        }
        public async Task<int> CreateProperty(CreateAndRemakeProperty request)
        {
            switch (request.Type)
            {
                case 1:
                    {
                        int maxColorId = 0;
                        if (await _context.Colors.CountAsync() > 0)
                            maxColorId = await _context.Colors.MaxAsync(x => x.ColorId);

                        var color = new Color()
                        {
                            ColorId = maxColorId + 1,
                            ColorDetail = request.Detail,
                            ColorCode = request.Code,
                        };
                        _context.Colors.Add(color);
                    }
                    break;
                case 2:
                    {
                        int maxProducerId = 0;
                        if (await _context.Producers.CountAsync() > 0)
                            maxProducerId = await _context.Producers.MaxAsync(x => x.ProducerId);

                        var producer = new Producer()
                        {
                            ProducerId = maxProducerId + 1,
                            ProducerDetail = request.Detail,
                            ProducerCode = request.Code,
                        };
                        _context.Producers.Add(producer);
                    }
                    break;
                case 3:
                    {
                        int maxBrandId = 0;
                        if (await _context.Brands.CountAsync() > 0)
                            maxBrandId = await _context.Brands.MaxAsync(x => x.BrandId);

                        var brand = new Brand()
                        {
                            BrandId = maxBrandId + 1,
                            BrandDetail = request.Detail,
                            BrandCode = request.Code,
                        };
                        _context.Brands.Add(brand);
                    }
                    break;
                case 4:
                    {
                        int maxSizeId = 0;
                        if (await _context.Sizes.CountAsync() > 0)
                            maxSizeId = await _context.Sizes.MaxAsync(x => x.SizeId);

                        var size = new Size()
                        {
                            SizeId = maxSizeId + 1,
                            SizeDetail = request.Detail,
                            SizeCode = request.Code,
                        };
                        _context.Sizes.Add(size);
                    }
                    break;
                default:
                    break;
            }
            return await _context.SaveChangesAsync();
        }
        public async Task<int> RemakeProperty(CreateAndRemakeProperty request)
        {
            switch (request.Type)
            {
                case 1:
                    {
                        if (request.Id != null)
                        {
                            var cl = await _context.Colors.FindAsync(request.Id);
                            if (cl == null) return 0;
                            cl.ColorDetail = request.Detail;
                            cl.ColorCode = request.Code;
                            _context.Update(cl);
                        }
                    }
                    break;
                case 2:
                    {
                        if (request.Id != null)
                        {
                            var producer = await _context.Producers.FindAsync(request.Id);
                            if (producer == null) return 0;
                            producer.ProducerDetail = request.Detail;
                            producer.ProducerCode = request.Code;
                            _context.Update(producer);
                        }
                    }
                    break;
                case 3:
                    {
                        if (request.Id != null)
                        {
                            var brand = await _context.Brands.FindAsync(request.Id);
                            if (brand == null) return 0;
                            brand.BrandDetail = request.Detail;
                            brand.BrandCode = request.Code;
                            _context.Update(brand);
                        }
                    }
                    break;
                default:
                    break;
                case 4:
                    {
                        if (request.Id != null)
                        {
                            var size = await _context.Sizes.FindAsync(request.Id);
                            if (size == null) return 0;
                            size.SizeDetail = request.Detail;
                            size.SizeCode = request.Code;
                            _context.Update(size);
                        }
                    }
                    break;
            }
            return await _context.SaveChangesAsync();
        }
        public async Task<List<Product>> SearchTop5Product(string? search)
        {
            return await _context.Products.Where(x => x.ProductName.Contains(search))
                                            .Take(5).Include(x => x.ProductImgs).ToListAsync();
        }

        public async Task<List<GetBillTypeRequest>> GetAllBillType(int opt)
        {
            var result = new List<GetBillTypeRequest>();
            var listBill = new List<Bill>();
            string buyStatus = null, shipStatus = null;

            switch (opt)
            {
                case 1:
                    listBill = await _context.Bills.Where(x => x.BillStatusId == 1).ToListAsync();
                    buyStatus = "Chưa Thanh Toán";
                    shipStatus = "Đơn chờ";
                    break;
                case 2:
                    listBill = await _context.Bills.Where(x => x.BillStatusId == 2).ToListAsync();
                    buyStatus = "Chưa Thanh Toán";
                    shipStatus = "Đang giao";
                    break;
                case 3:
                    listBill = await _context.Bills.Where(x => x.BillStatusId == 3 || x.BillStatusId == 4 || x.BillStatusId == 5).ToListAsync();
                    buyStatus = "Đã Thanh Toán";
                    shipStatus = "Đã Nhận Hàng";
                    break;
                default:
                    break;
            }

            if (listBill.Count == 0) return null;

            foreach (var bill in listBill)
            {
                var asc = await _context.AccountShipContacts.FindAsync(bill.AccountShipContactId);
                var shipMethod = await _context.ShipMethods.FindAsync(bill.ShipMethodId);
                var buyMethod = await _context.BuyMethods.FindAsync(bill.BuyMethodId);
                var voucherShip = await _context.BillSales.Include(x => x.Sales).Where(x => x.BillId == bill.BillId && x.Sales.SaleTypeId == 1).FirstOrDefaultAsync();
                var voucher = await _context.BillSales.Include(x => x.Sales).Where(x => x.BillId == bill.BillId && x.Sales.SaleTypeId == 2).FirstOrDefaultAsync();
                var billStatus = await _context.BillStatuses.FindAsync(bill.BillStatusId);

                var billDetails = await _context.BillDetails.Where(x => x.BillId == bill.BillId).Include(x => x.Product).ToListAsync();

                var billDetailAnalyses = new List<BillDetailAnalyses>();
                foreach (var bd in billDetails)
                {
                    var bda = new BillDetailAnalyses()
                    {
                        shellprice = bd.Product.ShellPrice,
                        billDetailId = bd.BillDetailId,
                        productName = bd.Product.ProductName,
                        quantityInventory = bd.Product.Quantity,
                        quantity = bd.Quantity,
                        total = bd.Price,
                        productImg = await _context.ProductImgs.Where(x => x.ProductId == bd.ProductId).Select(x => x.ProductImg).FirstOrDefaultAsync(),
                    };
                    billDetailAnalyses.Add(bda);
                }

                var getBillTypeRequest = new GetBillTypeRequest()
                {
                    billId = bill.BillId,
                    billCode = bill.BillCode,
                    CreateBill = bill.CreateDate,
                    customerName = asc != null ? await _context.Accounts.Where(x => x.AccountId == asc.AccountId).Select(x => x.AccountName).FirstOrDefaultAsync() : null,
                    reveceiName = asc!=null? asc.ReceiverName : null,
                    reveceiSdt = asc != null ? asc.AccountPhoneNumber : null,
                    shipMethodName = shipMethod.ShipMethodName,
                    voucherCode = voucher != null ? voucher.Sales.SalesCode : null,
                    voucherShipCode = voucherShip != null ? voucherShip.Sales.SalesCode : null,
                    shipPrice = bill.ShipPrice,
                    notification = bill.BuyerNotification,
                    totalBill = await _context.BillDetails.Where(x => x.BillId == bill.BillId).SumAsync(x => x.Price),
                    shipVoucher = 1000, //voucherShip!=null? voucherShip.Sales.SalesInt : null,
                    voucherVoucher = voucher != null ? voucher.Sales.SalesInt + voucher.Sales.SalesPercent : null,
                    billStatus = billStatus.BillStatusDetail,
                    buyMethod = buyMethod.BuyMethodName,
                    buyStatus = buyStatus,
                    reveceiMethod = shipMethod.ShipMethodName,
                    shipStatus = shipStatus,
                    reveceiContact = asc != null ? asc.AccountPhoneNumber : null,
                    billStatusId = billStatus.BillStatusId,
                    billDetailAnalyses = billDetailAnalyses
                };
                result.Add(getBillTypeRequest);
            }
            return result;
        }

        public async Task<GetSaleResponse> GetSales()
        {
            var sales = _context.Sales.AsQueryable();
            var shipVouchers = await sales.Where(x => x.SaleTypeId == 1).ToListAsync();
            var voucherVouchers = await sales.Where(x => x.SaleTypeId == 2).ToListAsync();
            return new GetSaleResponse()
            {
                shipVouchers = shipVouchers,
                voucherVouchers = voucherVouchers,
            };
        }
        public async Task<int> CreateSales(CreateSaleRequest request)
        {
            int maxSalesId = 0;
            if (await _context.Sales.CountAsync() > 0)
                maxSalesId = await _context.Sales.MaxAsync(x => x.SalesId);

            var sales = new Sales()
            {
                SalesId = maxSalesId + 1,
                OpenDate = request.OpenDate,
                EndDate = request.EndDate,
                SalesCode = request.SalesCode,
                SalesInt = request.SalesInt,
                SalesName = request.SalesName,
                SalesPercent = request.SalesPercent,
                SaleTypeId = request.SaleTypeId,
                SalesStatusId = 1
            };
            await _context.AddAsync(sales);
            return await _context.SaveChangesAsync();
        }
    }
}
