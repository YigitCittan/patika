using Microsoft.AspNetCore.Mvc;
using patikaodev.CustomAttributes;
using patikaodev.DBOperations;
using patikaodev.Models;

namespace patikaodev.ProductOperations.GetProducts
{
    public class GetProductsQuery
    {
        private readonly ProductsDbContext _dbContext;
        public GetProductsQuery(ProductsDbContext dbContext)
        {
            _dbContext = dbContext;
        } 

        public List<ProductsViewModel> Handle()
        {
            var Products = _dbContext.products.OrderBy(Product => Product.Id).ToList<Product>();
            List<ProductsViewModel> VM = new List<ProductsViewModel>();
            foreach (var Product in Products)
            {
                VM.Add(new ProductsViewModel()
                {
                    Name = Product.Name,
                    Genre = ((Genreenum)Product.GenreID).ToString(),
                    Color = Product.Color,
                    Price = Product.Price,
                });
            }
            return (VM);
        }
    }

    public class ProductsViewModel
    {
        public string? Name { get; set; }
        public string Genre { get; set; }
        public string?Color { get; set; }
        public decimal Price { get; set; }
        
    }

}