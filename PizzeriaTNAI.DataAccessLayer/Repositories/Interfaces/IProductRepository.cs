﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaTNAI.Entities.Models;

namespace PizzeriaTNAI.DataAccessLayer.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductAsync(int id);
        Task<List<Product>> GetProductsAsync();
        Task<bool> SaveProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
