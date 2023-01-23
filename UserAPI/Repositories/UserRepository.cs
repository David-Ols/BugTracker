using System;
using Microsoft.Extensions.Caching.Memory;
using UserAPI.Entities;
using UserAPI.Repositories.Interfaces;

namespace UserAPI.Repositories
{
	public class UserRepository : IUserRepository
	{
        private readonly IMemoryCache _memoryCache;
        private const string _userKey = "User";

        public UserRepository(IMemoryCache memoryCache)
		{
            _memoryCache = memoryCache;

            var initialUsers = new List<User>() {
                new User{
                    Id = new Guid("d8ed78a1-032c-4ed9-bac3-036fb65fd41d"),
                    Name = "David"
                },
                new User{
                    Id = new Guid("f1e43af1-9373-4b03-b2f9-3eec1eedeb58"),
                    Name = "Jack"
                }
            };
            _memoryCache.Set(_userKey, initialUsers);
        }

        public User GetById(Guid id)
        {
            var users = _memoryCache.Get<List<User>>(_userKey);

            return users.FirstOrDefault(b => b.Id == id);
        }
    }
}

