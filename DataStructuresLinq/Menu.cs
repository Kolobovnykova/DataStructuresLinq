using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataStructuresLinq.Entities;
using Newtonsoft.Json;

namespace DataStructuresLinq
{
    public class Menu
    {
        private static readonly HttpClient client = new HttpClient() { BaseAddress = new Uri("https://5b128555d50a5c0014ef1204.mockapi.io/") };
        private static RequestService requestService;
        private const string menu = "What would you like to do?\n '1' - get number of comments under post for user by id" +
                                    "\n '2' - get number of comments under posts with length more than 50 of a particular user" +
                                    "\n '3' - get number of complete todos of a particular user" +
                                    "\n '4' - get a set of sorted users with todos" +
                                    "\n '5' - " +
                                    "\n '6' - " +
                                    "\n 'Esc' - press escape to exit application \n";

        public Menu()
        {
            var dataFromServer = GetDataFromServer().GetAwaiter().GetResult();
            var users = GetCollection(dataFromServer);
            requestService = new RequestService(users);
        }

        public void SelectMenu()
        {
            ConsoleKeyInfo enteredKey;
            do
            {
                Console.Clear();
                Console.WriteLine(menu);
                enteredKey = Console.ReadKey();

                switch (enteredKey.Key)
                {
                    case ConsoleKey.D1:
                        GetPostsCommentsAmount();
                        break;
                    case ConsoleKey.D2:
                        GetCommentsWithLength();
                        break;
                    case ConsoleKey.D3:
                        GetCompeteTodos();
                        break;
                    case ConsoleKey.D4:
                        GetSortedUsersWithTodos();
                        break;
                    case ConsoleKey.D5:
                        break;
                    case ConsoleKey.D6:
                        break;
                    case ConsoleKey.D7:
                        PrintAllUsers();
                        break;
                }

                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            } while (enteredKey.Key != ConsoleKey.Escape);
        }

        private void GetSortedUsersWithTodos()
        {
            Console.Clear();
            Console.WriteLine("Get a set of sorted users with todos.\nEnter number of users you want to be displayed:");
            try
            {
                var numberOfUsers = int.Parse(Console.ReadLine());
                var sortedUsers = requestService.GetListOfSortedUsers(numberOfUsers);
                requestService.PrintSortedUsersWithTodos(sortedUsers);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input.");
            }
        }

        private void GetCompeteTodos()
        {
            Console.Clear();
            Console.WriteLine("Get number of complete todos of a particular user.\nEnter the id:");
            try
            {
                var userId = int.Parse(Console.ReadLine());
                var completeTodos = requestService.GetListOfCompleteTodos(userId);
                requestService.PrintListOfCompleteTodos(completeTodos.todos, userId);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input.");
            }
        }
        
        private void GetCommentsWithLength()
        {
            Console.Clear();
            Console.WriteLine("Get number of comments under posts with length more than 50 of a particular user.\nEnter the id:");
            try
            {
                var userId = int.Parse(Console.ReadLine());
                var postsWithCommentsNumber = requestService.GetUserCommentsWithLength(userId);
                requestService.PrintUserCommentsWithLength(postsWithCommentsNumber.comments, userId);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input.");
            }
        }

        private void GetPostsCommentsAmount()
        {
            Console.Clear();
            Console.WriteLine("Get number of comments under posts of a particular user.\nEnter the id:");
            try
            {
                var userId = int.Parse(Console.ReadLine());
                var postsWithCommentsNumber = requestService.GetUserCommentsNumber(userId);
                requestService.PrintUserCommentsNumber(postsWithCommentsNumber.postsComments, userId);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input.");
            }
        }

        private void PrintAllUsers()
        {
            requestService.PrintAllUsers();
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
