using Microsoft.AspNetCore.Mvc;
using patikaodev.CustomAttributes;
using patikaodev.DBOperations;
using patikaodev.ProductOperations.CreateProduct;
using patikaodev.ProductOperations.DeleteProduct;
using patikaodev.ProductOperations.GetProducts;
using patikaodev.ProductOperations.GetProductsDetail;
using patikaodev.ProductOperations.UpdateProduct;
using static patikaodev.ProductOperations.CreateProduct.CreateProductCommand;
using static patikaodev.ProductOperations.UpdateProduct.UpdateProductCommand;

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
//    [HttpGet]
//    public async Task<IActionResult> GetProducts()
//    {
//         var Products = _context.products.OrderBy(Product => Product.Id).ToList();
//         if (Products.Count == 0)
//         return BadRequest();
//         return Ok(Products);
//    }
    [HttpGet]
    public IActionResult GetProducts()
    {
        GetProductsQuery query = new GetProductsQuery(_context);
        var result = query.Handle();
        return Ok(result);
        
    }
    [HttpGet("{id}")]
    public IActionResult GetProductsById(int id)
    {   
        ProductsDetailViewModel result;
        try
        {
            GetProductsById query = new GetProductsById(_context);
            query.ProductID = id;
            result = query.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(result);
         
    }
    [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductModel newProduct)
        {
            CreateProductCommand command = new CreateProductCommand(_context);

            try
            {
            command.Model = newProduct;
            command.Handle();  
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
           

            return Ok();
        }
    [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id ,[FromBody] UpdateProductModel updatedProduct)
        {
            try
            {
                UpdateProductCommand command = new UpdateProductCommand(_context);
                command.ProductID = id;
                command.Model = updatedProduct;
                command.Handle();
            }
            catch (Exception ex)
            {
              return BadRequest(ex.Message);
            }
            
            return Ok();
        }
    [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
              DeleteProduct command = new DeleteProduct(_context);
            command.ProductID = id;
            command.Handle();  
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

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
            Product.Color= updatedProduct.Color != default ? updatedProduct.Color :Product.Color;
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
