using Microsoft.EntityFrameworkCore;

namespace patikaodev.DBOperations
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options):base(options)
        {

        }
        public DbSet<Product> products { get; set; }
        
    }
}