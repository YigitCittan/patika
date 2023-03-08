using Microsoft.AspNetCore.Mvc;

namespace patikaodev.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{   public static List<Products> ProductList = new List<Products>
        {   
            new Products{Id=1,Name="Kalem",Description="Kurşun",Price= 12,},
            new Products{Id=2,Name="Kalem",Description="Tükenmez",Price= 13,}
        };
   [HttpGet]
   public async Task<IActionResult> GetProducts()
   {
        var Products = ProductList.OrderBy(Products => Products.Id).ToList();
        if (ProductList.Count == 0)
        return BadRequest();
        return Ok(ProductList);
   }
    [HttpGet("{id}")]
    public Products GetProductsById(int id)
    {
        var Products = ProductList.Find(Products => Products.Id == id);
        return Products;
    }
    [HttpPost]
        public IActionResult CreateProduct([FromBody] Products newProduct)
        {
            var Product = ProductList.SingleOrDefault(x=>x.Id==newProduct.Id);
            if (Product is not null)
                return BadRequest();
            ProductList.Add(newProduct);
            return Ok();
        }
    [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id ,[FromBody] Products updatedProduct)
        {
            var Product = ProductList.SingleOrDefault(x=>x.Id==id);
            if (Product is null)
                return BadRequest();

            Product.Id= updatedProduct.Id != default ? updatedProduct.Id :Product.Id;
            Product.Name= updatedProduct.Name != default ? updatedProduct.Name :Product.Name;
            Product.Description= updatedProduct.Description != default ? updatedProduct.Description :Product.Description;
            Product.Price= updatedProduct.Price != default ? updatedProduct.Price :Product.Price;


            return Ok();
        }
    [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var Product = ProductList.SingleOrDefault(x=>x.Id==id);
            if (Product is null)
                return NoContent();

            ProductList.Remove(Product);
            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProduct(int id, [FromBody] Products updatedProduct)
        {
            var Product = ProductList.SingleOrDefault(x=>x.Id==id);

            if (Product is null)
                return BadRequest();

            Product.Id= updatedProduct.Id != default ? updatedProduct.Id :Product.Id;
            Product.Name= updatedProduct.Name != default ? updatedProduct.Name :Product.Name;
            Product.Description= updatedProduct.Description != default ? updatedProduct.Description :Product.Description;
            Product.Price= updatedProduct.Price != default ? updatedProduct.Price :Product.Price;

            return Ok();

        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetToDescending()
        {
            var productList = ProductList.OrderByDescending(product => product.Name).ToList();
            return Ok(productList);
        }
}
