using AutoMapper;
using System;
using TestCase.Application.Extensions;
using TestCase.Application.Features.CompanyFeatures.Commands.Requests;
using TestCase.Application.Features.CompanyFeatures.Commands.Responses;
using TestCase.Application.Features.CompanyFeatures.Queries.Responses;
using TestCase.Application.Features.JobFeatures.Commands.Requests;
using TestCase.Application.Features.JobFeatures.Commands.Responses;
using TestCase.Application.Features.JobFeatures.Queries.Responses;
using TestCase.Application.Tools;
using TestCase.Domain.Entities;
using TestCase.Domain.Enums;

namespace TestCase.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Company
            CreateMap<CreateCompanyCommandRequest, Company>().ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => HashPasswordHelper.HashPassword(src.Password)));
            CreateMap<UpdateCompanyCommandRequest, Company>();
            CreateMap<Company, CreateCompanyCommandResponse>();

            CreateMap<Company, GetAllCompaniesQueryResponse>();
            CreateMap<Company, GetCompanyByIdQueryResponse>();
            #endregion

            #region Job
            CreateMap<JobCreateCommandRequest, Job>().ForMember(dest => dest.SideRights, opt => opt.MapFrom(src => src.SideRights != null ? string.Join(",", src.SideRights) : null));
            CreateMap<Job, JobCreateCommandResponse>().ForMember(dest => dest.SideRights, opt => opt.MapFrom(src => src.SideRights != null ?
            string.Join(",", src.SideRights.Split(",", StringSplitOptions.None)
                .Select(x => Enum.Parse<SideRightTypes>(x))
                .Select(x => x.GetDescription())) : null));
            CreateMap<Job, GetJobByIdQueryResponse>().ForMember(dest => dest.SideRights, opt => opt.MapFrom(src => src.SideRights != null ?
            string.Join(",", src.SideRights.Split(",", StringSplitOptions.None)
                .Select(x => Enum.Parse<SideRightTypes>(x))
                .Select(x => x.GetDescription())) : null));
            CreateMap<Job, GetJobsByCompanyIdQueryResponse>().ForMember(dest => dest.SideRights, opt => opt.MapFrom(src => src.SideRights != null ? 
            string.Join(",",src.SideRights.Split(",", StringSplitOptions.None)
                .Select(x => Enum.Parse<SideRightTypes>(x))
                .Select(x => x.GetDescription())) :  null));
            CreateMap<Job, JobUpdateCommandResponse>().ForMember(dest => dest.SideRights, opt => opt.MapFrom(src => src.SideRights != null ?
            string.Join(",", src.SideRights.Split(",", StringSplitOptions.None)
                .Select(x => Enum.Parse<SideRightTypes>(x))
                .Select(x => x.GetDescription())) : null));
            #endregion
        }
    }
}
