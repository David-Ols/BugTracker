using System;
namespace BugTracker.Models
{
	public class Bug
	{
        public Guid Id { get; set; }
        public string PublicId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime OpenedOn { get; set; }
        public User AssignedUser { get; set; }
    }
}

