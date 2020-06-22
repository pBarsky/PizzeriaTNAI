using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaTNAI.Entities.Models;

namespace PizzeriaTNAI.Entities.Configurations
{
    class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            Property(x => x.OrderId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserId).IsRequired();
            Property(x => x.Address).HasMaxLength(255).IsRequired();
            Property(x => x.City).HasMaxLength(255).IsRequired();
            Property(x => x.OverallPrice).IsRequired();
            Property(x => x.ZipCode).HasMaxLength(255).IsRequired();
            Property(x => x.DateOfCreation).IsRequired();
            HasMany(x => x.Items).WithRequired(x => x.Order);
        }
    }
}
