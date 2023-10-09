using Practice_3.Models;

namespace Practice_3.ViewModels
{
    public class SearchVM
    {
        public List<Category> Categories { get; set; }
        public Category Category { get; set; }
        public List<News> News { get; set; }
        public List<News> Allnews { get; set; }
        public News New { get; set; }
        public List<Comment> Comments { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public string Messsage { get; set; } 
    }
}
