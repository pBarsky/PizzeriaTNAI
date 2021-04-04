using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzeriaTNAI.UI.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Some story";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us if you need";

            return View();
        }
    }
}