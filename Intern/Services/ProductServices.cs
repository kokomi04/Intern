//using Microsoft.EntityFrameworkCore;
using Intern.EF;
using Intern.Entities;
using Intern.ViewModels;
using Intern.ViewModels.Authen;
using Intern.ViewModels.ChangeAccount;
using Intern.ViewModels.Order;
using Intern.ViewModels.PagingCommon;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace Intern.Services
{
    public class ProductServices : IProductServices
    {
        private readonly MyDbContext _context;

        public ProductServices(MyDbContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public async Task<List<Product>> NextPage(int pageIndex)
        {
            var products = _context.Products.AsQueryable();

            var data = await products.Skip((5 * (pageIndex - 1))).Take(5).Include(x => x.ProductImgs).ToListAsync();

            return data;
        }

        public async Task<List<Product>> SearchProduct(string search)
        {
            var products = _context.Products.AsQueryable();
            if (!search.IsNullOrEmpty())
            {
                products = products.Where(x => x.ProductName.Contains(search)).Include(x => x.ProductImgs);
            }
            return await products.ToListAsync();
        }

        public async Task<List<Product>> GetProductHome()
        {
            return await _context.Products.Include(x => x.ProductImgs).ToListAsync();
        }
        public async Task<ProductDetail> GetProductId(int productId)
        {
            var product = await _context.Products.Include(x => x.ProductImgs).FirstOrDefaultAsync(x => x.ProductId == productId);
            if (product == null) return null;

            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.BrandId == product.BrandId);
            var producer = await _context.Producers.FirstOrDefaultAsync(x => x.ProducerId == product.ProducerId);
            var size = await _context.Sizes.FirstOrDefaultAsync(x => x.SizeId == product.SizeId);
            var color = await _context.Colors.FirstOrDefaultAsync(x => x.ColorId == product.ColorId);
            var category = await _context.CategoryTypes.FirstOrDefaultAsync(x => x.CategoryTypeId == product.CategoryTypeId);

            var productDetail = new ProductDetail()
            {
                product = product,
                brand = brand,
                producer = producer,
                size = size,
                color = color,
                categoryType = category
            };

            return productDetail;
        }

        public async Task AddProductToBag(AddProToBagRequest request)
        {
            using (var trans = await _context.Database.BeginTransactionAsync())
            {
                var accountBagId = 0;
                if (await _context.AccountBags.FirstOrDefaultAsync() != null)
                    accountBagId = await _context.AccountBags.MaxAsync(x => x.AccountBagId);
                var accountBag = new AccountBag()
                {
                    AccountBagId = accountBagId + 1,
                    AccountId = request.AccountId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                };
                await _context.AccountBags.AddAsync(accountBag);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();
            }
        }

        public async Task<List<GetProductBagResponse>> GetProductBagByAccountId(int accountId)
        {
            var result = new List<GetProductBagResponse>();
            var accountBag = await _context.AccountBags.Where(x => x.AccountId == accountId).ToListAsync();
            if (accountBag.Count == 0)
                return null;
            foreach (var item in accountBag)
            {
                var product = await _context.Products.Include(x => x.ProductImgs).FirstOrDefaultAsync(x => x.ProductId == item.ProductId);
                var categoryType = await _context.CategoryTypes.FindAsync(product.CategoryTypeId);
                var productBag = new GetProductBagResponse()
                {
                    Product = product,
                    CategoryType = categoryType,
                    AccountBag = item
                };
                result.Add(productBag);
            }
            return result;
        }
        public async Task<int> DeleteBag(int accountBagId)
        {
            var accountBag = await _context.AccountBags.FindAsync(accountBagId);
            if (accountBag == null)
                return 0;
            _context.AccountBags.Remove(accountBag);
            return await _context.SaveChangesAsync();
        }

        public async Task<AccountBag> UpdateAccountBagById(int[] request)
        {
            var accountBag = await _context.AccountBags.FindAsync(request[0]);
            if (accountBag == null)
                return null;
            accountBag.Quantity = request[1];
            _context.AccountBags.Update(accountBag);
            await _context.SaveChangesAsync();
            return accountBag;
        }

        public async Task<List<Product>> DressCategory()
        {
            var products = await _context.Products.Where(x => x.CategoryTypeId == 1).Include(x => x.ProductImgs).ToListAsync();

            return products;
        }
        public async Task<List<Product>> PanCategory()
        {
            var products = await _context.Products.Where(x => x.CategoryTypeId == 2).Include(x => x.ProductImgs).ToListAsync();

            return products;
        }
        public async Task<List<Product>> ShirtCategory()
        {
            var products = await _context.Products.Where(x => x.CategoryTypeId == 3).Include(x => x.ProductImgs).ToListAsync();

            return products;
        }

        public async Task<SignInResponse> CheckLogin(SignInRequest request)
        {
            var acc = _context.Accounts.FirstOrDefault(x => (x.AccountUserName == request.UserName) & x.AccountPassWord == request.userPass);
            if (acc == null)
                return null;
            var lstShipContacts = await _context.AccountShipContacts.Where(x => x.AccountId == acc.AccountId).ToListAsync();
            var kq = new SignInResponse()
            {
                Id = acc.AccountId,
                Name = acc.AccountName,
                Address = acc.AccountDetailAddress,
                Born = acc.AccountBorn,
                RoleID = acc.RoleId,
                Sdt = acc.Sdt,
                ShipContacts = lstShipContacts
            };
            return kq;
        }

        public async Task<SignUpRequest> CreateAccount(SignUpRequest request)
        {
            using (var trans = await _context.Database.BeginTransactionAsync())
            {
                int maxId = _context.Accounts.Max(x => x.AccountId);
                if (_context.Accounts.Any(x => x.AccountUserName == request.userName))
                {
                    request.status = 1;
                    return request;
                }

                var acc = new Account()
                {
                    AccountId = maxId + 1,
                    AccountUserName = request.userName,
                    AccountName = request.name,
                    AccountPassWord = request.userPass,
                    AccountStatusId = 1,
                    AccountCreateDate = DateTime.Now,
                    AccountStatus = await _context.AccountStatuses.FindAsync(1),
                    RoleId = 3,
                    Role = await _context.Roles.FindAsync(3),
                    CreateDate = DateTime.Now,
                    Sdt = request.sdt,
                };
                await _context.AddAsync(acc);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();
                return request;
            }

        }
        public async Task<AccountCustom> GetContacts(int accountId)
        {
            var acc = await _context.Accounts.FindAsync(accountId);
            if (acc == null)
                return null;
            var accShipContacts = await _context.AccountShipContacts.Where(x => x.AccountId == accountId).ToListAsync();
            var accCus = new AccountCustom()
            {
                Id = accountId,
                sdt = acc.Sdt,
                address = acc.AccountDetailAddress,
                born = acc.AccountBorn,
                name = acc.AccountName,
                roleID = acc.RoleId,
                shipContacts = accShipContacts,
            };
            return accCus;
        }

        public async Task<RepassResponse> RePass(RepassRequest request)
        {
            var acc = await _context.Accounts.FindAsync(request.accountId);
            if (acc == null)
                return new RepassResponse(1, "Tai khoan khong ton tai");
            if (acc.AccountPassWord != request.oldPass)
                return new RepassResponse(2, "Mat khau sai");
            acc.AccountPassWord = request.newPass;
            _context.Accounts.Update(acc);
            await _context.SaveChangesAsync();
            return new RepassResponse(3, "Thay doi mat khau thanh cong");
        }
        public async Task<ReInfoRequest> ReInfo(ReInfoRequest request)
        {
            var acc = await _context.Accounts.FindAsync(request.accountId);
            if (acc == null)
                return null;

            acc.AccountName = request.name;
            acc.AccountBorn = request.born;
            acc.AccountDetailAddress = request.address;
            acc.Sdt = request.sdt;

            _context.Accounts.Update(acc);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<AddAccShipContactRequest> AddNewAccountShipContact(AddAccShipContactRequest request)
        {
            int accShipContactMax = 0;
            if (await _context.AccountShipContacts.CountAsync() > 0)
                accShipContactMax = await _context.AccountShipContacts.MaxAsync(x => x.AccountShipContactId);
            var accShipContact = new AccountShipContact()
            {
                AccountShipContactId = accShipContactMax + 1,
                AccountId = request.accountId,
                AccountDetailAddress = request.accountDetailAddress,
                AccountPhoneNumber = request.accountPhoneNumber,
                ReceiverName = request.receiverName,
                AccountShipContactStatusId = 1,
                DistrictID = request.districtID,
                ProvinceID = request.provinceId,
                WardCode = request.wardCode,
            };
            await _context.AccountShipContacts.AddAsync(accShipContact);
            await _context.SaveChangesAsync();
            return request;
        }
        public async Task<int> DeleteShipContact(int idAccountShipContact)
        {
            var accountShipContact = await _context.AccountShipContacts.FindAsync(idAccountShipContact);
            if (accountShipContact == null)
                return 0;
            _context.AccountShipContacts.Remove(accountShipContact);
            return await _context.SaveChangesAsync();
        }

        public async Task<CreateOrder> GetCalculbag(int[] request)
        {
            var sales = await _context.Sales.Where(x => x.SalesStatusId == 1 || x.SalesStatusId == 2).ToListAsync();
            var buyMethods = await _context.BuyMethods.ToListAsync();
            var shipMethods = await _context.ShipMethods.ToListAsync();

            var aB = await _context.AccountBags.FindAsync(request[0]);
            var accountShipContacts = await _context.AccountShipContacts.Where(x => x.AccountId == aB.AccountId).ToListAsync();

            var orderItems = new List<OrderItem>();

            foreach (var id in request)
            {
                var accountBag = await _context.AccountBags.FindAsync(id);
                var product = await _context.Products.Include(x => x.ProductImgs).FirstOrDefaultAsync(x => x.ProductId == accountBag.ProductId);

                var orderItem = new OrderItem()
                {
                    accountBagId = id,
                    quantity = accountBag.Quantity,
                    product = product,
                    categoryType = await _context.CategoryTypes.FindAsync(product.CategoryTypeId)
                };
                orderItems.Add(orderItem);
            }

            var createOrder = new CreateOrder()
            {
                salesOfBill = sales,
                accountShipContacts = accountShipContacts,
                buyMethods = buyMethods,
                orderItems = orderItems,
                shipMethods = shipMethods,
            };

            return createOrder;
        }

        public async Task<bool> CreateBill(CreateBillRequest request)
        {
            using (var trans = await _context.Database.BeginTransactionAsync())
            {
                int maxBillId = 0;
                if (await _context.Bills.CountAsync() > 0)
                    maxBillId = await _context.Bills.MaxAsync(x => x.BillId);
                var bill = new Bill()
                {
                    BillId = maxBillId + 1,
                    BillCode = "HD" + maxBillId + 1,
                    ShipMethodId = request.ShipOptId,
                    ShipPrice = request.ShipPrice,
                    BuyMethodId = request.BuyOptId,
                    BillStatusId = 1,
                    BuyerNotification = request.BuyerNotification,
                    CreateDate = DateTime.UtcNow,
                    AccountShipContactId = request.AccountShipContactId,
                    TotalBill = 0
                };
                await _context.Bills.AddAsync(bill);
                await _context.SaveChangesAsync();

                if (request.ShipVoucher.HasValue)
                {
                    int maxBillSalesId = 0;
                    if (await _context.BillSales.CountAsync() > 0)
                        maxBillSalesId = await _context.BillSales.MaxAsync(x => x.BillSalesId);
                    var billSales = new BillSales()
                    {
                        BillSalesId = maxBillSalesId + 1,
                        BillId = bill.BillId,
                        SalesId = request.ShipVoucher,
                    };
                    await _context.BillSales.AddAsync(billSales);
                    await _context.SaveChangesAsync();
                }
                if (request.VoucherVoucher.HasValue)
                {
                    int maxBillSalesId = 0;
                    if (await _context.BillSales.CountAsync() > 0)
                        maxBillSalesId = await _context.BillSales.MaxAsync(x => x.BillSalesId);
                    var billSales = new BillSales()
                    {
                        BillSalesId = maxBillSalesId + 1,
                        BillId = bill.BillId,
                        SalesId = request.VoucherVoucher,
                    };
                    await _context.BillSales.AddAsync(billSales);
                    await _context.SaveChangesAsync();
                }

                int count = 0;
                double totalBill = 0;
                foreach (var accountBagId in request.AccountBags)
                {
                    count++;
                    var accountBag = await _context.AccountBags.FindAsync(accountBagId);
                    if (accountBag == null) return false;
                    var product = await _context.Products.FindAsync(accountBag.ProductId);
                    if (product == null) return false;
                    if (product.Quantity < accountBag.Quantity)
                        return false;
                    product.Quantity -= accountBag.Quantity;
                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();

                    totalBill += product.ShellPrice * accountBag.Quantity;

                    int maxBillDetailId = 0;
                    if (await _context.BillDetails.CountAsync() > 0)
                        maxBillDetailId = await _context.BillDetails.MaxAsync(x => x.BillDetailId);
                    var billDetail = new BillDetail()
                    {
                        BillDetailId = maxBillDetailId + count,
                        BillId = bill.BillId,
                        ProductId = accountBag.ProductId,
                        Price = accountBag.Quantity * product.Price,
                        Quantity = accountBag.Quantity,
                    };
                    await _context.BillDetails.AddAsync(billDetail);
                    _context.AccountBags.Remove(accountBag);
                    await _context.SaveChangesAsync();
                }

                bill.TotalBill = totalBill;
                _context.Bills.Update(bill);
                await _context.SaveChangesAsync();

                await trans.CommitAsync();
                return true;
            }
        }

        public async Task<List<GetOrderResponse>> GetBillDetailByAccountId(int accountId)
        {
            var result = new List<GetOrderResponse>();
            var accShipContacts = await _context.AccountShipContacts.Where(x => x.AccountId == accountId).ToListAsync();
            if (accShipContacts.Count == 0) return null;
            foreach (var accShipContact in accShipContacts)
            {
                var bills = await _context.Bills.Where(x => x.AccountShipContactId == accShipContact.AccountShipContactId).ToListAsync();
                if (bills.Count > 0)
                    foreach (var bill in bills)
                    {
                        var shipMethod = await _context.ShipMethods.FirstOrDefaultAsync(x => x.ShipMethodId == bill.ShipMethodId);
                        var buyMethod = await _context.BuyMethods.FirstOrDefaultAsync(x => x.BuyMethodId == bill.BuyMethodId);
                        var billStatus = await _context.BillStatuses.FirstOrDefaultAsync(x => x.BillStatusId == bill.BillStatusId);

                        var productBillDetails = new List<BillDetailAndProduct>();
                        var billDetails = await _context.BillDetails.Where(x => x.BillId == bill.BillId).ToListAsync();
                        foreach (var item in billDetails)
                        {
                            var product = await _context.Products.FindAsync(item.ProductId);
                            product.ProductImgs = await _context.ProductImgs.Where(x => x.ProductId == product.ProductId).ToListAsync();
                            productBillDetails.Add(new BillDetailAndProduct()
                            {
                                BillDetail = item,
                                Product = product,
                            });
                        }

                        var billSales = await _context.BillSales.Where(x => x.BillId == bill.BillId).Include(x => x.Sales).ToListAsync();
                        int? freeShip = 0, voucherSIXDO = 0;
                        if (billSales.Count > 0)
                            foreach (var item in billSales)
                            {
                                if (item.Sales.SaleTypeId == 1)
                                    freeShip = item.Sales.SalesPercent + item.Sales.SalesInt;
                                else if (item.Sales.SaleTypeId == 2)
                                    voucherSIXDO = item.Sales.SalesPercent + item.Sales.SalesInt;
                            }

                        result.Add(new GetOrderResponse()
                        {
                            ShipMethod = shipMethod,
                            AccountShipContact = accShipContact,
                            Bill = bill,
                            BillStatus = billStatus,
                            BuyMethod = buyMethod,
                            FreeShip = freeShip,
                            VoucherSIXDO = voucherSIXDO,
                            ProductBillDetails = productBillDetails,
                        });
                    }
            }
            return result;
        }

        public async Task<bool> CancelBill(int billId, int type)
        {
            var bill = await _context.Bills.Include(x => x.BillDetail).FirstOrDefaultAsync(x => x.BillId == billId);
            if (bill == null || bill.BillDetail.Count() == 0) return false;

            bill.BillStatusId = 4;
            bill.CloseDate = DateTime.Now;
            _context.Bills.Update(bill);
            await _context.SaveChangesAsync();

            foreach (var item in bill.BillDetail)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                    product.Quantity += item.Quantity;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }

            return true;
        }
    }
}
