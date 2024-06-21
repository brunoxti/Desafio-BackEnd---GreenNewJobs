using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GreenNewJobs.Tests
{
    public class DeliveryPersonTests
    {
        private readonly HttpClient _client;
        private string _token;

        public DeliveryPersonTests()
        {
            _client = new HttpClient();
            _client.BaseAddress = new System.Uri("http://localhost:5000/api/"); // URL da sua API
        }

        private async Task AuthenticateAsync()
        {
            if (string.IsNullOrEmpty(_token))
            {
                var authTests = new AuthTests();
                var loginResponse = await _client.PostAsJsonAsync("auth/login", new
                {
                    Username = "admin",
                    Password = "password"
                });

                loginResponse.EnsureSuccessStatusCode();

                var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();

                _token = loginResult.Token;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            }
        }

        [Fact]
        public async Task CreateAndGetDeliveryPerson()
        {
            await AuthenticateAsync();

            // Criar um novo driver
            var createDeliveryPersonResponse = await _client.PostAsJsonAsync("drivers", new
            {
                name = "Otavio",
                birthDate = "2010-06-17T19:31:03.345Z",
                cnhNumber = "9d84477777",
                cnpj = "232333",
                cnhType = "A"
                
            });

            createDeliveryPersonResponse.EnsureSuccessStatusCode();

            var createdDeliveryPerson = await createDeliveryPersonResponse.Content.ReadFromJsonAsync<DeliveryPerson>();

            // Consultar o driver criado
            var getDeliveryPersonResponse = await _client.GetAsync($"/drivers");

            getDeliveryPersonResponse.EnsureSuccessStatusCode();

            var fetchedDeliveryPerson = await getDeliveryPersonResponse.Content.ReadFromJsonAsync<DeliveryPerson>();

            Assert.Equal(createdDeliveryPerson.Name, fetchedDeliveryPerson.Name);
            Assert.Equal("John Doe", fetchedDeliveryPerson.Name);
        }

        private class LoginResult
        {
            public string Token { get; set; }
        }

        public class DeliveryPerson
        {
            public string Name { get; set; }
            public string CNPJ { get; set; }
            public string BirthDate { get; set; }
            public string CNHNumber { get; set; }
            public string CNHType { get; set; }
        }
    }
}
