using Microsoft.AspNetCore.Mvc;
using patikaodev.CustomAttributes;
using patikaodev.DBOperations;
using patikaodev.Models;
using Microsoft.EntityFrameworkCore;
namespace patikaodev.ProductOperations.UpdateProduct
{
    public class UpdateProductCommand
    {
        private readonly ProductsDbContext _context;
        public int ProductID { get; set; }
        public UpdateProductModel Model { get; set; }
        public UpdateProductCommand(ProductsDbContext context)
        {
            _context = context;

        }
        public void Handle()
        {
            var Product = _context.products.SingleOrDefault(x=>x.Id==ProductID);
            if (Product is null)
                throw new InvalidOperationException("Güncellenecek Ürün Bulunamadı");

            Product.Name= Model.Name != default ? Model.Name :Product.Name;
            Product.Price= Model.Price != default ? Model.Price :Product.Price;

            _context.SaveChanges();

        }
        public class UpdateProductModel
        {
            public String? Name { get; set; }
            public int Price { get; set; }
        }

    }
} 