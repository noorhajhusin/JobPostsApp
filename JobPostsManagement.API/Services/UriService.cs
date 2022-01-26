using JobPostsManagement.API.Contracts;
using JobPostsManagement.API.Interfaces;
using JobPostsManagement.API.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPostsManagement.API.Services
{
    public class UriService : IUriService
    {
        private string baseUri;
        private readonly IConfiguration configuration;

        public UriService(string absoluteUri, IConfiguration configuration)
        {
            this.baseUri = absoluteUri;
            this.configuration = configuration;
        }

        public Uri GetPageUri(string endpoint, int pageNumber, int pageSize)
        {
            if (pageNumber == 0) return null;
            var uri = new Uri(baseUri + "/" + endpoint);

            var modifiedUri = QueryHelpers.AddQueryString(baseUri, "pageNumber", pageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pageSize.ToString());

            return new Uri(modifiedUri);
        }
        public Uri GetJobPostUri(JobPost jobPost)
        {
            return new Uri(baseUri + "/" + ApiRoutes.JobPostsRoutes.GetById.Replace("{jobPostId}", jobPost.Id.ToString()));
        }
        public Uri GetJobApplicationUri(JobApplication jobApplication)
        {
            return new Uri(baseUri + "/" + ApiRoutes.JobApplicationsRoutes.GetById.Replace("{jobApplicationId}", jobApplication.Id.ToString()));
        }
    }
}
