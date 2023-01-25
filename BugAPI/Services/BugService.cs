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

        public bool Update(BugUpdate request)
        {
            var bug = _bugRepository.GetById(request.BugId);

            if (bug == null) return false;

            bug.Description = request.Description;
            bug.Title = request.Title;
            bug.UserId = request.UserId;

            if(bug.Status != request.Status && request.Status == StatusEnum.open.ToString())
            {
                bug.OpenedOn = DateTime.Now;
            }
            else if(bug.Status != request.Status && request.Status == StatusEnum.closed.ToString())
            {
                bug.OpenedOn = null;
            }

            bug.Status = request.Status;

            return _bugRepository.Update(bug);
        }

        public bool UpdateBugStatus(BugStatusUpdate request)
        {
            return _bugRepository.UpdateBugStatus(request);
        }
    }
}

