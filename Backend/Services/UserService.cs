using System;
using BugTracker.Models;
using BugTracker.Repository.Interfaces;
using BugTracker.Services.Interfaces;

namespace BugTracker.Services
{
	public class UserService : IUserService
	{
        private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
            _userRepository = userRepository;
		}

        public async Task<User> CreateUser(string name)
        {
            return await _userRepository.Create(name);
        }
    }
}

