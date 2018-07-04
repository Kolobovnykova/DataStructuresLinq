using System;
using System.Collections.Generic;

namespace DataStructuresLinq.Entities
{
    public class PostDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int Likes { get; set; }

        public List<CommentDTO> Comments { get; set; }

        public PostDTO() {}

        public PostDTO(PostDTO post)
        {
            Id = post.Id;
            CreatedAt = post.CreatedAt;
            Body = post.Body;
            Likes = post.Likes;
            Title = post.Title;
            UserId = post.UserId;
        }

        public PostDTO(PostDTO post, IEnumerable<CommentDTO> comments) : this(post)
        {
            Comments = new List<CommentDTO>(comments);
        }


        public override string ToString()
        {
            return $"Post: {Id}, Created at: {CreatedAt}, Title: {Title}, Body: {Body}, Likes: {Likes}";
        }
    }
}