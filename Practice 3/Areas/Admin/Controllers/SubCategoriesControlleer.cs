using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice_3.DAL;
using Practice_3.Models;
using Practice_3.ViewModels;

namespace Practice_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class SubCategoriesController : Controller
    {
        private readonly AppDbContext _db;
        public SubCategoriesController(AppDbContext db)
        {
            _db= db;
        }
        public IActionResult AddSubCategory()
        {
            SubCatVM sbVM = new SubCatVM()
            {
                Categories = _db.Categories.Where(x => x.IsDeactive == false).ToList(),
            };
            return View(sbVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSubCategory(SubCatVM sbVM)
        {
            if (sbVM == null)
            {
                return NotFound();
            }
            bool IsExist = _db.SubCategories.Any(x=>x.Name == sbVM.Name);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "This sub category is already exist");
                SubCatVM sbVM1 = new SubCatVM()
                {
                    Categories = _db.Categories.Where(x => x.IsDeactive == false).ToList(),
                };
                return View(sbVM1);
            }
            var newsbCat = new SubCategory
            {
                Name = sbVM.Name,
                Description = sbVM.Description,
                CategoryId = sbVM.CategoryId,
                CreateTime = DateTime.Now,
            };
           await  _db.SubCategories.AddAsync(newsbCat);
            await _db.SaveChangesAsync();
            return RedirectToAction("ManageSubCategory", "SubCategories");
        }
        public  IActionResult ManageSubCategory()
        {
            var subcategories = _db.SubCategories.Where(x=>x.IsDeactive==false).ToList();
            ManageSubCat msVM = new ManageSubCat()
            {
                SubCategories = _db.SubCategories.Include(x=>x.Category).Where(x => x.IsDeactive == false).ToList(),
                DeactivedSubCategories= _db.SubCategories.Include(x=>x.Category).Where(x=>x.IsDeactive==true).ToList(),
            };
            return View(msVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            SubCategory sub = await _db.SubCategories.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            SubCatVM scVM = new SubCatVM
            {
                SubCategory=sub,
                Categories = await _db.Categories.Where(x=>x.IsDeactive==false).ToListAsync(),
               Name=sub.Name,
               Description=sub.Description,
            };


            return View(scVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SubCatVM subCatVM,int id)
        {
            if (subCatVM == null)
            {
                return NotFound();
            }
            bool isExist = await _db.SubCategories.AnyAsync(x=>x.Id==id);
            if (!isExist)
            {
                SubCatVM scVM = new SubCatVM
                {
                    SubCategory = await _db.SubCategories.FirstOrDefaultAsync(x => x.Id == id),
                    Categories = await _db.Categories.Where(x => x.IsDeactive == false).ToListAsync(),
                };
                ModelState.AddModelError("Name", "This SubCategory is already exist!");
                return View(scVM);
            }
            SubCategory editedsubcat = await _db.SubCategories.FirstOrDefaultAsync(x => x.Id == id);
            editedsubcat.Name = subCatVM.Name;
            if(subCatVM.Description != null)
            {
                editedsubcat.Description = subCatVM.Description;
            }
            
            editedsubcat.LastUpdateDate = DateTime.Now;
            editedsubcat.CategoryId = subCatVM.CategoryId;
            await _db.SaveChangesAsync();
            return RedirectToAction("ManageSubCategory", "SubCategories");
        }
        public async Task<IActionResult> Deactive(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var subcat = await _db.SubCategories.FirstOrDefaultAsync(x => x.Id == id);
            subcat.IsDeactive = true;
            await _db.SaveChangesAsync();
            return RedirectToAction("ManageSubCategory", "SubCategories");
        }
        public async Task<IActionResult> Restore(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var subcat = await _db.SubCategories.FirstOrDefaultAsync(x => x.Id == id);
            subcat.IsDeactive = false;
            await _db.SaveChangesAsync();
            return RedirectToAction("ManageSubCategory", "SubCategories");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var subcat = await _db.SubCategories.FirstOrDefaultAsync(x => x.Id == id);
            var news = _db.News.Where(x => x.SubCategoryId==id).ToList();
            foreach (var item in news)
            {
                _db.News.Remove(item);
            }
            _db.SubCategories.Remove(subcat);
            await _db.SaveChangesAsync();
            return RedirectToAction("ManageSubCategory", "Subcategories");
        }
    }
}
