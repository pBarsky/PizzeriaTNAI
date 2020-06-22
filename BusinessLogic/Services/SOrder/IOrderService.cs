using PizzeriaTNAI.BusinessLogic.Session;
using PizzeriaTNAI.Entities.Models;

namespace BusinessLogic.Services.SOrder
{
    public interface IOrderService
    {
        SessionManager SessionMenager { get; }
        Order CreateOrder(Order newOrder, string userId);
    }
}