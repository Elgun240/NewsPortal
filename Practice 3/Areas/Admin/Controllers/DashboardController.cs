using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Practice_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class DashboardController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
