namespace Practice_3.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public DateTime CreateTime { get; set; }
        public News? News { get; set; }
        public int NewsId { get; set; }
    }
}
