using System;
using System.Collections.Generic;

namespace DataStructuresLinq.Entities
{
    public class User
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }

        public User(UserDTO user, IEnumerable<PostDTO> posts, IEnumerable<TodoDTO> todos)
        {
            Id = user.Id;
            CreatedAt = user.CreatedAt;
            Name = user.Name;
            Email = user.Email;
            Avatar = user.Avatar;
            Posts = new List<PostDTO>(posts);
            Todos = new List<TodoDTO>(todos);
        }

        public List<PostDTO> Posts { get; set; }
        public List<TodoDTO> Todos { get; set; }
        public List<CommentDTO> Comments { get; set; }


        public override string ToString()
        {
            return $"User: {Id}, Name: {Name}, Email: {Email}";
        }
    }
}
