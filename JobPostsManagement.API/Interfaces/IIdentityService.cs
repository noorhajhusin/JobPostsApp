using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using JobPostsManagement.API.Contracts.V1;
using JobPostsManagement.API.Models;

namespace JobPostsManagement.API.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterEmployerAsync(Employer createdUser, string password);

        Task<AuthenticationResult> LoginAsync(string email, string password);

        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
    }
}