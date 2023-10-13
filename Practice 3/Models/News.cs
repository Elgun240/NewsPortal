using System.ComponentModel.DataAnnotations.Schema;

namespace Practice_3.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsDeactive { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public int SubCategoryId { get; set; }
        public List<Comment>? Comments { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
