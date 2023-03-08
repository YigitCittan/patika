
using Microsoft.AspNetCore.Mvc;
using patikaodev.CustomAttributes;
using patikaodev.DBOperations;
using patikaodev.Models;
using Microsoft.EntityFrameworkCore;
namespace patikaodev.ProductOperations.GetProductsDetail {
    public class GetProductsById {
        private readonly ProductsDbContext _dbContext;
        public int ProductID { get; set; }
       public GetProductsById(ProductsDbContext dbContext)
        {   
            _dbContext = dbContext;
        } 
            public ProductsDetailViewModel Handle()
{
            var product = _dbContext.products.Where(product => product.Id == ProductID ).SingleOrDefault();
            if(product is null)
            throw new InvalidOperationException("Ürün bulunamadı");
            ProductsDetailViewModel VM = new ProductsDetailViewModel();
            VM.Name = product.Name;
            VM.Genre = product.Genre;
            VM.Color = product.Color;
            VM.Price = product.Price;
            return(VM);
        }
    }
     public class ProductsDetailViewModel
    {
        public string? Name { get; set; }
        public string? Genre { get; set; }
        public string? Color { get; set; }
        public decimal Price { get; set; }
        
    }
}