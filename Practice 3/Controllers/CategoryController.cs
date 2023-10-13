using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice_3.DAL;
using Practice_3.ViewModels;

namespace Practice_3.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int CategoryId)
        {
           
            CategoryVM catVM = new CategoryVM()
            {
                News =_db.News.Where(x=>x.CategoryId==CategoryId).ToList(),
                Comments = _db.Comments.Where(x=>x.IsApproved==true).ToList(),
                Category =  _db.Categories.FirstOrDefault(x=>x.Id==CategoryId),
                Categories = _db.Categories.Where(x => x.IsDeactive == false).ToList(),
                SubCategories = _db.SubCategories.Where(x=>x.IsDeactive==false).ToList()

            };
            return View(catVM);
        }

       
    }
}
