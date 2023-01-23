using System;
using BugTracker.Dtos;
using BugTracker.Models;

namespace BugTracker.Mappers.Interfaces
{
	public interface IBugMapper
	{
		BugDto Map(Bug bug, User user);
	}
}

