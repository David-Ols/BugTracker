using System;
using UserAPI.Entities;

namespace UserAPI.Services.Interfaces
{
	public interface IUserService
	{
		User GetById(Guid id);
		User Create(string name);
		IEnumerable<User> GetAll();
	}
}

