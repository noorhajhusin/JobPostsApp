using JobPostsManagement.API.Models;
using System;
using System.Collections.Generic;

namespace JobPostsManagement.API.Contracts.V1.Responses
{
    public class CandidateResponse : UserResponse
    {
        public string StudyLevel { get; set; }
        public string Skills { get; set; }
        public string Qualifications { get; set; }
        public string Address { get; set; }
        public virtual List<JobApplicationResponse> JobApplications { get; set; }
    }
}