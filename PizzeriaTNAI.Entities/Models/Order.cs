using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaTNAI.UI.Models;

namespace PizzeriaTNAI.Entities.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }

        public DateTime DateOfCreation { get; set; }
        public decimal OverallPrice { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}
