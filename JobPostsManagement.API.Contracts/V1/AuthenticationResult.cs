using JobPostsManagement.API.Contracts.V1.Responses;
using System.Collections.Generic;

namespace JobPostsManagement.API.Contracts.V1
{
    public class AuthenticationResult
    {
        public ErrorResponse Error { get; set; }
        public UserResponse User { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}