using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace GlimpseDemo.Controllers
{
	public class HomeController : Controller
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));

		public ActionResult Index()
		{
			ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

			log.Debug("Log4Net: Debug");
			log.Info("Log4Net: Info");
			log.Warn("Log4Net: Warn");
			log.Error("Log4Net: Error");
			log.Fatal("Log4Net: Fatal");

			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your app description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}
