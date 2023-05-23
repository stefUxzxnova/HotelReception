 using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
	public class UsersController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
