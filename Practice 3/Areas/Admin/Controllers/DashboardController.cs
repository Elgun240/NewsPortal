using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice_3.DAL;
using Practice_3.ViewModels;

namespace Practice_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;
        public DashboardController(AppDbContext db)
        {

            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            DashboardVM dashboardVM = new DashboardVM()
            { 
                CategoryCount= await _db.Categories.Where(x=>x.IsDeactive==false).CountAsync(),
                SubCategoriesCount= await _db.SubCategories.Where(x=>x.IsDeactive==false).CountAsync(),
                NewsCount= await _db.News.Where(x=>x.IsDeactive==false).CountAsync(),
                TrashNewsCount= await _db.News.Where(x=>x.IsDeactive==true).CountAsync(),
            };
            var image = _db.ProfilePhotos.Select(x => x.UserId == User.Identity.Name);


            return View(dashboardVM);
        }
    }
}
