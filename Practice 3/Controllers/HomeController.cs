using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice_3.DAL;
using Practice_3.Models;
using Practice_3.ViewModels;
using System.Diagnostics;
using System.Linq;

namespace Practice_3.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            
            _db = db;
        }
        public async Task<IActionResult> Index(int page =1)
        {


           
            int showPostCount = 3;
            ViewBag.PostCount = Math.Ceiling((decimal)_db.News.Count() / 3);
            HomeVM homeVM = new HomeVM
            {
                AllNews = _db.News.Where(x => x.IsDeactive == false).OrderByDescending(x => x.Id).Take(5).ToList(),
                Categories = _db.Categories.Where(x => x.IsDeactive == false).ToList(),
                Comments = _db.Comments.Where(x => x.IsApproved == true).ToList(),
                News = _db.News.Include(x => x.Category).Where(x => x.IsDeactive == false).Skip((page - 1) * showPostCount).Take(showPostCount).ToList(),
                SubCategories = _db.SubCategories.Where(x => x.IsDeactive == false).ToList(),
                currentPage = page,
               

        };
            return View(homeVM);
        }
    }
}