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
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyDetail> PropertyDetails { get; set; }
        public DbSet<ProductDetailPropertyDetail> ProductDetailPropertyDetails { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }

    }
}
