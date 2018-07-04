using System.Net.Http;
using System.Threading.Tasks;

namespace DataStructuresLinq
{
    public class Program
    {
        static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {

            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            HttpResponseMessage response = await client.GetAsync("https://5b128555d50a5c0014ef1204.mockapi.io/users");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

        }
    }
}