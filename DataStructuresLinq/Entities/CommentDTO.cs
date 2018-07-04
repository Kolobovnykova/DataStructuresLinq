using System;

namespace DataStructuresLinq.Entities
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int Likes { get; set; }

        public CommentDTO() {}

        public CommentDTO(CommentDTO comment)
        {
            Id = comment.Id;
            CreatedAt = comment.CreatedAt;
            Body = comment.Body;
            Likes = comment.Likes;
            PostId = comment.PostId;
            UserId = comment.UserId;
        }

        public override string ToString()
        {
            return $"Comment: {Id}, Created at: {CreatedAt}, Body: {Body}, Likes: {Likes}, UserId: {UserId}, PostId {PostId}";
        }
    }
}