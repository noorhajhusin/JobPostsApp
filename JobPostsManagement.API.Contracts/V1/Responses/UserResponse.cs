using JobPostsManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPostsManagement.API.Contracts.V1.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
    }
}
