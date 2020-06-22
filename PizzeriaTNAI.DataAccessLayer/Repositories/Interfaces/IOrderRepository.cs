using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaTNAI.Entities.Models;

namespace PizzeriaTNAI.DataAccessLayer.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderAsync(int id);
        Task<List<Order>> GetOrdersAsync();
        Task<bool> SaveOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(Order order);
    }
}
