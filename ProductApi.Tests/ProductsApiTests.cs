using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;
using ProductApi.Models;
using System.Net.Http.Headers;

namespace ProductApi.Tests
{
    public class ProductsApiTests : IClassFixture<WebApplicationFactory<ProductApi.Program>>
    {
        private readonly HttpClient _client;
        public ProductsApiTests(WebApplicationFactory<ProductApi.Program> factory)
        {
            _client = factory.CreateClient();
        }
        private async Task<String> GetJWtTokenAsync()
        {
            //Arrange
            var loginRequest = new
            {
                Username = "testuser",
                Password = "password" 
            };
            var loginContent = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");

            var loginResponse = await _client.PostAsync("/api/auth/login", loginContent);
            loginResponse.EnsureSuccessStatusCode();

            var responseString = await loginResponse.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseString);
            return tokenResponse.Token;

        }
        [Fact]
        public async Task GetProducts_ReturnsOkAndProducts()
        {
            var token = await GetJWtTokenAsync();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("/api/products");

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);
        }
    }
}