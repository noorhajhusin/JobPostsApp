﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JobPostsManagement.API.Models
{
    public class Candidate : BaseUser
    {
        public string StudyLevel { get; set; }
        public string Skills { get; set; }
        public string Qualifications { get; set; }
        public string Address { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; }
    }
}
