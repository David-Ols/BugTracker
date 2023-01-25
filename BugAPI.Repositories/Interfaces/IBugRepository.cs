using System;
using BugAPI.Entities;
using BugAPI.Models;

namespace BugAPI.Repositories.Interfaces
{
	public interface IBugRepository
	{
		IEnumerable<Bug> GetAll();
		Bug GetByPublicId(string publicId);
        Bug GetById(Guid id);
        Bug Create(Bug bug);
		string BuildPublicId();
		bool UpdateBugStatus(BugStatusUpdate request);
		bool Update(Bug bug);
    }
}

