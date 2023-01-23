using System;
using BugAPI.Entities;

namespace BugAPI.Services.Interfaces
{
	public interface IBugService
	{
		IEnumerable<Bug> GetAllBugs();
		Bug GetByPublicId(string publicId);
    }
}

