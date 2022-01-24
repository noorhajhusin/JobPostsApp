using System;

namespace JobPostsManagement.API.Models
{
    public class JobPost
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DeadLineDate { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
    }
}
