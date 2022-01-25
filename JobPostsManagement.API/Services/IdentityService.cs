using AutoMapper;
using JobPostsManagement.API.Configurations;
using JobPostsManagement.API.Contracts.V1;
using JobPostsManagement.API.Contracts.V1.Responses;
using JobPostsManagement.API.Interfaces;
using JobPostsManagement.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DbContext = JobPostsManagement.API.Data.DbContext;

namespace JobPostsManagement.API.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<BaseUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JwtConfigs jwtConfigs;
        private readonly TokenValidationParameters tokenValidationParameters;
        private readonly DbContext context;
        private readonly IMapper mapper;

        public IdentityService(UserManager<BaseUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager, JwtConfigs jwtConfigs, TokenValidationParameters tokenValidationParameters, DbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.jwtConfigs = jwtConfigs;
            this.tokenValidationParameters = tokenValidationParameters;
            this.context = context;
        }

        public async Task<AuthenticationResult> RegisterEmployerAsync(Employer createdUser, string password)
        {
            var existingUser = await userManager.FindByEmailAsync(createdUser.Email);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Error = new ErrorResponse{Description= "User with this email address already exists" }
                };
            }
            createdUser.UserName = createdUser.Email;

            var newUser = await userManager.CreateAsync(createdUser, password);

            await userManager.AddToRoleAsync(createdUser, "Employer");

            if (!newUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Error = new ErrorResponse{Code="", Description= "" }
                };
            }

            return await GenerateAuthResult(createdUser);
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Error = new ErrorResponse { Code = "", Description="User does not exist" }
                };
            }

            var userHasValidPassword = await userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Error = new ErrorResponse { Code = "", Description = "User/password combination is wrong" }
                };
            }

            return await GenerateAuthResult(user);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);

            if (validatedToken == null)
            {
                return new AuthenticationResult
                {
                    Error = new ErrorResponse { Code = "InvalidToken", Description = "Invalid Token" } };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

            if (storedRefreshToken == null)
            {
                return new AuthenticationResult
                {
                    Error = new ErrorResponse { Code = "TokenNotExist", Description = "This refresh token does not exist" } };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult
                {
                    Error = new ErrorResponse { Code = "RefreshTokenExpired", Description = "This refresh token has expired" } };
            }

            if (storedRefreshToken.Invalidated)
            {
                return new AuthenticationResult
                {
                    Error = new ErrorResponse { Code = "RefreshTokenInvalid", Description = "This refresh token has been invalidated" } };
            }

            if (storedRefreshToken.Used)
            {
                return new AuthenticationResult
                {
                    Error = new ErrorResponse { Code = "RefreshTokenUsed", Description = "This refresh token has been used" } };
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult
                {
                    Error = new ErrorResponse { Code = "RefreshTokenNotMatched", Description = "This refresh token does not match this JWT" } };
            }

            var user = await userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);


            var expiryDateUnix =
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult
                {
                    User = mapper.Map<UserResponse>(user),
                    Success = true,
                    Token = token,
                    RefreshToken = refreshToken
                };
            }

            storedRefreshToken.Used = true;
            context.RefreshTokens.Update(storedRefreshToken);
            await context.SaveChangesAsync();

            return await GenerateAuthResult(user);
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = this.tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task<AuthenticationResult> GenerateAuthResult(BaseUser User)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtConfigs.SecretKey);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, User.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Email", User.Email),
                new Claim("id", User.Id)
            };

            var userClaims = await userManager.GetClaimsAsync(User);
            claims.AddRange(userClaims);

            var userRoles = await userManager.GetRolesAsync(User);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await roleManager.FindByNameAsync(userRole);
                if (role == null) continue;
                var roleClaims = await roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim))
                        continue;

                    claims.Add(roleClaim);
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(jwtConfigs.TokenLifetime),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserID = User.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await context.RefreshTokens.AddAsync(refreshToken);
            await context.SaveChangesAsync();

            return new AuthenticationResult
            {
                User = mapper.Map<UserResponse>(User),
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }
    }
}