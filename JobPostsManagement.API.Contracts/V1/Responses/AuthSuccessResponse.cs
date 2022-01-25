namespace JobPostsManagement.API.Contracts.V1.Responses
{
    public class AuthSuccessResponse
    {
        public UserResponse User { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}