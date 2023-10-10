using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Practice_3.DAL;
using Practice_3.Models;
using Practice_3.ViewModels;

namespace Practice_3.Controllers
{
    public class CommentController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
      

        public CommentController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsVM newsVM)
        {
            if(User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if(user == null)
                {
                    NotFound();
                }
                await _db.Comments.AddAsync(new Comment()
                {
                    CreateTime = DateTime.UtcNow,
                    IsApproved = false,
                    Username=user.Name,
                    UserId = user.Id,
                    Text = newsVM.Description,
                    NewsId = newsVM.New.Id
                }) ;
                await _db.SaveChangesAsync();
               
            }
            return RedirectToAction("NewDetail", "News", new { NewsId = newsVM.New.Id });


        }
    }
}
