using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using Practice_3.DAL;
using Practice_3.Helpers;
using Practice_3.Models;
using Practice_3.ViewModels;

namespace Practice_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public NewsController(AppDbContext db , IWebHostEnvironment env)
        {
            _env= env;
             _db = db;
        }

        public async Task<IActionResult> AddPost()
        {
            NewsAddVM naVM = new NewsAddVM()
            {
                Categories = await _db.Categories.Where(x => x.IsDeactive == false).ToListAsync(),
                SubCategories = await _db.SubCategories.Where(x => x.IsDeactive == false).ToListAsync(),
            };
            return View(naVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPost(NewsAddVM nnaVM)
        {
            if (nnaVM == null)
            {
                return NotFound();
            }
            NewsAddVM naVM = new NewsAddVM()
            {
                Categories = await _db.Categories.Where(x => x.IsDeactive == false).ToListAsync(),
                SubCategories = await _db.SubCategories.Where(x => x.IsDeactive == false).ToListAsync(),
            };
            if (nnaVM.Photo == null)
            {
                
                return View(naVM);
            }
            if (!nnaVM.Photo.IsImage())
            {
               
                return View(naVM);
            }
            if (nnaVM.Photo.IsMore4mb())
            {
               
                return View(naVM);
            }
            string path = Path.Combine(_env.WebRootPath, @"admin\assets\postimages");
            nnaVM.Image = await nnaVM.Photo.SaveImageAsync(path);
            News news = new News()
            {
                Title = nnaVM.Title,
                Description=nnaVM.Description,
                CreateTime=DateTime.Now,
                Image = nnaVM.Image,
                CategoryId = nnaVM.CategoryId,
                SubCategoryId = nnaVM.SubCategoryId,
            };
            await _db.News.AddAsync(news);
            await _db.SaveChangesAsync();
            return  RedirectToAction("Index" , "Dashboard");
        }
        public async Task<IActionResult> Manage()
        {
            //News news = new News();
            var news = await _db.News.Include(x=>x.Category).Include(x=>x.SubCategory).Where(x=>x.IsDeactive==false).ToListAsync();
            return View(news);
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            News news = await _db.News.FirstOrDefaultAsync(x => x.Id == id);
            NewsEditVM neVM = new NewsEditVM()
            {

                Categories = await _db.Categories.Where(x => x.IsDeactive == false).ToListAsync(),
                SubCategories = await _db.SubCategories.Where(x => x.IsDeactive == false).ToListAsync(),
                EditedNew = news,
                Title= news.Title,
                Description = news.Description
                
                
            };
            return View(neVM);
           
        }
        public async Task<IActionResult> UpdatePhoto(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var ENew = await _db.News.FirstOrDefaultAsync(x => x.Id == id);
            NewsEditVM neVM = new NewsEditVM()
            {

                Categories = await _db.Categories.Where(x => x.IsDeactive == false).ToListAsync(),
                SubCategories = await _db.SubCategories.Where(x => x.IsDeactive == false).ToListAsync(),
                EditedNew = await _db.News.FirstOrDefaultAsync(x => x.Id == id),
                Image = ENew.Image,
                Title = ENew.Title,
            };
            return View(neVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePhoto(NewsEditVM neVM , int id)
        {
            if (neVM == null)
            {
                return NotFound();
            }

            if (neVM.Photo == null)
            {

                return View(neVM);
            }
            if (!neVM.Photo.IsImage())
            {

                return View(neVM);
            }
            if (neVM.Photo.IsMore4mb())
            {

                return View(neVM);
            }
            string path = Path.Combine(_env.WebRootPath, @"admin\assets\postimages");
            neVM.Image = await neVM.Photo.SaveImageAsync(path);

            var news = _db.News.Where(x => x.IsDeactive == false).FirstOrDefault(x => x.Id == id);
            news.Image = neVM.Image;
            await _db.SaveChangesAsync();   
            return RedirectToAction("Edit" , "News" , new { id});
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewsEditVM nneVM,int id)
        {
            if (nneVM == null)
            {
                return NotFound();
            }
            bool IsExist = _db.News.Any(x => x.Title == nneVM.Title); 
            NewsEditVM neVM = new NewsEditVM()
            {

                Categories = await _db.Categories.Where(x => x.IsDeactive == false).ToListAsync(),
                SubCategories = await _db.SubCategories.Where(x => x.IsDeactive == false).ToListAsync(),
                EditedNew = await _db.News.FirstOrDefaultAsync(x => x.Id == id),
                
            };
            if (IsExist)
            {
                ModelState.AddModelError("Error", "This name is already exist!");
                return View(neVM);
            }
            neVM.EditedNew.Title = nneVM.Title;
            neVM.EditedNew.Description = nneVM.Description;
            neVM.EditedNew.CategoryId = nneVM.CategoryId;
            neVM.EditedNew.SubCategoryId = nneVM.SubCategoryId;
            //neVM.EditedNew.Image = nneVM.Image;
            await _db.SaveChangesAsync();
            ModelState.AddModelError("Success", "Finished update");
            return View(neVM);

        }
        public async Task<ActionResult> Deactive(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            News deactivenews= await _db.News.Where(x=>x.IsDeactive==false).FirstOrDefaultAsync(x=>x.Id==id);
            deactivenews.IsDeactive = true;
            await _db.SaveChangesAsync();
            return RedirectToAction("Manage","News");
        }
        public async Task<IActionResult> Trash()
        {
            var news = await _db.News.Include(x=>x.Category).Include(x=>x.SubCategory).Where(x => x.IsDeactive == true).ToListAsync();
            return View(news);
        }
        public async Task<IActionResult> Return(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var newforreturn =await _db.News.FirstOrDefaultAsync(x=>x.Id==id);
            newforreturn.IsDeactive =false;
            await _db.SaveChangesAsync();
            return RedirectToAction("Trash", "News");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var deletenews = await _db.News.FirstOrDefaultAsync(x=>x.Id==id);
            var comments = await _db.Comments.Where(x=>x.NewsId==id).ToListAsync();
            foreach (var item in comments)
            {
                _db.Comments.Remove(item);
            }
             _db.News.Remove(deletenews);
            await _db.SaveChangesAsync();
            return RedirectToAction("Trash", "News");
        }
    }
}
