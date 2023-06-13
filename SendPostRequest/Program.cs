using System.Text;
using System.Text.Json;

namespace SendPostRequest
{
    internal static class SendPostRequest
    {
        static void Main(string[] args)
        {
            var postData = new PostData
            {
                Name = "John Doe",
                Age = 30,
                Address = "123 Main St"
            };

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

            var json = JsonSerializer.Serialize(postData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            var response = client.PostAsync("posts", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var responseContent = response.Content.ReadAsStringAsync().Result;
                var postResponse = JsonSerializer.Deserialize<PostResponse>(responseContent, options);
                Console.WriteLine("Id: " + postResponse.Id);
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }
    }
}