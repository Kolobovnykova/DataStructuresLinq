using System;

namespace DataStructuresLinq.Entities
{
    public class TodoDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

        public TodoDTO() {}

        public TodoDTO(TodoDTO todo)
        {
            Id = todo.Id;
            CreatedAt = todo.CreatedAt;
            Name = todo.Name;
            UserId = todo.UserId;
        }

        public override string ToString()
        {
            return $"Todo: {Id}, Created at: {CreatedAt}, Name: {Name}, UserId: {UserId}";
        }
    }
}