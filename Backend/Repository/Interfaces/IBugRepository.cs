using System;
using BugTracker.Models;

namespace BugTracker.Repository.Interfaces
{
	public interface IBugRepository
	{
		Task<IEnumerable<Bug>> GetAllBugs();
    }
}

