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
        }

        public IEnumerable<Bug> GetAll()
        {
            var bugs = _memoryCache.Get<IEnumerable<Bug>>(_bugKey);

            if (bugs == null)
            {
                bugs = Enumerable.Empty<Bug>();
            }
            return bugs;
        }

        public Bug GetByPublicId(string publicId)
        {
            var bugs = _memoryCache.Get<List<Bug>>(_bugKey);

            return bugs.FirstOrDefault(b => b.PublicId == publicId);
        }

        public Bug Create(Bug bug)
        {
            var bugs = _memoryCache.Get<List<Bug>>(_bugKey);
            if (bugs == null) bugs = new List<Bug>();

            bugs.Add(bug);

            _memoryCache.Set(_bugKey, bugs);

            return bug;
        }

        public string BuildPublicId()
        {
            var bugs = GetAll();


            return $"Bug-{((bugs?.Count() ?? 0) + 1)}";
        }
    }
}

