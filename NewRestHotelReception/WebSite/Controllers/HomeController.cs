using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using WebSite.Models;
using WebSite.Models.Login;
using WebSite.OtherContent.Token;

namespace WebSite.Controllers
{
	
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

        //private readonly Uri url = new Uri("https://localhost:7278/api/Token/");
        [CustomTokenAuthorization]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}