namespace JobPostsManagement.API.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string CurrentVersion = "V1";
        public const string Prefix = Root + "/" + CurrentVersion;

        public static class IdentityRoutes
        {
            public const string RegisterEmployer = Prefix + "/Identity/RegisterEmployer";
            public const string RegisterCandidate = Prefix + "/Identity/RegisterCandidate";
            public const string Login = Prefix + "/Identity/Login";
            public const string Refresh = Prefix + "/Identity/Refresh";
            public const string CheckIfAuthorized = Prefix + "/Identity/CheckIfAuthorized";
        }
        public static class EmployersRoutes
        {
            public const string GetAll = Prefix + "/employers";
            public const string GetAllIdle = Prefix + "/employers/idle";
            public const string GetById = Prefix + "/employers/{employerId}";
            public const string Update = Prefix + "/employers/{employerId}";
            public const string Delete = Prefix + "/employers/{employerId}";
        }
        public static class JobPostsRoutes
        {
            public const string GetAll = Prefix + "/jobPosts";
            public const string Create = Prefix + "/jobPosts";
            public const string GetById = Prefix + "/jobPosts/{jobPostId}";
            public const string Update = Prefix + "/jobPosts/{jobPostId}";
            public const string Delete = Prefix + "/jobPosts/{jobPostId}";
        }
        public static class JobApplicationsRoutes
        {
            public const string GetAll = Prefix + "/jobApplications";
            public const string Create = Prefix + "/jobApplications";
            public const string GetById = Prefix + "/jobApplications/{jobApplicationId}";
            public const string Update = Prefix + "/jobApplications/{jobApplicationId}";
            public const string Delete = Prefix + "/jobApplications/{jobApplicationId}";
        }
    }
}