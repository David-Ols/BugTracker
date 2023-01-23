using System;
using BugTracker.Dtos;
using BugTracker.Models;
using BugTracker.Repository.Interfaces;
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

        public Task<IEnumerable<Bug>> GetAllBugs()
        {
            return _bugRepository.GetAllBugs();
        }

        public async Task<BugDto> GetByPublicId(string publicId)
        {
            var bug = await _bugRepository.GetByPublicId(publicId);

            if (bug == null) return null;

            var user = await _userRepository.GetById(bug.UserId);

            if (user == null) return null;

            return _bugMapper.Map(bug, user);
        }
    }
}

