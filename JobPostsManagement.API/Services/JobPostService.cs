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
    public class JobPostService : IJobPostService
    {
        private readonly DbContext context;

        public JobPostService(DbContext context)
        {
            this.context = context;
        }

        #region JobPost service


        public async Task<List<JobPost>> GetAllAsync(int? pageNumber, int? pageSize)
        {
            var jobPostsList = context.JobPosts.AsQueryable();
            if (pageNumber is null || pageNumber.Value <= 0 | pageSize is null || pageSize.Value <= 0)
            {
                return await jobPostsList.ToListAsync();
            }
            var skip = (pageNumber.Value - 1) * pageSize.Value;
            return await jobPostsList.Skip(skip).Take(pageSize.Value).ToListAsync();
        }
        public async Task<bool> CreateAsync(JobPost createdJobPost)
        {
            await context.JobPosts.AddAsync(createdJobPost);
            var created = await context.SaveChangesAsync();
            return created > 0;
        }
        public async Task<JobPost> GetByIdAsync(long jobpostId,bool include=true)
        {
            if (include)
            {
                return await context.JobPosts.AsNoTracking()
                    .SingleOrDefaultAsync(c => c.Id == jobpostId);
            }
            return await context.JobPosts .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Id == jobpostId);
        }

        public async Task<bool> UpdateAsync(JobPost updatedJobPost)
        {
            context.JobPosts.Update(updatedJobPost);
            var updated = await context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(JobPost deletedJobPost)
        {
            var deleted = await context.SaveChangesAsync();
            return deleted > 0;
        }
        public async Task<int> GetCount()
        {
            return await context.JobPosts.CountAsync();
        }
        #endregion
    }
}
