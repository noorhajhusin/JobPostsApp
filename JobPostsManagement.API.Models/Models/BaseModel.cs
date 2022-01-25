using System;
using System.ComponentModel.DataAnnotations;

namespace JobPostsManagement.API.Models
{
    public class BaseModel
    {
        public virtual long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}