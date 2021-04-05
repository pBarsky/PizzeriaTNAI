using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Services.SBasket;
using BusinessLogic.Services.SOrder;
using BusinessLogic.Session;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PizzeriaTNAI.DataAccessLayer.Repositories.Implementations;
using PizzeriaTNAI.DataAccessLayer.Repositories.Interfaces;
using PizzeriaTNAI.Entities;
using PizzeriaTNAI.Entities.Models;

namespace PizzeriaTNAI.UI.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;
        private readonly ISessionManager _sessionMenager;
        private ApplicationUserManager _userManager;

        public OrdersController(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _sessionMenager = new SessionManager();
            _basketService = new BasketService(_sessionMenager, productRepository);
            _orderService = new OrderService(orderRepository, productRepository);
        }

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

        public ActionResult Index()
        {
            var list = Task.Run(() => _orderService.GetOrdersAsync()).Result;
            ViewBag.Orders = "active";
            return View(list);
        }

        public ActionResult OrderConfirm()
        {
            return View();
        }

        public async Task<ActionResult> Pay()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { returnurl = Url.Action("Pay", "Orders") });
            }

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            var order = new Order
            {
                Address = user.AddressData.Address,
                City = user.AddressData.City,
                ZipCode = user.AddressData.ZipCode
            };
            return View(order);
        }

        [HttpPost]
        public async Task<ActionResult> Pay(Order order)
        {
            if (!Request.IsAuthenticated)
            {
                return View(order);
            }

            var userId = User.Identity.GetUserId();
            var orderStatus = await _orderService.SaveOrderAsync(order, userId);
            if (!orderStatus)
            {
                return View("Error");
            }
            var user = await UserManager.FindByIdAsync(userId);
            TryUpdateModel(user.AddressData);
            await UserManager.UpdateAsync(user);
            _basketService.EmptyBasket();
            return RedirectToAction("OrderConfirm");
        }
    }
}