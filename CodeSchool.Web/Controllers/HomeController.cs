using Microsoft.AspNetCore.Mvc;

namespace CodeSchool.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
