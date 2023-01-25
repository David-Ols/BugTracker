using System;
namespace BugTracker.Models
{
	public class BugUpdate
	{
        public string Status { get; set; }
        public Guid? UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid BugId { get; set; }
    }
}

