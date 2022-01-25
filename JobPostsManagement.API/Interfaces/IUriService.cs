using JobPostsManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPostsManagement.API.Interfaces
{
    public interface IUriService
    {
        Uri GetPageUri(string endpoint, int pageNumber, int pageSize);
        Uri GetJobPostUri(JobPost jobPost);
    }
}
