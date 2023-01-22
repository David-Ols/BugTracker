using System;
using BugTracker.Models;

namespace BugTracker.Services.Interfaces
{
	public interface IBugService
	{
		Task<IEnumerable<Bug>> GetAllBugs();
	}
}

