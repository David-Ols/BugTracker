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

            //var initialUsers = new List<User>() {
            //    new User{
            //        Id = new Guid("d8ed78a1-032c-4ed9-bac3-036fb65fd41d"),
            //        Name = "David"
            //    },
            //    new User{
            //        Id = new Guid("f1e43af1-9373-4b03-b2f9-3eec1eedeb58"),
            //        Name = "Jack"
            //    }
            //};
            //_memoryCache.Set(_userKey, initialUsers);
        }

        public User GetById(Guid id)
        {
            var users = _memoryCache.Get<List<User>>(_userKey);

            return users.FirstOrDefault(b => b.Id == id);
        }

        public User Create(User newUser)
        {
            var users = _memoryCache.Get<List<User>>(_userKey);
            if (users == null) users = new List<User>();

            users.Add(newUser);

            _memoryCache.Set(_userKey, users);

            return newUser;
        }

        public IEnumerable<User> GetAll()
        {
            return _memoryCache.Get<List<User>>(_userKey);
        }

        public bool Update(User user)
        {
            var users = _memoryCache.Get<List<User>>(_userKey);
            var userToUpdate = users.FirstOrDefault(u => u.Id == user.Id);

            if (userToUpdate == null) return false;

            userToUpdate.Name = user.Name;

            users.RemoveAll(u => u.Id == user.Id);
            users.Add(userToUpdate);

            _memoryCache.Set(_userKey, users);

            return true;
        }
    }
}

