using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using DataStructuresLinq.Entities;

namespace DataStructuresLinq
{
    public class RequestService
    {
        private readonly IEnumerable<User> users;

        public RequestService(IEnumerable<User> usersData)
        {
            users = new List<User>(usersData);
        }

        public (IEnumerable<(PostDTO post, int amount)> postsComments, int userId) GetUserCommentsNumber(int userId)
        {
            var postWithComments = users.FirstOrDefault(x => x.Id == userId)?
                .Posts.Select(x => (x, x.Comments.Count));

            return (postWithComments, userId);
        }

        public void PrintUserCommentsNumber(IEnumerable<(PostDTO post, int amount)> postsComments, int userId)
        {
            Console.Clear();
            Console.WriteLine("Get number of comments under posts of a particular user.");
            if (postsComments == null)
            {
                Console.WriteLine($"User with id {userId} was not found.");
                return;
            }

            Console.WriteLine($"User Id: {userId}");
            foreach (var post in postsComments)
            {
                Console.WriteLine($"Post #{post.post.Id} has {post.amount} comment(s)");
            }
        }

        public (IEnumerable<CommentDTO> comments, int userId) GetUserCommentsWithLength(int userId)
        {
            var commentsWithLength = users.FirstOrDefault(x => x.Id == userId)?
                .Posts.SelectMany(x => x.Comments.Where(y => y.Body.Length > 50));

            return (commentsWithLength, userId);
        }

        public void PrintUserCommentsWithLength(IEnumerable<CommentDTO> commentsWithLength, int userId)
        {
            Console.Clear();
            Console.WriteLine("Get number of comments with length more than 50 of a particular user.");
            if (commentsWithLength == null)
            {
                Console.WriteLine($"User with id {userId} was not found.");
                return;
            }

            Console.WriteLine($"User Id: {userId}");
            foreach (var comment in commentsWithLength)
            {
                Console.WriteLine(comment);
            }
        }

        public (IEnumerable<(int id, string name)> todos, int userId) GetListOfCompleteTodos(int userId)
        {
            var listOfTodos = users.FirstOrDefault(x => x.Id == userId)?
                .Todos.Where(x => x.IsComplete).Select(x => (x.Id, x.Name));

            return (listOfTodos, userId);
        }

        public void PrintListOfCompleteTodos(IEnumerable<(int id, string name)> todos, int userId)
        {
            Console.Clear();
            Console.WriteLine("Get number of complete todos of a particular user.");
            if (todos == null)
            {
                Console.WriteLine($"User with id {userId} was not found or there are no complete todos.");
                return;
            }

            Console.WriteLine($"User Id: {userId}");
            foreach (var todo in todos)
            {
                Console.WriteLine($"Todo #{todo.id} with name {todo.name} is complete");
            }
        }

        public IEnumerable<User> GetListOfSortedUsers(int numberOfUsers)
        {
            var listOfSortedUsers = users.OrderBy(x => x.Name)
                .Select(x => new User(x, x.Todos.OrderByDescending(y => y.Name.Length))).Take(numberOfUsers);

            return listOfSortedUsers;
        }

        public void PrintSortedUsersWithTodos(IEnumerable<User> users)
        {
            Console.Clear();
            Console.WriteLine("Get sorted.");
            if (users == null)
            {
                Console.WriteLine("Users were not found.");
                return;
            }

            Console.WriteLine($"User Id: {users}");
            foreach (var user in users)
            {
                Console.WriteLine(user);
                foreach (var todo in user.Todos)
                {
                    Console.WriteLine($"\t{todo}");
                }
            }
        }

        public void PrintAllUsers()
        {
            foreach (var user in users)
            {
                Console.WriteLine(user);
                foreach (var post in user.Posts)
                {
                    Console.WriteLine($"\t{post}");
                    foreach (var comment in post.Comments)
                    {
                        Console.WriteLine($"\t\t{comment}");
                    }
                }

                foreach (var todo in user.Todos)
                {
                    Console.WriteLine($"\t{todo}");
                }
            }
        }
    }
}
