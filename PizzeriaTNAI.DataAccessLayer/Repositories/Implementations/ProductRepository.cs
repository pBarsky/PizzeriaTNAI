using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaTNAI.DataAccessLayer.Repositories.Interface;
using PizzeriaTNAI.Entities.Models;

namespace PizzeriaTNAI.DataAccessLayer.Repositories.Implementations
{
    class ProductRepository : BaseRepository, IProductRepository
    {
        public async Task<Product> GetProductAsync(int id)
        {
            return await Context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await Context.Products.ToListAsync();
        }

        public async Task<bool> SaveProductAsync(Product product)
        {
            if (product == null)
                return false;
            try
            {
                Context.Entry(product).State = product.ProductId == default(int) ? EntityState.Added : EntityState.Modified;
                await Context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            if (product == null)
                return false;
            Context.Products.Remove(product);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
