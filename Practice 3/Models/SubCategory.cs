namespace Practice_3.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeactive { get; set; }
        public DateTime CreateTime { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
        public List<News>? News { get; set; }
    }
}
