using System;
using System.Data.Entity;
using System.Web;
using BusinessLogic.Services.SBasket;
using BusinessLogic.Services.SOrder;
using BusinessLogic.Services.SProduct;
using BusinessLogic.Session;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using PizzeriaTNAI.DataAccessLayer.Repositories.Implementations;
using PizzeriaTNAI.DataAccessLayer.Repositories.Interfaces;
using PizzeriaTNAI.Entities;
using PizzeriaTNAI.Entities.Identity;
using PizzeriaTNAI.UI.Controllers;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace PizzeriaTNAI.UI
{
    /// <summary>
    /// Specifies the Unity configuration for the main _container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container

        private static Lazy<IUnityContainer> _container =
          new Lazy<IUnityContainer>(() =>
          {
              var unityContainer = new UnityContainer();
              RegisterTypes(unityContainer);
              return unityContainer;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => _container.Value;

        #endregion Unity Container

        /// <summary>
        /// Registers the type mappings with the Unity _container.
        /// </summary>
        /// <param name="container">The unity _container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // _container.LoadConfiguration();

            container.RegisterType<DbContext, AppDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(x =>
                HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<AccountController>(new InjectionConstructor());

            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IOrderRepository, OrderRepository>();

            container.RegisterType<ISessionManager, SessionManager>();
            container.RegisterType<IBasketService, BasketService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IProductService, ProductService>();
        }
    }
}