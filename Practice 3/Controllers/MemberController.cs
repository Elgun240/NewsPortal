using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice_3.DAL;
using Practice_3.Models;

namespace Practice_3.Controllers
{
    public class MemberController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _db;
        public MemberController(AppDbContext db, RoleManager<IdentityRole> rolemanager,
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
            var user = await _userManager.GetUserAsync(User);
            List<Comment> comments = await _db.Comments.Where(x=>x.UserId== user.Id).ToListAsync();
            var image = _db.ProfilePhotos.Select(i => i).Where(x => x.UserId == user.Id).FirstOrDefault();
            ViewData["profilePhoto"] = image.ImagePath;
            return View(comments);
        }
    }
}
