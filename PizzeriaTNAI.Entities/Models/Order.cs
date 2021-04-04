using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaTNAI.Entities.Identity;

namespace PizzeriaTNAI.Entities.Models
{
    public class Order
    {
        public string Address { get; set; }

        public string City { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DateOfCreation { get; set; }

        [ScaffoldColumn(false)]
        public virtual ICollection<OrderItem> Items { get; set; }

        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [ScaffoldColumn(false)]
        public decimal OverallPrice { get; set; }

        [ScaffoldColumn(false)]
        public virtual ApplicationUser User { get; set; }

        [ScaffoldColumn(false)]
        public string UserId { get; set; }

        public string ZipCode { get; set; }
    }
}