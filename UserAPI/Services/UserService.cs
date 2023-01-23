using System;
using UserAPI.Entities;
using UserAPI.Repositories.Interfaces;
using UserAPI.Services.Interfaces;

namespace UserAPI.Services
{
	public class UserService : IUserService
	{
        private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
            _userRepository = userRepository;
		}

        public User GetById(Guid id)
        {
            return _userRepository.GetById(id);
        }
    }
}

