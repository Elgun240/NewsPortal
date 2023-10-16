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
            if(image == null)
            {
                ViewData["profilePhoto"] = "b119040e-e40f-4f3b-a524-ee3d8f415a37261eae38-384e-4101-961a-a949f727e4ccbank.png";
            }
            else
            {
                ViewData["profilePhoto"] = image.ImagePath;
            }
            
            return View(comments);
        }
    }
}
