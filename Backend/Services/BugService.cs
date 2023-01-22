using System;
using BugTracker.Models;
using BugTracker.Repository.Interfaces;
using BugTracker.Services.Interfaces;

namespace BugTracker.Services
{
	public class BugService : IBugService
	{
        private readonly IBugRepository _bugRepository;

		public BugService(IBugRepository bugRepository)
		{
            _bugRepository = bugRepository;
		}

        public Task<IEnumerable<Bug>> GetAllBugs()
        {
            return _bugRepository.GetAllBugs();
        }
    }
}

