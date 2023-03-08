using Microsoft.AspNetCore.Mvc;
using patikaodev.CustomAttributes;
using patikaodev.DBOperations;

namespace patikaodev.Controllers;

[ApiController]
[Account("Admin")]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductsDbContext _context;
	public ProductsController (ProductsDbContext context)
    {
        _context = context;
    }

//    public static List<Product> ProductList = new List<Product>
//         {   
//             new Product{Id=1,Name="Kalem",Description="Kurşun",Price= 12,},
//             new Product{Id=2,Name="Kalem",Description="Tükenmez",Price= 13,}
//         };
   [HttpGet]
   public async Task<IActionResult> GetProducts()
   {
        var Products = _context.products.OrderBy(Product => Product.Id).ToList();
        if (Products.Count == 0)
        return BadRequest();
        return Ok(Products);
   }
    [HttpGet("{id}")]
    public Product GetProductsById(int id)
    {
        var product = _context.products.Where(product => product.Id == id).SingleOrDefault();
        return product;
    }
    [HttpPost]
        public IActionResult CreateProduct([FromBody] Product newProduct)
        {
            var Product = _context.products.SingleOrDefault(x=>x.Id==newProduct.Id);
            if (Product is not null)
                return BadRequest();
            _context.products.Add(newProduct);
            _context.SaveChanges();
            return Ok();
        }
    [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id ,[FromBody] Product updatedProduct)
        {
            var Product = _context.products.SingleOrDefault(x=>x.Id==id);
            if (Product is null)
                return BadRequest();

            Product.Id= updatedProduct.Id != default ? updatedProduct.Id :Product.Id;
            Product.Name= updatedProduct.Name != default ? updatedProduct.Name :Product.Name;
            Product.Description= updatedProduct.Description != default ? updatedProduct.Description :Product.Description;
            Product.Price= updatedProduct.Price != default ? updatedProduct.Price :Product.Price;

            _context.SaveChanges();

            return Ok();
        }
    [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var Product = _context.products.SingleOrDefault(x=>x.Id==id);
            if (Product is null)
                return NoContent();

            _context.products.Remove(Product);
            _context.SaveChanges();

            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProduct(int id, [FromBody] Product updatedProduct)
        {
            var Product = _context.products.SingleOrDefault(x=>x.Id==id);

            if (Product is null)
                return BadRequest();

            Product.Id= updatedProduct.Id != default ? updatedProduct.Id :Product.Id;
            Product.Name= updatedProduct.Name != default ? updatedProduct.Name :Product.Name;
            Product.Description= updatedProduct.Description != default ? updatedProduct.Description :Product.Description;
            Product.Price= updatedProduct.Price != default ? updatedProduct.Price :Product.Price;
            _context.SaveChanges();

            return Ok();

        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetToDescending()
        {
            var productList = _context.products.OrderByDescending(product => product.Name).ToList();
            return Ok(productList);
        }
}
