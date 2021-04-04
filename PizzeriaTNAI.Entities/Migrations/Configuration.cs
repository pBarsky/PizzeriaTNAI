using System.Web.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PizzeriaTNAI.Entities.Models;

namespace PizzeriaTNAI.Entities.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PizzeriaTNAI.Entities.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PizzeriaTNAI.Entities.AppDbContext context)
        {
            if (!context.Products.Any())
            {
                var hawaiian = new Product
                {
                    Name = "Hawaiian Pizza",
                    Price = 25m,
                    Description = "Pizza with ham and pineapple",
                    PictureUrl = "https://www.lifeandmore.pl/files/Image/smakiswiata/pizza_hawajska.jpg"
                };
                var pepperoni = new Product
                {
                    Name = "Pepperoni Pizza",
                    Price = 26.50m,
                    Description = "Pizza with salami, pepperoni peppers and onions",
                    PictureUrl = "https://thumbs.dreamstime.com/z/pepperoni-pizza-30402134.jpg"
                };
                var capriciosa = new Product
                {
                    Name = "Caprriciosa Pizza",
                    Price = 23.50m,
                    Description = "Pizza with mozarella, ham and mushrooms",
                    PictureUrl = "https://images.telepizza.com/vol/pl/images/content/productos/p2ca_c.png"
                };
                var vega = new Product
                {
                    Name = "Vegan Pizza",
                    Price = 16.50m,
                    Description = "Pizza with ricotta cheese, mozarella, spinach and salad tomatos.",
                    PictureUrl = "https://vega.sklep.pl/media/catalog/product/cache/11/image/40ff0ebde3677abe38d507cf99819010/2/2/22._cadru.jpg"
                };
                context.Products.Add(hawaiian);
                context.Products.Add(pepperoni);
                context.Products.Add(capriciosa);
                context.Products.Add(vega);
                context.SaveChanges();
            }
            // TODO: nie wiem jak zseedowaæ usera, przy odpalaniu Seed'a wyrzuca ze The entity type ApplicationUser is not part of the model for the current context
            // if (!context.Roles.Any(r => r.Name == "AppAdmin"))
            // {
            //     var store = new RoleStore<IdentityRole>(context);
            //     var manager = new RoleManager<IdentityRole>(store);
            //     var role = new IdentityRole { Name = "AppAdmin" };
            //
            //     manager.Create(role);
            // }
            //
            // if (!context.Users.Any(u => u.UserName == "admin"))
            // {
            //     var store = new UserStore<ApplicationUser>();
            //     var manager = new UserManager<ApplicationUser>(store);
            //     var user = new ApplicationUser { UserName = "admin" };
            //     if (manager.Create(user, WebConfigurationManager.AppSettings["DefaultAdminPassword"]).Succeeded)
            //     {
            //         manager.AddToRole(user.Id, "AppAdmin");
            //     }
            // }
        }
    }
}