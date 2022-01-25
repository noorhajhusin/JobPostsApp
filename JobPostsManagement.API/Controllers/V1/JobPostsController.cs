using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JobPostsManagement.API.Contracts.V1.Requests;
using JobPostsManagement.API.Contracts.V1.Responses;
using JobPostsManagement.API.Contracts.V1;
using JobPostsManagement.API.Interfaces;
using JobPostsManagement.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using JobPostsManagement.API.Contracts;

namespace JobPostsManagement.API.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class JobPostsController : ControllerBase
    {
        #region Fields
        private readonly IJobPostService jobpostService;
        private readonly ILogger<JobPostsController> logger;
        private readonly IMapper mapper;
        private readonly IUriService uriService;
        #endregion

        #region Ctors
        public JobPostsController(IJobPostService jobpostService, ILogger<JobPostsController> logger, IMapper mapper, IUriService uriService)
        {
            this.jobpostService = jobpostService;
            this.mapper = mapper;
            this.logger = logger;
            this.uriService = uriService;
        }
        #endregion

        #region Endpoints

        /// <summary>
        /// Get all jobposts and its informations.
        /// </summary>
        /// <response code="200">Returns list of jobpostResponse</response>
        [ProducesResponseType(typeof(ListResponse<JobPostResponse>), 200)]
        [HttpGet(ApiRoutes.JobPostsRoutes.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] int? pageNumber = null, [FromQuery] int? pageSize = null)
        {
            var jobpostsList = await jobpostService.GetAllAsync(pageNumber, pageSize);

            var response = mapper.Map<List<JobPostResponse>>(jobpostsList);

            if (pageNumber is null || pageNumber.Value <= 0 | pageSize is null || pageSize.Value <= 0)
            {
                return Ok(new ListResponse<JobPostResponse>
                {
                    Data = response
                });
            }

            var totalPages =
                Math.Ceiling((decimal)await jobpostService.GetCount() / pageSize.Value);
            var nextPage = pageNumber < totalPages ?
                uriService.GetPageUri(ApiRoutes.JobPostsRoutes.GetAll, pageNumber.Value + 1, pageSize.Value).ToString() : null;
            var prevPage = pageNumber.Value > 1 ?
                uriService.GetPageUri(ApiRoutes.JobPostsRoutes.GetAll, pageNumber.Value - 1, pageSize.Value).ToString() : null;

            return Ok(new PagedResponse<JobPostResponse>
            {
                Data = response,
                PageNumber = pageNumber.Value,
                PageSize = pageSize.Value,
                TotalPages = (int)totalPages,
                NextPage = nextPage,
                PreviousPage = prevPage
            });
        }


        /// <summary>
        /// Get specified jobpost by given jobpostId
        /// </summary>
        /// <param name="jobpostId">Id of the jobpost to find</param>
        /// <response code="200">JobPost object has found.</response>
        /// <response code="404">JobPost object has not found, Id is incorrect.</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(ObjectResponse<JobPostResponse>), 200)]
        [ProducesResponseType(404)]
        [HttpGet(ApiRoutes.JobPostsRoutes.GetById)]
        public async Task<IActionResult> Get([FromRoute] long jobpostId)
        {
            var jobpost = await jobpostService.GetByIdAsync(jobpostId);

            if (jobpost is null)
            {
                return NotFound();
            }

            var response = mapper.Map<JobPostResponse>(jobpost);
            return Ok(new ObjectResponse<JobPostResponse>(response));
        }


        /// <summary>
        /// Create new jobpost
        /// </summary>
        /// <response code="201">JobPost object has created successfully.</response>
        /// <response code="400">JobPost object has not created.</response>
        [ProducesResponseType(typeof(ObjectResponse<JobPostResponse>), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [HttpPost(ApiRoutes.JobPostsRoutes.Create)]
        public async Task<IActionResult> Create([FromBody] CreateUpdateJobPostRequest request)
        {
            var createdJobPost = mapper.Map<JobPost>(request);

            var created = await jobpostService.CreateAsync(createdJobPost);

            if (!created)
            {
                return BadRequest(new ErrorResponse { Code = "ObjectNotCreated", Description = "JobPost object has not created." });
            }

            var response = mapper.Map<JobPostResponse>(createdJobPost);

            return Created(uriService.GetJobPostUri(createdJobPost), new ObjectResponse<JobPostResponse>(response));
        }


        /// <summary>
        /// Update the jobpost speciefied by given Id with new values.
        /// </summary>
        /// <param name="jobpostId">Id of the jobpost to find</param>
        /// <response code="200">JobPost object has updated successfully.</response>
        /// <response code="400">JobPost object has not updated.</response>
        /// <response code="404">JobPost object has not found, Id is incorrect.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(404)]
        [HttpPut(ApiRoutes.JobPostsRoutes.Update)]
        public async Task<IActionResult> Update([FromRoute] long jobpostId, [FromBody] CreateUpdateJobPostRequest request)
        {
            var updatedJobPost = await jobpostService.GetByIdAsync(jobpostId, false);
            if (updatedJobPost is null)
            {
                return NotFound();
            }
            updatedJobPost = mapper.Map<CreateUpdateJobPostRequest, JobPost>(request, updatedJobPost);

            var updated = await jobpostService.UpdateAsync(updatedJobPost);
            if (updated)
            {
                var response = mapper.Map<JobPostResponse>(updatedJobPost);
                return Ok();
            }

            return BadRequest(new ErrorResponse { Code = "ObjectNotUpdated", Description = "JobPost object has not updated." });
        }

        /// <summary>
        /// Delete the jobpost speciefied by given Id.
        /// </summary>
        /// <param name="jobpostId">Id of the jobpost to find</param>
        /// <response code="204">JobPost object has deleted successfully.</response>
        /// <response code="400">JobPost object has not deleted.</response>
        /// <response code="404">JobPost object has not found, Id is incorrect.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(404)]
        [HttpDelete(ApiRoutes.JobPostsRoutes.Delete)]
        public async Task<IActionResult> Delete([FromRoute] long jobpostId)
        {
            var deletedJobPost = await jobpostService.GetByIdAsync(jobpostId, false);
            if (deletedJobPost is null)
            {
                return NotFound();
            }

            var deleted = await jobpostService.DeleteAsync(deletedJobPost);

            if (deleted)
            {
                return NoContent();
            }

            return BadRequest(new ErrorResponse { Code = "ObjectNotDeleted", Description = "JobPost object has not deleted." });
        }
        #endregion
    }
}
