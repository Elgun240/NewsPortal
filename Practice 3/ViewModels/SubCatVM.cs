using Practice_3.Models;

namespace Practice_3.ViewModels
{
    public class SubCatVM
    {
        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public SubCategory SubCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string SelectedCategory { get; set; }
    }
}
