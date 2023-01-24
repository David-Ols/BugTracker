using System;
using BugTracker.Models;

namespace BugTracker.Repository.Interfaces
{
	public interface IUserRepository
	{
		Task<User> GetById(Guid userId);
		Task<User> Create(string name);
	}
}

