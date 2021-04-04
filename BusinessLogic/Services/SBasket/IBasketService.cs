using System.Collections.Generic;
using PizzeriaTNAI.Entities.Models;

namespace BusinessLogic.Services.SBasket
{
    public interface IBasketService
    {
        void AddToBasket(int productId);

        void EmptyBasket();

        List<BasketEntry> GetBasket();

        int GetBasketAmount();

        decimal GetBasketValue();

        void RemoveBasket(int productId);
    }
}