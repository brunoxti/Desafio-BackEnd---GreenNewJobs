using System.Net.Http.Json;

namespace GreenNewJobs.Tests
{
    public class AuthTests
    {
        private static readonly HttpClient _client = new HttpClient();

        public AuthTests()
        {
            
            _client.BaseAddress = new Uri("http://localhost:5000/api/");
        }

        [Fact]
        public async Task Login_ReturnsToken()
        {
            var fullUrl = _client.BaseAddress + "auth/login";
            Console.WriteLine("Full login URL: " + fullUrl);

          
            var loginResponse = await _client.PostAsJsonAsync("auth/login", new
            {
                Username = "admin",
                Password = "password"
            });

            Console.WriteLine("Login response status code: " + loginResponse.StatusCode);
            loginResponse.EnsureSuccessStatusCode();

            var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();

            Assert.False(string.IsNullOrEmpty(loginResult.Token));
        }

        private class LoginResult
        {
            public string Token { get; set; }
        }
    }
}
