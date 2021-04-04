using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Session;
using PizzeriaTNAI.Entities.Models;

namespace BusinessLogic.Services.SOrder
{
    public interface IOrderService
    {
        SessionManager SessionMenager { get; }

        Task<bool> DeleteOrderAsync(int id);

        Task<Order> GetOrderAsync(int id);

        Task<List<Order>> GetOrdersAsync();

        bool SaveOrder(Order newOrder, string userId);

        Task<bool> SaveOrderAsync(Order order, string userId);
    }
}