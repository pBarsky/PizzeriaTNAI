using PizzeriaTNAI.Entities.Models;
using PizzeriaTNAI.UI.Models;

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
            if (context.Products.Any()) return;

            var hawajska = new Product
            {
                Name = "Pizza Hawajska",
                Price = 25m,
                Description = "Pizza z ananasem i szynk¹",
                PictureUrl = "https://www.lifeandmore.pl/files/Image/smakiswiata/pizza_hawajska.jpg"
            };
            var pepperoni = new Product
            {
                Name = "Pizza Pepperoni",
                Price = 26.50m,
                Description = "Pizza z pepperoni i cebul¹",
                PictureUrl = "https://thumbs.dreamstime.com/z/pepperoni-pizza-30402134.jpg"
            };

            context.Products.Add(hawajska);
            context.Products.Add(pepperoni);
            context.SaveChanges();
        }
    }
}
