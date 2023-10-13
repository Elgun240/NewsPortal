using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice_3.DAL;
using Practice_3.Helpers;
using Practice_3.Models;
using Practice_3.ViewModels;

namespace Practice_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _db;
        public CategoriesController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (category == null)
            {
                return NotFound();
            }
            bool isExist = _db.Categories.Any(x=>x.Name==category.Name);
            if (isExist==true)
            {
                ModelState.AddModelError("Name", "This category is already exist!");
                return View();
            }            
            

            Category newcategory = new Category 
            { 
                 Name = category.Name,
                 Desctiption = category.Desctiption,
                 CreatedDate = DateTime.Now,
            
            };
             _db.Categories.Add(newcategory);
            await _db.SaveChangesAsync();
            return RedirectToAction("Manage" , "Categories");
        }
        public async Task<IActionResult> Manage()
        {
            ManageCategoriesVM mcVM = new ManageCategoriesVM
            {
                DeactivedCategories = await _db.Categories.Where(x => x.IsDeactive == true).ToListAsync(),
                Categories = await _db.Categories.Where(x => x.IsDeactive == false).ToListAsync()
            };
            return View(mcVM);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var category = _db.Categories.FirstOrDefault(x => x.Id == id);
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,Category category)
        {
            if (category == null)
            {
                return NotFound();
            }
            var dbcategory = _db.Categories.FirstOrDefault(x => x.Id == id);
            bool isExist = _db.Categories.Any(c=>c.Name==category.Name);
            if (isExist==true)
            {
                ModelState.AddModelError("Name", "This category is already exist!");
                return View();
            }
            dbcategory.Name=category.Name;
            dbcategory.CreatedDate = category.CreatedDate;
            if(category.Desctiption!=null)
            {
                dbcategory.Desctiption = category.Desctiption;
            }
            
            dbcategory.LastUpddationTime = DateTime.Now;
           await  _db.SaveChangesAsync();
            return RedirectToAction("Manage" , "Categories");
        }
        public async Task<IActionResult> Deactive(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            category.IsDeactive= true;
            await _db.SaveChangesAsync();
            return RedirectToAction("Manage" , "Categories");
        }
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            category.IsDeactive = false;
            await _db.SaveChangesAsync();
            return RedirectToAction("Manage", "Categories");
        }
        public async Task<IActionResult> Delete(int? id)
        {
           if(id == 0)
            {
                return NotFound();
            }
            var category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return RedirectToAction("Manage", "Categories");
        }
    }
}
