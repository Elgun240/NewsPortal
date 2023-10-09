using Practice_3.Models;

namespace Practice_3.ViewModels
{
    public class CategoryVM
    {
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }
        public List<News> News { get; set; }
        public List<Comment> Comments { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public string Name { get; set; }
    }
}
