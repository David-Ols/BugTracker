using System;
namespace BugAPI.Entities
{
    public class Bug
    {
        public Guid Id { get; set; }
        public string PublicId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime OpenedOn { get; set; }
        public Guid UserId { get; set; }
    }
}

