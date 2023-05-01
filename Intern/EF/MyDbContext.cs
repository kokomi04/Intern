using Intern.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Intern.EF
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImgs> ProductImgs { get; set; }
        public DbSet<CategoryType> CategoryTypes { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductStatus> ProductStatuses { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BuyMethod> BuyMethods { get; set; }
        public DbSet<ShipMethod> ShipMethods { get; set; }
        public DbSet<BillStatus> BillStatuses { get; set; }
        public DbSet<BillSales> BillSales { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<SalesType> SalesTypes { get; set; }
        public DbSet<SalesStatus> SalesStatuses { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<VoteStar> VoteStars { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountStatus> AccountStatuses { get; set; }
        public DbSet<AccountBag> AccountBags { get; set; }
        public DbSet<AccountShipContact> AccountShipContacts { get; set; }
        public DbSet<AccountShipContactStatus> AccountShipContactStatuses { get; set; }


    }
}
