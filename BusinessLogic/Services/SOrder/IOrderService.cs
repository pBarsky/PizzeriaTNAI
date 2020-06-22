using System.Collections.Generic;
using System.Threading.Tasks;
using PizzeriaTNAI.BusinessLogic.Session;
using PizzeriaTNAI.Entities.Models;

namespace BusinessLogic.Services.SOrder
{
    public interface IOrderService
    {
        SessionManager SessionMenager { get; }
        bool SaveOrder(Order newOrder, string userId);
        Task<Order> GetOrderAsync(int id);
        Task<List<Order>> GetOrdersAsync();
        Task<bool> SaveOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
    }
}