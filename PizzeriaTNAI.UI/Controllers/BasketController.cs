using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzeriaTNAI.BusinessLogic.Services.SBasket;
using PizzeriaTNAI.UI.ViewModels;

namespace PizzeriaTNAI.UI.Controllers
{
    public class BasketController : Controller
    {
        private IBasketService _basketService;


        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public ActionResult Index()
        {
            var basketItems = _basketService.GetBasket();
            var totalPrice = _basketService.GetBasketValue();
            BasketViewModel basketVm = new BasketViewModel()
            {
                BasketItems = basketItems,
                TotalPrice = totalPrice
            };
            return View(basketVm);
        }
        public ActionResult AddToBasket(int id)
        {
            _basketService.AddToBasket(id);

            return RedirectToAction("Index");
        }

        public int GetNumberOfPositionsInBasket()
        {
            return _basketService.GetBasketAmount();
        }

        public RedirectToRouteResult RemoveFromBasket(int productId)
        {
            _basketService.RemoveBasket(productId);
            return RedirectToAction("Index");
        }

    }
}