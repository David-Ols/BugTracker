using System;
using BugAPI.Entities;
using BugAPI.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace BugAPI.Repository
{
	public class BugRepository : IBugRepository
	{
        private readonly IMemoryCache _memoryCache;
        private const string _bugKey = "Bug";

        public BugRepository(IMemoryCache memoryCache)
		{
            _memoryCache = memoryCache;

            var initialBugs = new List<Bug>() {
                new Bug{
                    Id = Guid.NewGuid(),
                    Description = "Description",
                    OpenedOn = new DateTime(2023, 01, 01),
                    PublicId = "Bug-1",
                    Status = "Open",
                    Title = "Title 1"
                },
                new Bug{
                    Id = Guid.NewGuid(),
                    Description = "Description 2",
                    OpenedOn = new DateTime(2023, 01, 02),
                    PublicId = "Bug-2",
                    Status = "Closed",
                    Title = "Title 2"
                },
                new Bug{
                    Id = Guid.NewGuid(),
                    Description = "Description 3",
                    OpenedOn = new DateTime(2023, 01, 03),
                    PublicId = "Bug-3",
                    Status = "Open",
                    Title = "Title 3"
                },
            };
            _memoryCache.Set(_bugKey, initialBugs);
        }

        public IEnumerable<Bug> GetAll()
        {
            return _memoryCache.Get<List<Bug>>(_bugKey);
        }


    }
}

