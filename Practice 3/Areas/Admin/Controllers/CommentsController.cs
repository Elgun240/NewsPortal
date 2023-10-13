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
    public class CommentsController : Controller
    {
        private readonly AppDbContext _db;
        public CommentsController(AppDbContext db)
        {
                _db = db;
        }
        public async Task<IActionResult> Waiting()
        {
            var comments = await _db.Comments.Include(x=>x.News).Where(x=>x.IsApproved==false).ToListAsync();
            return View(comments);
        }
        public async Task<ActionResult> Approve(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            Comment comment = await _db.Comments.FirstOrDefaultAsync(x => x.Id == id);
            comment.IsApproved = true;
            await _db.SaveChangesAsync();
            return RedirectToAction("Waiting", "Comments");
        }
        public async Task<IActionResult> ApprovedComments()
        {
            var comments = await _db.Comments.Include(x => x.News).Where(x => x.IsApproved == true).ToListAsync();
            return View(comments);
        }
        public async Task<IActionResult> Prohibid(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            Comment comment = await _db.Comments.FirstOrDefaultAsync(x => x.Id == id);
            comment.IsApproved = false;
            await _db.SaveChangesAsync();
            return RedirectToAction("Waiting", "Comments");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            Comment comment = await _db.Comments.FirstOrDefaultAsync(x => x.Id == id);
            _db.Comments.Remove(comment);
            await _db.SaveChangesAsync();
            return RedirectToAction("Waiting", "Comments");
        }
    }
}
