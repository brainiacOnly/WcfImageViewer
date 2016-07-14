using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WcfImageVeiwer.Client.Proxies;

namespace WcfImageVeiwer.Client.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var proxy = new PictureManagerClient();

            return View();
        }
    }
}