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
        public ActionResult About()
        {
            ViewBag.Message = "Some story";
            ViewBag.About = "active";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us if you need";
            ViewBag.Contact = "active";

            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Home = "active";
            return View();
        }
    }
}