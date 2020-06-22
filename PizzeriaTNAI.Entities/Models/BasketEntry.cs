using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaTNAI.Entities.Models
{
    public class BasketEntry
    {
        public Product Product { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
