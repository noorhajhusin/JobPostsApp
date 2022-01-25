using JobPostsManagement.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace JobPostsManagement.API.Data
{
    public class DbContext : IdentityDbContext<BaseUser>
    {
        public DbContext() : base()
        {
        }

        public DbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var entityType in builder.Model.GetEntityTypes().Select(et => et.ClrType))
            {
                if (entityType.Name == nameof(BaseUser))
                {
                    var param = Expression.Parameter(entityType);
                    var filter = (Expression<Func<BaseUser, bool>>)(e => e.IsDeleted == false);
                    var exp = ReplacingExpressionVisitor.Replace(filter.Parameters.Single(), param, filter.Body);
                    builder.Entity(entityType).HasQueryFilter(Expression.Lambda(exp, param));
                }
                else if (typeof(BaseModel).IsAssignableFrom(entityType))
                {
                    var param = Expression.Parameter(entityType);
                    var filter = (Expression<Func<BaseModel, bool>>)(e => e.IsDeleted == false);
                    var exp = ReplacingExpressionVisitor.Replace(filter.Parameters.Single(), param, filter.Body);
                    builder.Entity(entityType).HasQueryFilter(Expression.Lambda(exp, param));
                }
            }
        }
        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (typeof(BaseModel).IsAssignableFrom(entry.Entity.GetType())
                 || typeof(BaseUser).IsAssignableFrom(entry.Entity.GetType()))
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["CreatedAt"] = DateTime.UtcNow;
                            entry.CurrentValues["IsDeleted"] = false;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues["UpdatedAt"] = DateTime.UtcNow;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["IsDeleted"] = true;
                            entry.CurrentValues["UpdatedAt"] = DateTime.UtcNow;
                            break;
                    }
                }
            }
        }

        public virtual DbSet<Employer> Employers { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<JobPost> JobPosts { get; set; }
        public virtual DbSet<JobApplication> JobApplications { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}