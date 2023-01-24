using System;
using BugAPI.Entities;
using BugAPI.Models;

namespace BugAPI.Services.Interfaces
{
	public interface IBugService
	{
		IEnumerable<Bug> GetAllBugs();
		Bug GetByPublicId(string publicId);
		Bug Create(CreateBug bug);
		bool UpdateBugStatus(BugStatusUpdate request);
    }
}

