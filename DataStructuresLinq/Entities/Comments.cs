namespace DataStructuresLinq.Entities
{
    public class Comments
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int Likes { get; set; }
    }
}