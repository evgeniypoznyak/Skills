using System;
using System.Collections.Generic;
using AutoMapper;
using Skills.Domain.Entity;
using Skills.Domain.ValueObject;
using Skills.Infrastructure.Adapter;

namespace Skills.Domain.Repository
{
    public class SkillProfile : Profile
    {
        public SkillProfile()
        {
            CreateMap<SkillDto, Skill>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(
                        src => new ObjectId(src.Id)
                    )
                )
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(
                        src => new ObjectName(src.Name)
                    )
                )
                .ForMember(dest => dest.Projects,
                    opt => opt.MapFrom(
                        src => Mapper.Map<List<ProjectDto>, List<Project>>(src.Projects)
                    )
                );

//                .ForMember(dest => dest.ReportName, opt => opt.MapFrom(src => new ReportName(src.ReportName)))
//                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => new UserId(src.UserId)))
//                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => Convert.ToDateTime(src.DateCreated)))
               
        }
    }
}