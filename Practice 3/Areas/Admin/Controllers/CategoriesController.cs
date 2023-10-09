using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_3.DAL;
using Practice_3.Helpers;

namespace Practice_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _db;
        public CategoriesController(AppDbContext db)
        {
            _db = db;
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
