using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_3.Helpers;

namespace Practice_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
