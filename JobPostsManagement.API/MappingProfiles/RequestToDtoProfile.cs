using AutoMapper;
using JobPostsManagement.API.Contracts.V1.Requests;
using JobPostsManagement.API.Contracts.V1.Responses;
using JobPostsManagement.API.Models;

namespace JobPostsManagement.API.MappingProfiles
{
    public class RequestToDtoProfile : Profile
    {
        public RequestToDtoProfile()
        {
            AllowNullCollections = false;
            CreateMap<CreateUpdateJobPostRequest, JobPost>()
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue());

            CreateMap<RegisterEmployerRequest, Employer>();
        }
    }
}