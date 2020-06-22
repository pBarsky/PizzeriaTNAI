using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzeriaTNAI.Entities.Models;

namespace PizzeriaTNAI.UI.ViewModels
{
    public class BasketViewModel
    {
        public List<BasketEntry> BasketItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}