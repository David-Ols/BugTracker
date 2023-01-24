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

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<bool> UpdateUser(User user)
        {
            return await _userRepository.UpdateUser(user);
        }
    }
}

