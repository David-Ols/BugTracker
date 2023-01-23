using System;
using System.Net.Http.Headers;
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
    }
}

