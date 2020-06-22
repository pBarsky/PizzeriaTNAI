using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Services.SOrder;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PizzeriaTNAI.BusinessLogic.Services.SBasket;
using PizzeriaTNAI.BusinessLogic.Session;
using PizzeriaTNAI.DataAccessLayer.Repositories.Implementations;
using PizzeriaTNAI.DataAccessLayer.Repositories.Interfaces;
using PizzeriaTNAI.Entities;
using PizzeriaTNAI.Entities.Models;

namespace PizzeriaTNAI.UI.Controllers
{
    public class OrdersController : Controller
    {
        private IBasketService _basketService;
        private SessionManager _sessionMenager;
        private IOrderService _orderService;


        public OrdersController(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _sessionMenager = new SessionManager();
            _basketService = new BasketService(_sessionMenager, productRepository);
            _orderService = new OrderService(orderRepository, productRepository);
        }


        public ActionResult Index()
        {
            var list = Task.Run(() => _orderService.GetOrdersAsync()).Result;
            return View(list);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = Task.Run(() => _orderService.GetOrderAsync((int)id)).Result;
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        public async Task<ActionResult> Pay()
        {
            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                var order = new Order
                {
                    Address = user.AddressData.Address,
                    City = user.AddressData.City,
                    ZipCode = user.AddressData.ZipCode
                };
                return View(order);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnurl = Url.Action("Pay", "Orders") });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Pay(Order orderDetails)
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var orderStatus = _orderService.SaveOrder(orderDetails, userId);
                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.AddressData);
                await UserManager.UpdateAsync(user);
                _basketService.EmptyBasket();
                return RedirectToAction("OrderConfirm");
            }
            else
            {
                return View(orderDetails);
            }
        }

        public ActionResult OrderConfirm()
        {
            return View();
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}
