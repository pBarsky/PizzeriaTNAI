using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaTNAI.DataAccessLayer.Repositories.Interfaces;
using PizzeriaTNAI.Entities.Models;

namespace PizzeriaTNAI.DataAccessLayer.Repositories.Implementations
{

    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public async Task<Order> GetOrderAsync(int id)
        {
            return await Context.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await Context.Orders.ToListAsync();
        }

        public async Task<bool> SaveOrderAsync(Order Order)
        {
            if (Order == null)
                return false;
            try
            {
                Context.Entry(Order).State = Order.OrderId == default(int) ? EntityState.Added : EntityState.Modified;
                await Context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await GetOrderAsync(id);
            if (order == null)
                return false;
            Context.Orders.Remove(order);

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

