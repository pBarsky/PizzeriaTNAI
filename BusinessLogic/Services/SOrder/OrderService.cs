﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaTNAI.BusinessLogic.Services.SBasket;
using PizzeriaTNAI.DataAccessLayer.Repositories.Implementations;
using PizzeriaTNAI.DataAccessLayer.Repositories.Interfaces;
using PizzeriaTNAI.Entities.Models;

namespace BusinessLogic.Services.SOrder
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketMenager;
        // private ISessionManager _sessionMenager;
        private readonly IOrderRepository _orderRepository;
        public OrderService(IBasketService basketService, IOrderRepository orderRepository)
        {
            _orderRepository = new OrderRepository();
            // _sessionMenager = session;
            _basketMenager = basketService;
        }
        public Order CreateOrder(Order newOrder, string userId)
        {
            var basket = _basketMenager.GetBasket();
            newOrder.DateOfCreation = DateTime.Now;
            newOrder.UserId = userId;
            var result = Task.Run(() => _orderRepository.SaveOrderAsync(newOrder)).Result;
            if (newOrder.Items == null)
            {
                newOrder.Items = new List<OrderItem>();
            }
            var basketValue = 0m;

            foreach (var item in basket)
            {
                var newOrderItem = new OrderItem()
                {
                    ProductId = item.Product.ProductId,
                    Amount = item.Amount,
                    Price = item.Price
                };
                basketValue += (item.Amount * item.Price);
                newOrder.Items.Add(newOrderItem);
            }
            newOrder.OverallPrice = basketValue;

            return newOrder;
        }
    }
}
