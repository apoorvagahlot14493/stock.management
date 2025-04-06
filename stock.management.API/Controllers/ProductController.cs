using Microsoft.AspNetCore.Mvc;
using stock.management.BusinessAccess.Interfaces;
using stock.management.BusinessAccess.Services;
using stock.management.DataAccess.DataModels;

namespace stock.management.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct(ProductDetail product)
        {
            try
            {
               var createdProduct= await _productService.CreateProductAsync(product);
                return Ok(createdProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product with ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDetail product)
        {
            try
            {
                if (id != product.ProductId)
                {
                    throw new ArgumentException("Product ID mismatch");
                }
                var updatedProduct = await _productService.UpdateProductAsync(id, product);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating product with ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting product with ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpPut("decrement-stock/{id}/{quantity}")]
        public async Task<IActionResult> DecrementStock(int id, int quantity)
        {
            try
            {
                var product = await _productService.DecrementStock(id, quantity);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpPut("add-to-stock/{id}/{quantity}")]
        public async Task<IActionResult> AddToStock(int id, int quantity)
        {
            try
            {
                var product = await _productService.AddToStock(id, quantity);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding to stock for product with ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }
    }
}