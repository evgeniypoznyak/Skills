using AutoMapper;
using Skills.Domain.Dto;
using Skills.Domain.Entity;
using Skills.ValueObject;

namespace Skills.Domain.Repository
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDto, Project>()
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
//                .ForMember(dest => dest.Slug,
//                    opt => opt.MapFrom(
//                        src => new Slug(src)
//                    )
//                )
                ;
        }
    }
}