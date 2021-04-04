using PizzeriaTNAI.UI.ViewModels;
using System.Web.Mvc;
using BusinessLogic.Services.SBasket;

namespace PizzeriaTNAI.UI.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
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

        public ActionResult Index()
        {
            var basketItems = _basketService.GetBasket();
            var totalPrice = _basketService.GetBasketValue();
            var basketVm = new BasketViewModel()
            {
                BasketItems = basketItems,
                TotalPrice = totalPrice
            };
            return View(basketVm);
        }

        public RedirectToRouteResult RemoveFromBasket(int productId)
        {
            _basketService.RemoveBasket(productId);
            return RedirectToAction("Index");
        }
    }
}