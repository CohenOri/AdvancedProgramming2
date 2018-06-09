using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ConfigController : Controller
    {
        public static Settings settings = new Settings();
        // GET: Config
        public ActionResult Config()
        {
            ViewBag.message = "you are here!!!";
            return View(settings);
        }
    }
}