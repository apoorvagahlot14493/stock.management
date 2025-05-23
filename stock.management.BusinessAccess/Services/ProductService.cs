﻿using Microsoft.EntityFrameworkCore;
using stock.management.BusinessAccess.Interfaces;
using stock.management.DataAccess;
using stock.management.DataAccess.DataModels;
using stock.management.SharedLibrary.CommonUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock.management.BusinessAccess.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
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
            product.ProductId = CommonUtility.GenerateUniqueId();
            _context.ProductDetails.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<ProductDetail> UpdateProductAsync(int id, ProductDetail product)
        {
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
        public async Task<ProductDetail> DecrementStock(int id, int quantity)
        {
            var product = await _context.ProductDetails.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }
            if (product.Quantity < quantity)
            {
                throw new InvalidOperationException("Insufficient stock");
            }
            product.Quantity -= quantity;
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<ProductDetail> AddToStock(int id, int quantity)
        {
            var product = await _context.ProductDetails.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }
            product.Quantity += quantity;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
