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
    class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            Property(x => x.ProductId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).IsRequired().HasMaxLength(128);
            Property(x => x.Description).IsRequired().HasMaxLength(255);
            Property(x => x.Price).IsRequired();
            Property(x => x.PictureUrl).IsRequired();
            HasMany(x => x.Items).WithRequired(x => x.Product).WillCascadeOnDelete(false);
        }
    }
}
