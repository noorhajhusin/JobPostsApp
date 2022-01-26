using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using JobPostsManagement.API.Contracts.V1.Requests;
using JobPostsManagement.API.Interfaces;
using JobPostsManagement.API.Models;
using JobPostsManagement.API.Data;
using DbContext = JobPostsManagement.API.Data.DbContext;

namespace JobPostsManagement.API.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly DbContext context;

        public JobApplicationService(DbContext context)
        {
            this.context = context;
        }

        #region JobApplication service


        public async Task<List<JobApplication>> GetAllAsync(int? pageNumber, int? pageSize)
        {
            var jobApplicationsList = context.JobApplications.AsQueryable();
            if (pageNumber is null || pageNumber.Value <= 0 | pageSize is null || pageSize.Value <= 0)
            {
                return await jobApplicationsList.ToListAsync();
            }
            var skip = (pageNumber.Value - 1) * pageSize.Value;
            return await jobApplicationsList.Skip(skip).Take(pageSize.Value).ToListAsync();
        }
        public async Task<bool> CreateAsync(JobApplication createdJobApplication)
        {
            await context.JobApplications.AddAsync(createdJobApplication);
            var created = await context.SaveChangesAsync();
            return created > 0;
        }
        public async Task<JobApplication> GetByIdAsync(long jobpostId,bool include=true)
        {
            if (include)
            {
                return await context.JobApplications.AsNoTracking()
                    .SingleOrDefaultAsync(c => c.Id == jobpostId);
            }
            return await context.JobApplications .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Id == jobpostId);
        }

        public async Task<bool> UpdateAsync(JobApplication updatedJobApplication)
        {
            context.JobApplications.Update(updatedJobApplication);
            var updated = await context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(JobApplication deletedJobApplication)
        {
            var deleted = await context.SaveChangesAsync();
            return deleted > 0;
        }
        public async Task<int> GetCount()
        {
            return await context.JobApplications.CountAsync();
        }
        #endregion
    }
}
