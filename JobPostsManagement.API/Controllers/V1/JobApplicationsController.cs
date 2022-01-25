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
    public class JobApplicationsController : ControllerBase
    {
        #region Fields
        private readonly IJobApplicationService jobApplicationService;
        private readonly ILogger<JobApplicationsController> logger;
        private readonly IMapper mapper;
        private readonly IUriService uriService;
        #endregion

        #region Ctors
        public JobApplicationsController(IJobApplicationService jobApplicationService, ILogger<JobApplicationsController> logger, IMapper mapper, IUriService uriService)
        {
            this.jobApplicationService = jobApplicationService;
            this.mapper = mapper;
            this.logger = logger;
            this.uriService = uriService;
        }
        #endregion

        #region Endpoints

        /// <summary>
        /// Get all jobApplications and its informations.
        /// </summary>
        /// <response code="200">Returns list of jobApplicationResponse</response>
        [ProducesResponseType(typeof(ListResponse<JobApplicationResponse>), 200)]
        [HttpGet(ApiRoutes.JobApplicationsRoutes.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] int? pageNumber = null, [FromQuery] int? pageSize = null)
        {
            var jobApplicationsList = await jobApplicationService.GetAllAsync(pageNumber, pageSize);

            var response = mapper.Map<List<JobApplicationResponse>>(jobApplicationsList);

            if (pageNumber is null || pageNumber.Value <= 0 | pageSize is null || pageSize.Value <= 0)
            {
                return Ok(new ListResponse<JobApplicationResponse>
                {
                    Data = response
                });
            }

            var totalPages =
                Math.Ceiling((decimal)await jobApplicationService.GetCount() / pageSize.Value);
            var nextPage = pageNumber < totalPages ?
                uriService.GetPageUri(ApiRoutes.JobApplicationsRoutes.GetAll, pageNumber.Value + 1, pageSize.Value).ToString() : null;
            var prevPage = pageNumber.Value > 1 ?
                uriService.GetPageUri(ApiRoutes.JobApplicationsRoutes.GetAll, pageNumber.Value - 1, pageSize.Value).ToString() : null;

            return Ok(new PagedResponse<JobApplicationResponse>
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
        /// Get specified jobApplication by given jobApplicationId
        /// </summary>
        /// <param name="jobApplicationId">Id of the jobApplication to find</param>
        /// <response code="200">JobApplication object has found.</response>
        /// <response code="404">JobApplication object has not found, Id is incorrect.</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(ObjectResponse<JobApplicationResponse>), 200)]
        [ProducesResponseType(404)]
        [HttpGet(ApiRoutes.JobApplicationsRoutes.GetById)]
        public async Task<IActionResult> Get([FromRoute] long jobApplicationId)
        {
            var jobApplication = await jobApplicationService.GetByIdAsync(jobApplicationId);

            if (jobApplication is null)
            {
                return NotFound();
            }

            var response = mapper.Map<JobApplicationResponse>(jobApplication);
            return Ok(new ObjectResponse<JobApplicationResponse>(response));
        }


        /// <summary>
        /// Create new jobApplication
        /// </summary>
        /// <response code="201">JobApplication object has created successfully.</response>
        /// <response code="400">JobApplication object has not created.</response>
        [ProducesResponseType(typeof(ObjectResponse<JobApplicationResponse>), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [HttpPost(ApiRoutes.JobApplicationsRoutes.Create)]
        public async Task<IActionResult> Create([FromBody] CreateUpdateJobApplicationRequest request)
        {
            var createdJobApplication = mapper.Map<JobApplication>(request);

            var created = await jobApplicationService.CreateAsync(createdJobApplication);

            if (!created)
            {
                return BadRequest(new ErrorResponse { Code = "ObjectNotCreated", Description = "JobApplication object has not created." });
            }

            var response = mapper.Map<JobApplicationResponse>(createdJobApplication);

            return Created(uriService.GetJobApplicationUri(createdJobApplication), new ObjectResponse<JobApplicationResponse>(response));
        }


        /// <summary>
        /// Update the jobApplication speciefied by given Id with new values.
        /// </summary>
        /// <param name="jobApplicationId">Id of the jobApplication to find</param>
        /// <response code="200">JobApplication object has updated successfully.</response>
        /// <response code="400">JobApplication object has not updated.</response>
        /// <response code="404">JobApplication object has not found, Id is incorrect.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(404)]
        [HttpPut(ApiRoutes.JobApplicationsRoutes.Update)]
        public async Task<IActionResult> Update([FromRoute] long jobApplicationId, [FromBody] CreateUpdateJobApplicationRequest request)
        {
            var updatedJobApplication = await jobApplicationService.GetByIdAsync(jobApplicationId, false);
            if (updatedJobApplication is null)
            {
                return NotFound();
            }
            updatedJobApplication = mapper.Map<CreateUpdateJobApplicationRequest, JobApplication>(request, updatedJobApplication);

            var updated = await jobApplicationService.UpdateAsync(updatedJobApplication);
            if (updated)
            {
                var response = mapper.Map<JobApplicationResponse>(updatedJobApplication);
                return Ok();
            }

            return BadRequest(new ErrorResponse { Code = "ObjectNotUpdated", Description = "JobApplication object has not updated." });
        }

        /// <summary>
        /// Delete the jobApplication speciefied by given Id.
        /// </summary>
        /// <param name="jobApplicationId">Id of the jobApplication to find</param>
        /// <response code="204">JobApplication object has deleted successfully.</response>
        /// <response code="400">JobApplication object has not deleted.</response>
        /// <response code="404">JobApplication object has not found, Id is incorrect.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(404)]
        [HttpDelete(ApiRoutes.JobApplicationsRoutes.Delete)]
        public async Task<IActionResult> Delete([FromRoute] long jobApplicationId)
        {
            var deletedJobApplication = await jobApplicationService.GetByIdAsync(jobApplicationId, false);
            if (deletedJobApplication is null)
            {
                return NotFound();
            }

            var deleted = await jobApplicationService.DeleteAsync(deletedJobApplication);

            if (deleted)
            {
                return NoContent();
            }

            return BadRequest(new ErrorResponse { Code = "ObjectNotDeleted", Description = "JobApplication object has not deleted." });
        }
        #endregion
    }
}
