using System;

namespace JobPostsManagement.API.Contracts.V1.Responses
{
    public class JobPostResponse
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DeadLineDate { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string EmployerId { get; set; }
        public EmployerResponse Employer { get; set; }
    }
}