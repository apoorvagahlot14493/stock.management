using stock.management.DataAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock.management.BusinessAccess.Interfaces
{
    public interface IProductService
    {
        Task<ProductDetail> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDetail>> GetAllProductsAsync();
        Task<ProductDetail> CreateProductAsync(ProductDetail product);
        Task<ProductDetail> UpdateProductAsync(int id, ProductDetail product);
        Task DeleteProductAsync(int id);
    }
}
