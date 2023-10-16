using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice_3.DAL;
using Practice_3.Models;
using Practice_3.ViewModels;

namespace Practice_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class DashboardController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _db;
        public DashboardController(AppDbContext db, RoleManager<IdentityRole> rolemanager,
            UserManager<AppUser> usermanager,
            SignInManager<AppUser> signInManager)
        {
            _roleManager = rolemanager;
            _userManager = usermanager;
            _signInManager = signInManager;
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

            var user = await _userManager.GetUserAsync(User);
            List<Comment> comments = await _db.Comments.Where(x => x.UserId == user.Id).ToListAsync();
            var image1 = _db.ProfilePhotos.Select(i => i).Where(x => x.UserId == user.Id).FirstOrDefault();
            ViewData["profilePhoto"] = image1.ImagePath;
            return View(dashboardVM);
        }
    }
}
