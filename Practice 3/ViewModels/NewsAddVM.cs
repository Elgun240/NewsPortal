using Practice_3.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice_3.ViewModels
{
    public class NewsAddVM
    {
        public List<SubCategory> SubCategories { get; set; }
        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; }
        public int SubCategoryId { get; set; }
        public string Title { get; set; } = null;
        public string Description { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
