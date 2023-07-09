using AutoMapper;
using TestCase.Application.Features.CompanyFeatures.Commands.Requests;
using TestCase.Application.Features.CompanyFeatures.Commands.Responses;
using TestCase.Application.Features.CompanyFeatures.Queries.Responses;
using TestCase.Application.Features.JobFeatures.Commands.Requests;
using TestCase.Application.Features.JobFeatures.Commands.Responses;
using TestCase.Application.Features.JobFeatures.Queries.Responses;
using TestCase.Application.Tools;
using TestCase.Domain.Entities;

namespace TestCase.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Company
            CreateMap<CreateCompanyCommandRequest, Company>().ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => HashPasswordHelper.HashPassword(src.Password)));
            CreateMap<Company, CreateCompanyCommandResponse>();

            CreateMap<Company, GetAllCompaniesQueryResponse>();
            CreateMap<Company, GetCompanyByIdQueryResponse>();
            #endregion

            #region Job
            CreateMap<JobCreateCommandRequest, Job>();
            CreateMap<Job, JobCreateCommandResponse>();
            CreateMap<Job, GetJobByIdQueryResponse>();
            CreateMap<Job, GetJobsByCompanyIdQueryResponse>();
            #endregion
        }
    }
}
