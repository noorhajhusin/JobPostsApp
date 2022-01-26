using JobPostsManagement.API.Models;
using System;

namespace JobPostsManagement.API.Contracts.V1.Responses
{
    public class JobApplicationResponse
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string CoverLetter { get; set; }
        public ApplicationStatus Status { get; set; }
        public long JobPostId { get; set; }
        public  JobPostResponse JobPost { get; set; }
        public string CandidateId { get; set; }
        public UserResponse Candidate { get; set; }
    }
}