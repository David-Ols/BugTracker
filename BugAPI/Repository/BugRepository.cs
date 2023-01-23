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
                    PublicId = "Bug1",
                    Status = "Open",
                    Title = "Title 1",
                    UserId = new Guid("d8ed78a1-032c-4ed9-bac3-036fb65fd41d")
                },
                new Bug{
                    Id = Guid.NewGuid(),
                    Description = "Description 2",
                    OpenedOn = new DateTime(2023, 01, 02),
                    PublicId = "Bug2",
                    Status = "Closed",
                    Title = "Title 2",
                    UserId = new Guid("f1e43af1-9373-4b03-b2f9-3eec1eedeb58")
                },
                new Bug{
                    Id = Guid.NewGuid(),
                    Description = "Description 3",
                    OpenedOn = new DateTime(2023, 01, 03),
                    PublicId = "Bug3",
                    Status = "Open",
                    Title = "Title 3",
                    UserId = new Guid("d8ed78a1-032c-4ed9-bac3-036fb65fd41d")
                },
            };
            _memoryCache.Set(_bugKey, initialBugs);
        }

        public IEnumerable<Bug> GetAll()
        {
            return _memoryCache.Get<List<Bug>>(_bugKey);
        }

        public Bug GetByPublicId(string publicId)
        {
            var bugs = _memoryCache.Get<List<Bug>>(_bugKey);

            return bugs.FirstOrDefault(b => b.PublicId == publicId);
        }
    }
}

