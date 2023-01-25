using System;
using BugTracker.Models;

namespace BugTracker.Services.Interfaces
{
	public interface IUserService
	{
		Task<User> CreateUser(string name);
		Task<IEnumerable<User>> GetAll();
		Task<bool> UpdateUser(User user);
    }
}

