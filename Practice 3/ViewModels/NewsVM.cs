using Practice_3.Models;

namespace Practice_3.ViewModels
{
    public class NewsVM
    {
        public News New { get; set; }
        public List<Category> Categories { get; set; }
        public List<News> News { get; set; }
        public List<Comment> Comments { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public string Description { get; set; }
        public int NewsId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
