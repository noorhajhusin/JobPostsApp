using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPostsManagement.API.Models
{
    public class JobPost : BaseModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DeadLineDate { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string EmployerId { get; set; }

        [ForeignKey(nameof(EmployerId))]
        public virtual Employer Employer { get; set; }
    }
}
