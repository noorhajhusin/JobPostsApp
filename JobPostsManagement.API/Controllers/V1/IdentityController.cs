using AutoMapper;
using JobPostsManagement.API.Contracts;
using JobPostsManagement.API.Contracts.V1.Requests;
using JobPostsManagement.API.Contracts.V1.Responses;
using JobPostsManagement.API.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace JobPostsManagement.API.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        #region Fields

        private readonly IIdentityService identityService;
        private readonly ILogger<IdentityController> logger;
        private readonly IMapper mapper;

        #endregion Fields

        #region Ctor

        public IdentityController(IIdentityService identityService, IMapper mapper, ILogger<IdentityController> logger)
        {
            this.identityService = identityService;
            this.mapper = mapper;
            this.logger = logger;
        }

        #endregion Ctor

        #region Endpoints
        [AllowAnonymous]
        /// <summary>
        /// Registration of new Employer in the system
        /// </summary>
        /// <response code="200">Registration of new Employer has succeeded</response>
        /// <response code="400">Registration of new Employer has failed with errors</response>
        ///<returns>Jwt Token to attach it in authentication header in requests, and the RefreshToken.</returns>
        [HttpPost(ApiRoutes.IdentityRoutes.RegisterEmployer)]
        [ProducesResponseType(typeof(ObjectResponse<AuthSuccessResponse>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> RegisterEmployer([FromBody]RegisterEmployerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdEmployer = mapper.Map<Models.Employer>(request);

            var authResponse = await identityService.RegisterEmployerAsync(createdEmployer, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest();
            }

            return Ok(
                new AuthSuccessResponse
                {
                    User = authResponse.User,
                    Token = authResponse.Token,
                    RefreshToken = authResponse.RefreshToken
                }
            );
        }

        [AllowAnonymous]
        /// <summary>
        /// Login to the system using Username
        /// </summary>
        /// <response code="200">Login of User has succeeded</response>
        /// <response code="400">Login of User has failed with errors</response>
        ///<returns>Jwt Token to attach it in authentication header in requests, and the RefreshToken.</returns>
        [HttpPost(ApiRoutes.IdentityRoutes.Login)]
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Login([FromForm] UserLoginRequest request)
        {
            var authResponse = await identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse.Error);
            }

            return Ok(new AuthSuccessResponse
            {
                User=authResponse.User,
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        /// <summary>
        /// Refresh the Jwt Token to renew the expiry date of the session
        /// </summary>
        /// <response code="200">The Refresh token is valid and has been regenerated.</response>
        /// <response code="400">Failed to generate the new Jwt token</response>
        ///<returns>The new Jwt Token, and the new RefreshToken.</returns>
        [AllowAnonymous]
        [HttpPost(ApiRoutes.IdentityRoutes.Refresh)]
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var authResponse = await identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse.Error);
                
            }

            return Ok(new AuthSuccessResponse
            {
                User=authResponse.User,
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        /// <summary>
        /// Check if the user is Authorized
        /// </summary>
        /// <response code="200">The User is Authorized</response>
        [HttpGet(ApiRoutes.IdentityRoutes.CheckIfAuthorized)]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public IActionResult CheckIfAuthorized()
        {
            return Ok();
        }

        #endregion Endpoints
    }
}