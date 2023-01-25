using System;
using BugTracker.Models.Dtos;
using BugTracker.Mappers.Interfaces;
using BugTracker.Models;

namespace BugTracker.Mappers
{
	public class BugMapper : IBugMapper
    {
		
		public BugDto Map(Bug bug, User user)
		{
			return new BugDto
			{
				AssigneeName = user?.Name,
				Description = bug.Description,
				OpenedOn = bug.OpenedOn?.ToShortDateString() ?? "",
				PublicId = bug.PublicId,
				Status = bug.Status,
				Title = bug.Title,
				Id = bug.Id,
				UserId = user?.Id
			};
		}
	}
}

