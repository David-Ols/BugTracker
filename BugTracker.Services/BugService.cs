using System;
using BugTracker.Models.Dtos;
using BugTracker.Models;
using BugTracker.Repositories.Interfaces;
using BugTracker.Services.Interfaces;
using BugTracker.Mappers.Interfaces;

namespace BugTracker.Services
{
	public class BugService : IBugService
	{
        private readonly IBugRepository _bugRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBugMapper _bugMapper;

        public BugService(IBugRepository bugRepository, IBugMapper bugMapper,
            IUserRepository userRepository)
		{
            _bugRepository = bugRepository;
            _bugMapper = bugMapper;
            _userRepository = userRepository;
		}

        public async Task<Bug> CreateBug(CreateBug bug)
        {
            return await _bugRepository.CreateBug(bug);
        }

        public async Task<IEnumerable<Bug>> GetAllBugs()
        {
            return await _bugRepository.GetAllBugs();
        }

        public async Task<BugDto> GetByPublicId(string publicId)
        {
            User user = null;
            var bug = await _bugRepository.GetByPublicId(publicId);

            if (bug == null) return null;

            if(bug.UserId != null)
            {
                user = await _userRepository.GetById((Guid)bug.UserId);
            }

            return _bugMapper.Map(bug, user);
        }

        public async Task<bool> Update(BugUpdate request)
        {
            return await _bugRepository.Update(request);
        }

        public async Task<bool> UpdateBugStatus(BugStatusUpdate request)
        {
            return await _bugRepository.UpdateBugStatus(request);
        }
    }
}

