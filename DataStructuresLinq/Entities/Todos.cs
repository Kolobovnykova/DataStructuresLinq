using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructuresLinq.Entities
{
    public class Todos
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
