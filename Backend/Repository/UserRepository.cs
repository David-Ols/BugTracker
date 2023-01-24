using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using BugTracker.Models;
using BugTracker.Repository.Interfaces;

namespace BugTracker.Repository
{
	public class UserRepository : IUserRepository
	{
        private readonly HttpClient _client;
        private const string baseUrl = "https://localhost:7026";

        public UserRepository()
		{
            _client = new HttpClient();

            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<User> GetById(Guid userId)
        {
            var response = await _client.GetAsync($"/user/{userId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            else
            {
                return null;
            }
        }

        public async Task<User> Create(string name)
        {
            try
            {
                var stringContent = new StringContent(
                    JsonSerializer.Serialize(name),
                    UnicodeEncoding.UTF8,
                    "application/json"
                );

                var response = await _client.PostAsync($"/user", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<User>();
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                return null;
            }

        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var response = await _client.GetAsync($"/user");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<User>>();
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateUser(User user)
        {
            var stringContent = new StringContent(
                JsonSerializer.Serialize(user),
                UnicodeEncoding.UTF8,
                "application/json"
            );
            var response = await _client.PutAsync($"/user", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
                return false;
            }
        }
    }
}

