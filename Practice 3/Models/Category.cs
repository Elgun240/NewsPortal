namespace Practice_3.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubCategory>? SubCategories { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeactive { get; set; }
        public List<News>? News { get; set; }
        public string Desctiption { get; set; }
        public DateTime? LastUpddationTime { get; set; }

    }
}
