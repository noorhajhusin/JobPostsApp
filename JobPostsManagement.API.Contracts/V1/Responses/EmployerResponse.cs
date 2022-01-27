﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JobPostsManagement.API.Contracts.V1.Responses
{
    public class EmployerResponse: UserResponse
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyPhone { get; set; }
    }
}
