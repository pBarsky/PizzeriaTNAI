using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Services.SBasket;
using BusinessLogic.Session;
using PizzeriaTNAI.DataAccessLayer.Repositories.Implementations;
using PizzeriaTNAI.DataAccessLayer.Repositories.Interfaces;
using PizzeriaTNAI.Entities.Models;

namespace BusinessLogic.Services.SOrder
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketMenager;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = new OrderRepository();
            SessionMenager = new SessionManager();
            _basketMenager = new BasketService(SessionMenager, productRepository);
        }

        public SessionManager SessionMenager { get; }

        public Task<bool> DeleteOrderAsync(int id)
        {
            return _orderRepository.DeleteOrderAsync(id);
        }

        public Task<Order> GetOrderAsync(int id)
        {
            return _orderRepository.GetOrderAsync(id);
        }

        public Task<List<Order>> GetOrdersAsync()
        {
            return _orderRepository.GetOrdersAsync();
        }

        public bool SaveOrder(Order newOrder, string userId)
        {
            return Task.FromResult(SaveOrderAsync(newOrder, userId)).Result.Result;
        }

        public async Task<bool> SaveOrderAsync(Order order, string userId)
        {
            order.DateOfCreation = DateTime.Now;
            order.UserId = userId;

            if (order.Items == null)
            {
                order.Items = new List<OrderItem>();
            }

            MakeOrderFromBasket(order);

            var result = await _orderRepository.SaveOrderAsync(order);
            return result;
        }

        private void MakeOrderFromBasket(Order order)
        {
            var basketValue = 0m;
            var basket = _basketMenager.GetBasket();
            foreach (var item in basket)
            {
                var orderItem = new OrderItem()
                {
                    ProductId = item.Product.ProductId,
                    Amount = item.Amount,
                    Price = item.Price
                };
                basketValue += (item.Amount * item.Price);
                order.Items.Add(orderItem);
            }

            order.OverallPrice = basketValue;
        }
    }
}