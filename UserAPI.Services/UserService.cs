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

        public User Create(string name)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            return _userRepository.Create(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(Guid id)
        {
            return _userRepository.GetById(id);
        }

        public bool Update(User user)
        {
            return _userRepository.Update(user);
        }
    }
}

