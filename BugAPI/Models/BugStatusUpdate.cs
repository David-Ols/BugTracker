using System;
namespace BugAPI.Models
{
	public class BugStatusUpdate
	{
        public Guid BugId { get; set; }
        public string Status { get; set; }
    }
}

