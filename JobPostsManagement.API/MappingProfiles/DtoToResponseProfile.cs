using AutoMapper;
using JobPostsManagement.API.Contracts.V1.Responses;
using JobPostsManagement.API.Models;

namespace JobPostsManagement.API.MappingProfiles
{
    public class DtoToResponseProfile : Profile
    {
        public DtoToResponseProfile()
        {
            AllowNullCollections = false;
            CreateMap<Candidate, CandidateResponse>() ;
            CreateMap<Employer, EmployerResponse>() ;
            CreateMap<BaseUser, UserResponse>();
            CreateMap<JobPost, JobPostResponse>();
            CreateMap<JobApplication, JobApplicationResponse>();
        }
    }
}