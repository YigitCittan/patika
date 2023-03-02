using Microsoft.AspNetCore.Mvc;
using patikaodev.CustomAttributes;
using patikaodev.DBOperations;
using patikaodev.Models;
using Microsoft.EntityFrameworkCore;
namespace patikaodev.ProductOperations.DeleteProduct
{
    public class DeleteProduct
    {
        private readonly ProductsDbContext _dbcontext;
        public int ProductID { get; set; }
        public DeleteProduct(ProductsDbContext dbcontext)
        {
            _dbcontext = dbcontext;

        }
        public void Handle()
        {
            var Product = _dbcontext.products.SingleOrDefault(x=>x.Id==ProductID);
            if (Product is null)
                throw new InvalidOperationException("Silinecek Ürün Bulunamadı");
            _dbcontext.products.Remove(Product);
            _dbcontext.SaveChanges();
        }
    }
}