using System;
using BugTracker.Dtos;
using BugTracker.Models;

namespace BugTracker.Services.Interfaces
{
	public interface IBugService
	{
		Task<IEnumerable<Bug>> GetAllBugs();
		Task<BugDto> GetByPublicId(string publicId);
		Task<Bug> CreateBug(CreateBug bug);
		Task<bool> UpdateBugStatus(UpdateBugStatus request);
    }
}

