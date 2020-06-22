using System.Collections.Generic;
using PizzeriaTNAI.Entities.Models;

namespace PizzeriaTNAI.BusinessLogic.Services.SBasket
{
    public interface IBasketService
    {
        List<BasketEntry> GetBasket();
        void AddToBasket(int productId);
        void RemoveBasket(int productId);
        decimal GetBasketValue();
        int GetBasketAmount();
        void EmptyBasket();
    }
}