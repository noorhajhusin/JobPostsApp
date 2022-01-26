using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPostsManagement.API.Contracts.V1.Requests
{
    public class CreateUpdateJobApplicationRequest
    {
        public DateTime Date { get; set; }
        public string CoverLetter { get; set; }
        public long JobPostId { get; set; }
        public string CandidateId { get; set; }
    }
}
