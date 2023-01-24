using System;
using BugTracker.Models;

namespace BugTracker.Services.Interfaces
{
	public interface IUserService
	{
		Task<User> CreateUser(string name);
	}
}

