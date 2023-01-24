using System;
using BugTracker.Models;

namespace BugTracker.Repository.Interfaces
{
	public interface IBugRepository
	{
		Task<IEnumerable<Bug>> GetAllBugs();
		Task<Bug> GetByPublicId(string publicId);
		Task<Bug> CreateBug(CreateBug bug);
		Task<bool> UpdateBugStatus(UpdateBugStatus request);
    }
}

