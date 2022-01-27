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
            CreateMap<CreateUpdateJobApplicationRequest, JobApplication>()
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue());

            CreateMap<RegisterEmployerRequest, Employer>()
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue());
            CreateMap<UpdateEmployerRequest, Employer>()
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue());
            CreateMap<UpdateCandidateRequest, Candidate>()
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue());
            CreateMap<UpdateUserRequest, BaseUser>()
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.Email, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.UserName, opt => opt.UseDestinationValue());
        }
    }
}