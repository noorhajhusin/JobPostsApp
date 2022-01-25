using Microsoft.AspNetCore.Http;
using JobPostsManagement.API.Contracts.V1.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobPostsManagement.API.Models;

namespace JobPostsManagement.API.Interfaces
{
    public interface IJobPostService
    {
        Task<List<JobPost>> GetAllAsync(int? pageNumber, int? pageSize);
        Task<JobPost> GetByIdAsync(long jobpostId, bool include = true);
        Task<bool> CreateAsync(JobPost createdJobPost);
        Task<bool> UpdateAsync(JobPost updatedJobPost);
        Task<bool> DeleteAsync(JobPost deletedJobPost);
        Task<int> GetCount();
    }
}
