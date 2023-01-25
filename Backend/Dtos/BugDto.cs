using System;
namespace BugTracker.Dtos
{
	public class BugDto
	{
		public string PublicId { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string AssigneeName { get; set; }
        public string OpenedOn { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
    }
}

