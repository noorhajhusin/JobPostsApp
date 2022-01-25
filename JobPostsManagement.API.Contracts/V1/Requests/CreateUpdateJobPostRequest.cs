using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPostsManagement.API.Contracts.V1.Requests
{
    public class CreateUpdateJobPostRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DeadLineDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
