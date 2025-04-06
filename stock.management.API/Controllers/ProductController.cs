using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stock.management.BusinessAccess.Interfaces;
using stock.management.DataAccess;
using stock.management.DataAccess.DataModels;


namespace stock.management.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct(ProductDetail product)
        {
            var createdProduct = await _productService.CreateProductAsync(product);
            return Ok(createdProduct);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDetail product)
        {
            var updatedProduct = await _productService.UpdateProductAsync(id, product);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok();
        }
        [HttpPut("decrement-stock/{id}/{quantity}")]
        public async Task<IActionResult> DecrementStock(int id, int quantity)
        {
            var product = await _productService.DecrementStock(id, quantity);
            return Ok(product);
            //var product = await _context.ProductDetails.FindAsync(id);
            //if (product == null)
            //{
            //    return NotFound();
            //}

            //product.Quantity -= quantity;
            //await _context.SaveChangesAsync();
            //return Ok();
        }

        [HttpPut("add-to-stock/{id}/{quantity}")]
        public async Task<IActionResult> AddToStock(int id, int quantity)
        {
            var product = await _productService.AddToStock(id, quantity);
            return Ok(product);
            //var product = await _context.ProductDetails.FindAsync(id);
            //if (product == null)
            //{
            //    return NotFound();
            //}

            //product.Quantity += quantity;
            //await _context.SaveChangesAsync();
            //return Ok();
        }
    }
}
