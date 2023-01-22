using System;
using BugAPI.Entities;
using BugAPI.Repository;
using BugAPI.Repository.Interfaces;
using BugAPI.Services.Interfaces;

namespace BugAPI.Services
{
	public class BugService : IBugService
	{
        private readonly IBugRepository _bugRepository;

		public BugService(IBugRepository bugRepository)
		{
            _bugRepository = bugRepository;
		}

        public IEnumerable<Bug> GetAllBugs()
        {
            return _bugRepository.GetAll();
        }
    }
}

