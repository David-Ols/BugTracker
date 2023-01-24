using System;
using BugAPI.Entities;
using BugAPI.Models;

namespace BugAPI.Repository.Interfaces
{
	public interface IBugRepository
	{
		IEnumerable<Bug> GetAll();
		Bug GetByPublicId(string publicId);
		Bug Create(Bug bug);
		string BuildPublicId();
		bool UpdateBugStatus(BugStatusUpdate request);
    }
}

