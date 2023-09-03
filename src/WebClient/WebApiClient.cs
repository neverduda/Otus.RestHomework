using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebClient
{
    internal class WebApiClient: IDisposable
    {
        private readonly HttpClient _httpClient;

        public WebApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5000");
        }

        public async Task<Customer> GetCustomerByIdAsync(long id)
        {
            try
            {
                var endpoint = $"/customers/{id}";
                using var response = await _httpClient.GetAsync(endpoint);

                response.EnsureSuccessStatusCode();

                Customer? responseContent = await response.Content.ReadFromJsonAsync<Customer>();

                return responseContent;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка HTTP-запроса: {ex.Message}");
                throw;
            }
        }

        public async Task<long> AddCustomerAsync(CustomerCreateRequest customerReq)
        {
            try
            {
                var endpoint = $"/customers";
                var jsonCustomerReq = JsonConvert.SerializeObject(customerReq);
                var content = new StringContent(jsonCustomerReq, Encoding.UTF8, "application/json");
    

                using var response = await _httpClient.PostAsync(endpoint, content);

                response.EnsureSuccessStatusCode();

                long responseContent = await response.Content.ReadFromJsonAsync<long>();

                return responseContent;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка HTTP-запроса: {ex.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            // Освобождение ресурсов HttpClient при завершении работы.
            _httpClient.Dispose();
        }
    }
}
