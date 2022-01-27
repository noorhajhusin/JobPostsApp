using JobPostsManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPostsManagement.API.Contracts.V1.Requests
{
    public class UpdateEmployerRequest : UpdateUserRequest
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyPhone { get; set; }
    }
}
