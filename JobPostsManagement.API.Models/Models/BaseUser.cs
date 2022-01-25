using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JobPostsManagement.API.Models
{
    public class BaseUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; }
        public string ProfileImagePath { get; set; }
        public string ProfileThumbnailPath { get; set; }

        [JsonIgnore]
        [NotMapped]
        public string UserType
        {
            get
            {
                if (this is Employer)
                {
                    return "Employer";
                }
                else
                {
                    return "Candidate";
                }
            }
        }
    }
}