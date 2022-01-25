using System;

namespace JobPostsManagement.API.Configurations
{
    public class JwtConfigs
    {
        public string SecretKey { get; set; }
        public TimeSpan TokenLifetime { get; set; }
    }
}