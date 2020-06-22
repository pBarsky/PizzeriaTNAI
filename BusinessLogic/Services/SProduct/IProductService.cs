using System.Collections.Generic;
using System.Threading.Tasks;
using PizzeriaTNAI.Entities.Models;

namespace BusinessLogic.Services.SProduct
{
    public interface IProductService
    {
        Task<Product> GetProductAsync(int id);
        Task<List<Product>> GetProductsAsync();
        Task<bool> SaveProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}