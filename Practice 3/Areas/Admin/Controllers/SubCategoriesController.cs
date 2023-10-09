using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_3.Helpers;

namespace Practice_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SubCategoriesController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create() 
        {
            return View();
        }
        public IActionResult Manage()
        {
            return View();
        }


    }
}
