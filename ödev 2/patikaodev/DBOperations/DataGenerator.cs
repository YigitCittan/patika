using Microsoft.EntityFrameworkCore;

namespace patikaodev.DBOperations
 {
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ProductsDbContext(serviceProvider.GetRequiredService<DbContextOptions<ProductsDbContext>>()))
            {
                if(context.products.Any())
                {
                    return;
                }
                context.products.AddRange(
                
                    new Product{Name="Kalem",Description="Kurşun",Price= 12,},
                    new Product{Name="Kalem",Description="Tükenmez",Price= 13,}
                );
                context.SaveChanges();
            }
        }
    }
 }