using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPostsManagement.API.Models
{
    public class JobApplication: BaseModel
    {
        public DateTime Date { get; set; }
        public string CoverLetter { get; set; }
        public ApplicationStatus Status { get; set; }
        public long JobPostId { get; set; }

        [ForeignKey(nameof(JobPostId))]
        public virtual JobPost JobPost { get; set; }
        public string CandidateId { get; set; }

        [ForeignKey(nameof(CandidateId))]
        public virtual Candidate Candidate { get; set; }
    }
}
