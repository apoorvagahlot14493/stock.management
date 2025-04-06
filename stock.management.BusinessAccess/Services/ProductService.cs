using Microsoft.EntityFrameworkCore;
using stock.management.BusinessAccess.Interfaces;
using stock.management.DataAccess;
using stock.management.DataAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock.management.BusinessAccess.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext _context;

        public ProductService(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDetail> GetProductByIdAsync(int id)
        {
            return await _context.ProductDetails.FindAsync(id);
        }

        public async Task<IEnumerable<ProductDetail>> GetAllProductsAsync()
        {
            return await _context.ProductDetails.ToListAsync();
        }

        public async Task<ProductDetail> CreateProductAsync(ProductDetail product)
        {
            product.ProductId = new Random().Next(100000, 999999); // Example ID generation
            _context.ProductDetails.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<ProductDetail> UpdateProductAsync(int id, ProductDetail product)
        {
            if (id != product.ProductId)
            {
                throw new ArgumentException("Product ID mismatch");
            }

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.ProductDetails.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            _context.ProductDetails.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
