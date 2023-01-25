using System;
namespace BugTracker.Models
{
	public class CreateBug
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? UserId { get; set; }
    }
}

