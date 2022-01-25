using System;
using System.Collections.Generic;
using System.Text;

namespace JobPostsManagement.API.Models
{
    public class Employer : BaseUser
    {
        public virtual ICollection<JobPost> JobPosts { get; set; }
        public int MyProperty { get; set; }
    }
}
