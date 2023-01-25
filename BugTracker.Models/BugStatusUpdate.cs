using System;
namespace BugTracker.Models
{
	public class BugStatusUpdate
	{
		public Guid BugId { get; set; }
		public string Status { get; set; }
	}
}

