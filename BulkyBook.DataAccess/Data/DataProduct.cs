
using BulkyBook.Model;
using BulkyBook.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.Data
{
    public class DataProduct:DbContext
    {
        public DataProduct(DbContextOptions options) :base(options)
        {     
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
   
