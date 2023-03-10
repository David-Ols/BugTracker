using System;
using BugTracker.Models;

namespace BugTracker.Repositories.Interfaces
{
	public interface IUserRepository
	{
		Task<User> GetById(Guid userId);
		Task<User> Create(string name);
		Task<IEnumerable<User>> GetAll();
		Task<bool> UpdateUser(User user);
    }
}

