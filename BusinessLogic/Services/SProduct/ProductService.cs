using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using PizzeriaTNAI.DataAccessLayer.Repositories.Interfaces;
using PizzeriaTNAI.Entities.Models;

namespace BusinessLogic.Services.SProduct
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<Product> GetProductAsync(int id)
        {
            return _productRepository.GetProductAsync(id);
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return _productRepository.GetProductsAsync();
        }

        public Task<bool> SaveProductAsync(Product product)
        {
            return _productRepository.SaveProductAsync(product);
        }

        public Task<bool> DeleteProductAsync(int id)
        {
            return _productRepository.DeleteProductAsync(id);
        }
    }
}
