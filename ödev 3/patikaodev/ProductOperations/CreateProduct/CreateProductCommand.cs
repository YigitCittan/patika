using Microsoft.AspNetCore.Mvc;
using patikaodev.CustomAttributes;
using patikaodev.DBOperations;
using patikaodev.Models;

namespace patikaodev.ProductOperations.CreateProduct
{
    public class CreateProductCommand
    {   
        public CreateProductModel Model { get; set; }
        private readonly ProductsDbContext _dbContext;
        public CreateProductCommand(ProductsDbContext dbContext)
        {
            _dbContext= dbContext;
        }
    public void Handle()
    {
        var Product = _dbContext.products.SingleOrDefault(x=>x.Name==Model.Name);
            if (Product is not null)
            throw new InvalidOperationException("Ürün zaten mevcut");
            Product = new Product();
            Product.Name = Model.Name;
            Product.GenreID = Model.GenreID;
            Product.Color = Model.Color;
            Product.Price = Model.Price;
            _dbContext.products.Add(Product);
            _dbContext.SaveChanges();
    }
    public class CreateProductModel
    {
        public String Name { get; set; }
        public int GenreID { get; set; }
        public String Color { get; set; }
        public decimal Price { get; set; }
    }
    }
}