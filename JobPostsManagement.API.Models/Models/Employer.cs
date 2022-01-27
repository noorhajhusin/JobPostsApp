using System;
using System.Collections.Generic;
using System.Text;

namespace JobPostsManagement.API.Models
{
    public class Employer : BaseUser
    {
        public virtual ICollection<JobPost> JobPosts { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyPhone { get; set; }
    }
}
