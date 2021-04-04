using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Session;
using PizzeriaTNAI.DataAccessLayer.Repositories.Interfaces;
using PizzeriaTNAI.Entities.Models;

namespace BusinessLogic.Services.SBasket
{
    public class BasketService : IBasketService
    {
        private const string SessionKey = "BasketEntry";
        private readonly IProductRepository _productRepository;
        private readonly ISessionManager _sessionManager;

        public BasketService(ISessionManager sessionManager, IProductRepository productRepository)
        {
            _sessionManager = sessionManager;
            _productRepository = productRepository;
        }

        public void AddToBasket(int productId)
        {
            var basket = GetBasket();
            var basketEntries = basket.Find(k => k.Product.ProductId == productId);

            if (basketEntries != null)
            {
                basketEntries.Amount++;
                return;
            }
            var productToAdd = Task.Run(() => _productRepository.GetProductAsync(productId)).Result;
            if (productToAdd != null)
            {
                var basketEntry = new BasketEntry()
                {
                    Product = productToAdd,
                    Amount = 1,
                    Price = productToAdd.Price
                };
                basket.Add(basketEntry);
            }

            _sessionManager.Set(SessionKey, basket);
        }

        public void EmptyBasket()
        {
            _sessionManager.Set<List<BasketEntry>>(SessionKey, null);
        }

        public List<BasketEntry> GetBasket()
        {
            List<BasketEntry> basketList;

            if (_sessionManager.Get<List<BasketEntry>>(SessionKey) == null)
            {
                basketList = new List<BasketEntry>();
            }
            else
            {
                basketList = _sessionManager.Get<List<BasketEntry>>(SessionKey) as List<BasketEntry>;
            }
            return basketList;
        }

        public int GetBasketAmount()
        {
            var basket = GetBasket();
            int amount = basket.Sum(k => k.Amount);
            return amount;
        }

        public decimal GetBasketValue()
        {
            var basket = GetBasket();
            return basket.Sum(k => k.Amount * k.Product.Price);
        }

        public void RemoveBasket(int productId)
        {
            var basket = GetBasket();
            var basketEntry = basket.Find(k => k.Product.ProductId == productId);

            if (basketEntry != null)
            {
                basket.Remove(basketEntry);
            }
        }
    }
}