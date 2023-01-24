using System;
using BugAPI.Entities;

namespace BugAPI.Repository.Interfaces
{
	public interface IBugRepository
	{
		IEnumerable<Bug> GetAll();
		Bug GetByPublicId(string publicId);
		Bug Create(Bug bug);
		string BuildPublicId();
    }
}

