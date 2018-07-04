using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DataStructuresLinq.Entities;
using Newtonsoft.Json;

namespace DataStructuresLinq
{
    public class Program
    {
        private static readonly HttpClient client = new HttpClient() { BaseAddress = new Uri("https://5b128555d50a5c0014ef1204.mockapi.io/") };

        private static void Main(string[] args)
        {
            var dataFromServer = GetDataFromServer().GetAwaiter().GetResult();
            var users = GetCollection(dataFromServer);

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

            Console.ReadKey();
        }

        private static async Task<(List<UserDTO> usersData, List<PostDTO> postsData, List<CommentDTO> commentsData, List<TodoDTO> todosData)> GetDataFromServer()
        {
            string responseBody = await client.GetStringAsync("users");
            var usersData = JsonConvert.DeserializeObject<List<UserDTO>>(responseBody);

            responseBody = await client.GetStringAsync("posts");
            var postsData = JsonConvert.DeserializeObject<List<PostDTO>>(responseBody);

            responseBody = await client.GetStringAsync("comments");
            var commentsData = JsonConvert.DeserializeObject<List<CommentDTO>>(responseBody);

            responseBody = await client.GetStringAsync("todos");
            var todosData = JsonConvert.DeserializeObject<List<TodoDTO>>(responseBody);
            return (usersData, postsData, commentsData, todosData);
        }

        private static IEnumerable<User> GetCollection((List<UserDTO> usersData, List<PostDTO> postsData, List<CommentDTO> commentsData, List<TodoDTO> todosData) dataFromServer)
        {
            var users = from user in dataFromServer.usersData
                        join post in (from p in dataFromServer.postsData
                                      join comment in dataFromServer.commentsData on p.Id equals comment.PostId into postComment
                                      select new PostDTO(p, postComment)) on user.Id equals post.UserId into postComments
                        join todo in dataFromServer.todosData on user.Id equals todo.UserId into todos
                        select new User(user, postComments, todos);

            return users;
        }

    }
}