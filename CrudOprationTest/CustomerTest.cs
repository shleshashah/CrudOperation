using NUnit.Framework;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CrudOprationTest
{
    public class CustomerTest
    {
        private readonly HttpClient _client;

        public CustomerTest()
        {
            // Initialize your HttpClient
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44345"); // Adjust the base address as per your API URL
        }

        [Test]
        public async Task GetAllCustomers_ReturnsSuccessStatusCode()
        {
            // Arrange
            var response = await _client.GetAsync("/api/CustomersAPI/GetAllCustomer");

            // Act & Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task CreateCustomer_ReturnsSuccessStatusCode()
        {

            // Arrange
            var content = new StringContent("{ \"id\": 0,\"firstName\": \"John\", \"lastName\": \"Doe\",\"password\": \"1234\",\"confirmPassword\": \"1234\",\"loginUser\": \"johnDoe\", \"Email\": \"john@example.com\", \"PhoneNumber\": \"1234567890\" }", Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/CustomersAPI/AddCustomer", content);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task UpdateCustomer_ReturnsSuccessStatusCode()
        {

            // Arrange
            var content = new StringContent("{ \"id\": 1,\"firstName\": \"John\", \"lastName\": \"Doe\",\"password\": \"1234\",\"confirmPassword\": \"1234\",\"loginUser\": \"johnDoe\", \"Email\": \"johnDoe@example.com\", \"PhoneNumber\": \"1234567890\" }", Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/api/CustomersAPI/EditCustomer", content);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task GetCustomer_ReturnsSuccessStatusCode()
        {

            // Act
            var response = await _client.GetAsync("/api/CustomersAPI/GetCustomer/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Test]
        public async Task DeleteCustomer_ReturnsSuccessStatusCode()
        {

            // Act
            var response = await _client.DeleteAsync("/api/CustomersAPI/DeleteCustomer/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}