using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice_3.DAL;
using Practice_3.ViewModels;
using System.Diagnostics;

namespace Practice_3.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Categories =_db.Categories.Where(x=>x.IsDeactive==false).ToList(),
                Comments = _db.Comments.Where(x=>x.IsApproved==true).ToList(),
                News =  _db.News.Include(x=>x.Category).Where(x=>x.IsDeactive == false).ToList(),
                SubCategories= _db.SubCategories.Where(x=>x.IsDeactive==false).ToList()


            };
            return View(homeVM);
        }
    }
}