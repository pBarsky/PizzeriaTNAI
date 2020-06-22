using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Services.SOrder;
using PizzeriaTNAI.BusinessLogic.Services.SBasket;
using PizzeriaTNAI.BusinessLogic.Session;
using PizzeriaTNAI.DataAccessLayer.Repositories.Implementations;
using PizzeriaTNAI.DataAccessLayer.Repositories.Interfaces;
using PizzeriaTNAI.Entities;
using PizzeriaTNAI.Entities.Models;

namespace PizzeriaTNAI.UI.Controllers
{
    public class OrderController : Controller
    {
        private IBasketService _basketService;
        private SessionManager _sessionMenager;
        private IOrderService _orderService;


        public OrderController(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _sessionMenager = new SessionManager();
            _basketService = new BasketService(_sessionMenager, productRepository);
            _orderService = new OrderService(orderRepository, productRepository);
        }


        public ActionResult Index()
        {
            var list = db.Orders.OrderByDescending(x => x.OrderId).ToList();
            return View(list);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = db.Orders.Find(id);
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
                    Address = user.UserData.Address,
                    City = user.UserData.City,
                    ZIPCode = user.UserData.ZipCode
                };
                return View(order);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnurl = Url.Action("Pay", "Order") });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Pay(Order orderDetails)
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var newOrder = orderManager.CreateOrder(orderDetails, userId);
                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.UserData);
                await UserManager.UpdateAsync(user);
                _basketMenager.EmptyBasket();
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
