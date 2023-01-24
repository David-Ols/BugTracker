using System;
using UserAPI.Entities;

namespace UserAPI.Repositories.Interfaces
{
	public interface IUserRepository
	{
		User GetById(Guid id);
		User Create(User newUser);
		IEnumerable<User> GetAll();
		bool Update(User user);
    }
}

