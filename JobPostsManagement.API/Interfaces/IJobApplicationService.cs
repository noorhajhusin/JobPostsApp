using Microsoft.AspNetCore.Http;
using JobPostsManagement.API.Contracts.V1.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobPostsManagement.API.Models;

namespace JobPostsManagement.API.Interfaces
{
    public interface IJobApplicationService
    {
        Task<List<JobApplication>> GetAllAsync(int? pageNumber, int? pageSize);
        Task<JobApplication> GetByIdAsync(long jobpostId, bool include = true);
        Task<bool> CreateAsync(JobApplication createdJobApplication);
        Task<bool> UpdateAsync(JobApplication updatedJobApplication);
        Task<bool> DeleteAsync(JobApplication deletedJobApplication);
        Task<int> GetCount();
    }
}
