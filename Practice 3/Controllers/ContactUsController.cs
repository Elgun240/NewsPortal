using Microsoft.AspNetCore.Mvc;

namespace Practice_3.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
