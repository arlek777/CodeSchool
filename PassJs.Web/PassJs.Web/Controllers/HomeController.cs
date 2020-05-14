using Microsoft.AspNetCore.Mvc;

namespace PassJs.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Test()
        {
            return View();
        }
    }
}
