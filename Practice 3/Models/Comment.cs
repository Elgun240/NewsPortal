namespace Practice_3.Models
{
    public class Comment
    {
        public int Id { get; set; }
        
        public string UserId { get; set; }
        public string Username { get; set; }
        public bool IsApproved { get; set; }
        public string Text { get; set; }
        public DateTime CreateTime { get; set; }
        public News? News { get; set; }
        public int NewsId { get; set; }
    }
}
