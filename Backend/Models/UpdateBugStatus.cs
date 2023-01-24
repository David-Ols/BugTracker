using System;
namespace BugTracker.Models
{
	public class UpdateBugStatus
	{
		public Guid BugId { get; set; }
		public string Status { get; set; }
	}
}

