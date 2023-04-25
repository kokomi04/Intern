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
                Sdt = "0333961530",
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
                sdt = "",
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
                AccountShipContactStatusId = 1
            };
            await _context.AccountShipContacts.AddAsync(accShipContact);
            await _context.SaveChangesAsync();
            return request;
        }

        public Task<AccountCustom> GetCalculbag()
        {
            throw new NotImplementedException();
        }
    }
}
