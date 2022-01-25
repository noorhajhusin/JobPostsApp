using JobPostsManagement.API.Models;
using System;

namespace JobPostsManagement.API.Contracts.V1.Requests
{
    public class RegisterEmployerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}