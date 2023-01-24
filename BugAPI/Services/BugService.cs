using System;
using BugAPI.Entities;
using BugAPI.Enums;
using BugAPI.Models;
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

        public Bug Create(CreateBug bug)
        {
            var bugRequest = new Bug
            {
                Id = Guid.NewGuid(),
                Description = bug.Description,
                Title = bug.Title,
                Status = StatusEnum.closed.ToString(),
                UserId = bug.UserId,
                PublicId = _bugRepository.BuildPublicId()
            };

            return _bugRepository.Create(bugRequest);
        }

        public IEnumerable<Bug> GetAllBugs()
        {
            return _bugRepository.GetAll();
        }

        public Bug GetByPublicId(string publicId)
        {
            return _bugRepository.GetByPublicId(publicId);
        }
    }
}

