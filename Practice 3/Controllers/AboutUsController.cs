using Microsoft.AspNetCore.Mvc;

namespace Practice_3.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
