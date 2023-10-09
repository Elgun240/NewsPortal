using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice_3.DAL;
using Practice_3.Models;
using Practice_3.ViewModels;
using System.Linq;

namespace Practice_3.Controllers
{
    public class NewsController : Controller
    {
        private readonly AppDbContext _db;
        public NewsController(AppDbContext db)
        {
            _db = db;
        }
        ////public IActionResult Index()
        ////{
        ////    return View();
        ////}
        public IActionResult NewDetail(int NewsId)
        {
            NewsVM newsVM = new NewsVM()
            {
                News = _db.News.Where(x => x.IsDeactive == false).ToList(),
                Comments = _db.Comments.Where(x => x.IsApproved == true).ToList(),
                New = _db.News.Include(x => x.Category).Include(x => x.SubCategory).Include(x => x.Comments).FirstOrDefault(x => x.Id == NewsId),
                Categories = _db.Categories.Where(x => x.IsDeactive == false).ToList(),
                SubCategories = _db.SubCategories.Where(x => x.IsDeactive == false).ToList()
            };
            return View(newsVM);
        }
        public IActionResult Search(string searchtitle)
        {

            SearchVM searchVM = new SearchVM()
            {
                News = _db.News.Where(x => x.IsDeactive == false).Where(x=>x.Title.ToLower().Contains(searchtitle)).ToList(),
                Allnews = _db.News.Include(x => x.Category).Where(x => x.IsDeactive == false).ToList(),
                Comments = _db.Comments.Where(x => x.IsApproved == true).ToList(),
              //  New = _db.News.Include(x => x.Category).Include(x => x.SubCategory).Include(x => x.Comments).FirstOrDefault(x => x.Title == searchtitle),
                Categories = _db.Categories.Where(x => x.IsDeactive == false).ToList(),
                SubCategories = _db.SubCategories.Where(x => x.IsDeactive == false).ToList()
            };
            if (searchVM.News == null)
            {
                ModelState.AddModelError("", "Not found post with this name ");
                
                searchVM.Messsage = "Not found news!";
                return View(searchVM);

            }

            return View(searchVM);
        }
     
    }
}
