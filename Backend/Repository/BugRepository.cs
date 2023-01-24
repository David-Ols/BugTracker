﻿using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using BugTracker.Models;
using BugTracker.Repository.Interfaces;

namespace BugTracker.Repository
{
	public class BugRepository : IBugRepository
    {
		private readonly HttpClient _client;
        private const string baseUrl = "https://localhost:7283";

        public BugRepository()
		{
			_client = new HttpClient();

            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

		public async Task<IEnumerable<Bug>> GetAllBugs()
		{
			var response = await _client.GetAsync("/bug");

            if (response.IsSuccessStatusCode)
            {
                var bugs = await response.Content.ReadFromJsonAsync<IEnumerable<Bug>>();

                return bugs;
            }
            else
            {
                return null;
            }
        }

        public async Task<Bug> GetByPublicId(string publicId)
        {
            try
            {
                var response = await _client.GetAsync($"/bug/{publicId}");

                if (response.IsSuccessStatusCode)
                {
                    var bug = await response.Content.ReadFromJsonAsync<Bug>();

                    return bug;
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

        public async Task<Bug> CreateBug(CreateBug bug)
        {
            var stringContent = new StringContent(
                JsonSerializer.Serialize(bug),
                UnicodeEncoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync($"/bug", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Bug>();
            }
            else
            {
                return null;
            }
        }
    }
}

