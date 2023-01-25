using System;
using BugAPI.Entities;
using BugAPI.Enums;
using BugAPI.Models;
using BugAPI.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace BugAPI.Repositories
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

        public bool UpdateBugStatus(BugStatusUpdate request)
        {
            var bugs = GetAll().ToList();
            var bugToUpdate = bugs?.FirstOrDefault(b => b.Id == request.BugId);

            if (bugToUpdate == null) return false;

            bugToUpdate.Status = request.Status;

            if(bugToUpdate.Status == StatusEnum.closed.ToString())
            {
                bugToUpdate.OpenedOn = null;
            }

            bugs?.RemoveAll(b => b.Id == request.BugId);
            bugs?.Add(bugToUpdate);

            _memoryCache.Set(_bugKey, bugs);

            return true;
        }

        public Bug GetById(Guid id)
        {
            var bugs = GetAll().ToList();

            return bugs.FirstOrDefault(b => b.Id == id);
        }

        public bool Update(Bug bug)
        {
            var bugs = GetAll().ToList();
            var bugToReplace = bugs?.FirstOrDefault(b => b.Id == bug.Id);

            if (bugToReplace == null) return false;

            bugs?.RemoveAll(b => b.Id == bug.Id);
            bugs?.Add(bug);

            _memoryCache.Set(_bugKey, bugs);

            return true;
        }
    }
}

