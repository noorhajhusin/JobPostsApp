using JobPostsManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPostsManagement.API.Contracts.V1.Requests
{
    public class UpdateCandidateRequest : UpdateUserRequest
    {
        public string StudyLevel { get; set; }
        public string Skills { get; set; }
        public string Qualifications { get; set; }
        public string Address { get; set; }
    }
}
