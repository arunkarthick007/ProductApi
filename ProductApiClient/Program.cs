using ProductApi.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace ProductApiClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static string token;
        static async Task Main(string[] args)
        {
            client.BaseAddress = new Uri("https://localhost:7092");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            await LoginUserAsync();

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                ShowMenu();
            }
            else
            {
                Console.WriteLine("Login failed. Exiting the application.");
            }
        }

        private static async Task LoginUserAsync()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            token = await LoginAsync(username, password);
            Console.WriteLine("Login Successful and token obtained");
        }

        private static async Task<string> LoginAsync(string username, string password)
        {
            var loginRequest = new { Username = username, Password = password };
            var content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseString);
                return tokenResponse.Token;
            }
            else
            {
                Console.WriteLine("Login failed. Please check your credentials.");
                return null;
            }
        }

        private static async void ShowMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. See available products");
                Console.WriteLine("2. Add a product");
                Console.WriteLine("3. Delete a product");
                Console.WriteLine("4. Run Test");
                Console.Write("Enter your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GetProductsAsync().Wait();
                        break;
                    case "2":
                        AddProductAsync().Wait();
                        break;
                    case "3":
                        DeleteProductAsync().Wait();
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private static async Task GetProductsAsync()
        {
            var response = await client.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Products: " + responseString);
        }

        private static async Task AddProductAsync()
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            Console.Write("Enter product price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            var product = new Product { Name = name, Price = price };
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/products", content);
            response.EnsureSuccessStatusCode();
            Console.WriteLine("Product added successfully!");
        }

        private static async Task DeleteProductAsync()
        {
            Console.Write("Enter product ID to delete: ");
            string productId = Console.ReadLine();

            var response = await client.DeleteAsync($"/api/products/{productId}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Product deleted successfully!");
            }
            else
            {
                Console.WriteLine("Failed to delete product.");
            }
        }
    }
}